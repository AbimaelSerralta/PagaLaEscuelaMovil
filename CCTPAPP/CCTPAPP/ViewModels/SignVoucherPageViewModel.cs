using CCTPAPP.Models.Praga;
using CCTPAPP.PlatformServices.Abstract;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CCTPAPP.ViewModels
{
    public class SignVoucherPageViewModel : BindableBase, IDialogAware
    {
        #region Propiedades

        IPragaService dps = DependencyService.Resolve<IPragaService>();
        IPageDialogService _dialogService;

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        string sing = "";
        string apiKey = string.Empty;
        string encryptionKey = string.Empty;
        string userCode = string.Empty;
        int businessID = 0;
        string Folio = "";

        #endregion

        #region Atributos
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SendVoucherCommand { get; private set; }

        #endregion

        #region Constructor

        public SignVoucherPageViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;

            CloseCommand = new DelegateCommand(() => RequestClose(null));
            SendVoucherCommand = new DelegateCommand(ExcecuteSendVoucherCommand);
        }

        #endregion

        #region ICommands

        public event Action<IDialogParameters> RequestClose;
        private void ExcecuteSendVoucherCommand()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                dps.InitController();

                dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);
                dps.SignVoucher(sing, Folio);

            });
        }

        #endregion

        #region Events
        /// <summary>
        /// Funcion para capturar eventos "AndroidPragaListener"
        /// </summary>
        /// <param name="data"></param>
        private void SingData(string SingData)
        {
            sing = SingData;

            ExcecuteSendVoucherCommand();
        }
        private void OnReturnSingVoucher(ReturnVoucherData returnVoucherData)
        {
            if (returnVoucherData.sent)
            {
                RequestClose(new DialogParameters
                {
                    { "returnVoucherData", returnVoucherData }
                });

                //_dialogService.DisplayAlertAsync("Mensaje: ", "Vouncher enviado correctamente", "OK");
            }
            else
            {
                _dialogService.DisplayAlertAsync("Codigo: " + returnVoucherData.pragaErrorData.id, returnVoucherData.pragaErrorData.description, "OK");
            }
        }

        #endregion

        #region IDialogAware
        public bool CanCloseDialog() => true;
        public void OnDialogClosed()
        {
            // Eliminar referencia al evento
            MessagingCenter.Unsubscribe<string>(this, "SingData");
            MessagingCenter.Unsubscribe<ReturnVoucherData>(this, "OnReturnSingVoucher");
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            apiKey = parameters.GetValue<string>("apiKey");
            encryptionKey = parameters.GetValue<string>("encryptionKey");
            userCode = parameters.GetValue<string>("userCode");
            businessID = int.Parse(parameters.GetValue<string>("businessID"));
            Folio = parameters.GetValue<string>("Folio"); ;

            //apiKey = "ZDMxYzc3MGItZjEyMS00OTRhLTkxNmQtYmE5Yjk0M2YzYzlm";
            //encryptionKey = "NDQ1MUI0QTJFQkE5RTNENDlFNzk4MUZEMjQ2NEMzNjE=";
            //userCode = "1610137579779";
            //businessID = 13131;
            //Folio = "099992889";

            // Subscribe to Event
            MessagingCenter.Subscribe<string>(this, "SingData", this.SingData);
            MessagingCenter.Subscribe<ReturnVoucherData>(this, "OnReturnSingVoucher", this.OnReturnSingVoucher);
        }

        #endregion
    }
}
