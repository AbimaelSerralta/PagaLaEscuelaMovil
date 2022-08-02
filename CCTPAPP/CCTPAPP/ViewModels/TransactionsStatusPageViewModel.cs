using CCTPAPP.Models.Praga;
using CCTPAPP.PlatformServices.Abstract;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CCTPAPP.ViewModels
{
    public class TransactionsStatusPageViewModel : ViewModelBase, IPageLifecycleAware, INavigationAware
    {
        #region Propiedades

        PragaTransactionData pragaTransactionData = new PragaTransactionData();
        IPragaService dps = DependencyService.Resolve<IPragaService>();
        IPageDialogService _dialogService;
        IDialogService _iDialogService;

        private string _reference;
        public string Reference
        {
            get { return _reference; }
            set { SetProperty(ref _reference, value); }
        }

        private string _concept;
        public string Concept
        {
            get { return _concept; }
            set { SetProperty(ref _concept, value); }
        }

        private string _amount;
        public string Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private string _card;
        public string Card
        {
            get { return _card; }
            set { SetProperty(ref _card, value); }
        }

        private string _authorization;
        public string Authorization
        {
            get { return _authorization; }
            set { SetProperty(ref _authorization, value); }
        }
        private string _folio;
        public string Folio
        {
            get { return _folio; }
            set { SetProperty(ref _folio, value); }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private bool _isVisibleRegresarInicio;
        public bool IsVisibleRegresarInicio
        {
            get { return _isVisibleRegresarInicio; }
            set { SetProperty(ref _isVisibleRegresarInicio, value); }
        }

        private string _btnTitleSendEmail;
        public string BtnTitleSendEmail
        {
            get { return _btnTitleSendEmail; }
            set { SetProperty(ref _btnTitleSendEmail, value); }
        }

        string BtnSendEmailAction = string.Empty;

        string apiKey = string.Empty;
        string encryptionKey = string.Empty;
        string userCode = string.Empty;
        int businessID = 0;
        #endregion

        #region Atributos

        private readonly INavigationService _navigationService;

        public DelegateCommand SendVoucherCommand { get; private set; }
        public DelegateCommand GoToHomeCommand { get; private set; }
        #endregion

        #region Constructor
        public TransactionsStatusPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDialogService iDialogService)
            : base(navigationService)
        {
            Title = "Estatus del pago";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _iDialogService = iDialogService;

            IsVisibleRegresarInicio = false;
            BtnTitleSendEmail = "Enviar Voucher";
            BtnSendEmailAction = "Enviar";

            SendVoucherCommand = new DelegateCommand(ExecuteSendVoucherCommand);
            GoToHomeCommand = new DelegateCommand(ExecuteGoToHomeCommand);
        }
        #endregion

        #region ICommands

        private void ExecuteSendVoucherCommand()
        {
            SendVoucher();
            //_navigationService.NavigateAsync("NavigationPage/MenuTabbedPage");
        }
        private void ExecuteGoToHomeCommand()
        {
            _navigationService.NavigateAsync("/NavigationPage/MenuTabbedPage");
        }

        #endregion

        #region Metodos

        private void SendVoucher()
        {
            switch (BtnSendEmailAction)
            {
                case "Enviar":
                    if (pragaTransactionData.isQPS == false && pragaTransactionData.isChipPin == false)
                    {
                        _iDialogService.ShowDialog("SignVoucherPage", new DialogParameters
                        {
                            {"apiKey", apiKey },
                            {"encryptionKey", encryptionKey },
                            {"userCode", userCode },
                            {"businessID", businessID },
                            {"Folio", Folio }
                        }, CloseDialogCallback);
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            dps.InitController();

                            dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);
                            dps.SendVoucher(Email, Folio);

                        });
                    }
                    break;
                case "Reenviar":
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        dps.InitController();

                        dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);
                        dps.SendVoucher(Email, Folio);

                    });
                    break;
            }
        }

        private void CloseDialogCallback(IDialogResult obj)
        {
            if (obj.Parameters.GetValue<ReturnVoucherData>("returnVoucherData") != null)
            {
                ReturnVoucherData returnVoucherData = obj.Parameters.GetValue<ReturnVoucherData>("returnVoucherData");
                //OnReturnVoucher(returnVoucherData);

                if (returnVoucherData.sent)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        dps.InitController();

                        dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);
                        dps.SendVoucher(Email, Folio);

                    });
                }
                else
                {
                    _dialogService.DisplayAlertAsync("Codigo: " + returnVoucherData.pragaErrorData.id, returnVoucherData.pragaErrorData.description, "OK");
                }
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Funcion para capturar eventos "AndroidPragaListener"
        /// </summary>
        /// <param name="data"></param>
        private void OnReturnVoucher(ReturnVoucherData returnVoucherData)
        {
            if (returnVoucherData != null)
            {
                if (returnVoucherData.sent)
                {
                    _dialogService.DisplayAlertAsync("Mensaje: ", "Vouncher enviado correctamente", "OK");
                    IsVisibleRegresarInicio = true;
                    BtnTitleSendEmail = "Reenviar Voucher";
                    BtnSendEmailAction = "Reenviar";
                }
                else
                {
                    _dialogService.DisplayAlertAsync("Codigo: " + returnVoucherData.pragaErrorData.id, returnVoucherData.pragaErrorData.description, "OK");
                    IsVisibleRegresarInicio = false;
                }
            }
        }

        #endregion

        #region Page Lifecycle

        public void OnAppearing()
        {
            // Subscribe to Event
            MessagingCenter.Subscribe<ReturnVoucherData>(this, "OnReturnVoucher", this.OnReturnVoucher);
        }

        public void OnDisappearing()
        {
            // Eliminar referencia al evento
            MessagingCenter.Unsubscribe<ReturnVoucherData>(this, "OnReturnVoucher");
        }

        #endregion

        #region INavigationAware

        override
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.GetValue<string>("apiKey")) && !string.IsNullOrEmpty(parameters.GetValue<string>("encryptionKey")) && !string.IsNullOrEmpty(parameters.GetValue<string>("userCode")) && int.Parse(parameters.GetValue<string>("businessID")) != 0)
            {
                //=> Se asigna a las variables
                apiKey = parameters.GetValue<string>("apiKey");
                encryptionKey = parameters.GetValue<string>("encryptionKey");
                userCode = parameters.GetValue<string>("userCode");
                businessID = int.Parse(parameters.GetValue<string>("businessID"));
                Concept = parameters.GetValue<string>("Concept");
                Email = parameters.GetValue<string>("Email");

                if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(encryptionKey) && !string.IsNullOrEmpty(userCode) && businessID != 0)
                {
                    pragaTransactionData = parameters.GetValue<PragaTransactionData>("PragaTransactionData");

                    Reference = pragaTransactionData.reference;
                    Amount = "$" + decimal.Parse(pragaTransactionData.amount).ToString("N2");
                    Card = "XXXX XXXX XXXX " + pragaTransactionData.ccNumber;
                    Authorization = pragaTransactionData.auth;
                    Folio = pragaTransactionData.folio;
                    Date = pragaTransactionData.date + " " + pragaTransactionData.time;
                }
            }
        }

        override
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        #endregion
    }
}
