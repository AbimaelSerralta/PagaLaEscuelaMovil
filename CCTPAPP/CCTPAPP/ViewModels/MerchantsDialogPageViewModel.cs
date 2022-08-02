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
    public class MerchantsDialogPageViewModel : BindableBase, IDialogAware
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

        private List<PragaMerchants> _itemsSourceMerchants;
        public List<PragaMerchants> ItemsSourceMerchants
        {
            get { return _itemsSourceMerchants; }
            set { SetProperty(ref _itemsSourceMerchants, value); }
        }

        private bool _isPullToRefreshEnabled;
        public bool IsPullToRefreshEnabled
        {
            get { return _isPullToRefreshEnabled; }
            set { SetProperty(ref _isPullToRefreshEnabled, value); }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        private PragaMerchants _itemSelectedMerchant { get; set; }
        public PragaMerchants ItemSelectedMerchant
        {
            get { return _itemSelectedMerchant; }
            set
            {
                if (_itemSelectedMerchant != value)
                {
                    _itemSelectedMerchant = value;
                    GetSelectMerchant();
                }
            }
        }

        string apiKey = string.Empty;
        string encryptionKey = string.Empty;
        string userCode = string.Empty;
        int businessID = 0;

        #endregion

        #region Atributos
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand RefreshCommand { get; private set; }

        #endregion

        #region Constructor

        public MerchantsDialogPageViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;

            CloseCommand = new DelegateCommand(() => RequestClose(null));
            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand);
        }

        #endregion

        #region ICommands

        public event Action<IDialogParameters> RequestClose;
        public void ExecuteRefreshCommand()
        {
            IsRefreshing = true;
            GetMerchants();
            IsRefreshing = false;
        }

        #endregion

        #region Metodos

        private async void GetMerchants()
        {
            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    dps.InitController();

            //    dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);
            //    dps.SetDevice(PragaDeviceReaderData.QPOS);
            //    dps.GetMerchants();

            //});
        }

        private void GetSelectMerchant()
        {
            RequestClose(new DialogParameters
            {
                { "merchantSelected", ItemSelectedMerchant }
            });
        }
        #endregion

        #region Events
        /// <summary>
        /// Funcion para capturar eventos "AndroidPragaListener"
        /// </summary>
        /// <param name="data"></param>
        private void OnPragaError(PragaErrorData data)
        {
            _dialogService.DisplayAlertAsync("Codigo: " + data.id, data.description, "OK");
        }
        private void OnReturnMerchants(List<PragaMerchants> lsMerchants)
        {
            ItemsSourceMerchants = lsMerchants;
        }

        #endregion

        #region IDialogAware
        public bool CanCloseDialog() => true;
        public void OnDialogClosed()
        {
            // Eliminar referencia al evento
            MessagingCenter.Unsubscribe<PragaErrorData>(this, "OnPragaError");
            MessagingCenter.Unsubscribe<PragaMerchants>(this, "OnReturnMerchants");
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            //=> Se asigna a las variables
            List<PragaMerchants> lsPragaMerchants = parameters.GetValue<List<PragaMerchants>>("lsPragaMerchants");

            ItemsSourceMerchants = lsPragaMerchants;

            // Subscribe to Event
            MessagingCenter.Subscribe<PragaErrorData>(this, "OnPragaError", this.OnPragaError);
            MessagingCenter.Subscribe<List<PragaMerchants>>(this, "OnReturnMerchants", this.OnReturnMerchants);
        }

        #endregion
    }
}
