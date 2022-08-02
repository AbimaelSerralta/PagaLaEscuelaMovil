using CCTPAPP.Models.Praga;
using CCTPAPP.PlatformServices.Abstract;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CCTPAPP.ViewModels
{
    public class MerchantsPageViewModel : ViewModelBase, IPageLifecycleAware, INavigationAware
    {
        #region Propiedades

        //var dps = DependencyService.Get<IPragaService>();
        IPragaService dps = DependencyService.Resolve<IPragaService>();
        IPageDialogService _dialogService;

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
        private readonly INavigationService _navigationService;

        public DelegateCommand RefreshCommand { get; private set; }
        #endregion

        #region Constructor

        public MerchantsPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Listado de tipo de pago";
            _navigationService = navigationService;
            _dialogService = dialogService;

            IsPullToRefreshEnabled = true;

            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand);
        }

        #endregion

        #region ICommands
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
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("merchantSelected", ItemSelectedMerchant);
            _navigationService.GoBackAsync(navigationParameters);
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

        #region Page Lifecycle

        public void OnAppearing()
        {
            // Subscribe to Event
            MessagingCenter.Subscribe<PragaErrorData>(this, "OnPragaError", this.OnPragaError);
            MessagingCenter.Subscribe<List<PragaMerchants>>(this, "OnReturnMerchants", this.OnReturnMerchants);
        }

        public void OnDisappearing()
        {
            // Eliminar referencia al evento
            MessagingCenter.Unsubscribe<PragaErrorData>(this, "OnPragaError");
            MessagingCenter.Unsubscribe<PragaMerchants>(this, "OnReturnMerchants");

        }

        #endregion

        #region INavigationAware

        override
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //=> Se asigna a las variables
            List<PragaMerchants> lsPragaMerchants = parameters.GetValue<List<PragaMerchants>>("lsPragaMerchants");

            ItemsSourceMerchants = lsPragaMerchants;
        }

        override
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        #endregion
    }
}
