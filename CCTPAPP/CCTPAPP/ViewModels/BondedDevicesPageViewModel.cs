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
    public class BondedDevicesPageViewModel : ViewModelBase, IPageLifecycleAware, INavigationAware
    {
        #region Propiedades

        //var dps = DependencyService.Get<IPragaService>();
        IPragaService dps = DependencyService.Resolve<IPragaService>();
        IPageDialogService _dialogService;

        private List<PragaBondedDevicesData> _itemsSourceBondedDevices;
        public List<PragaBondedDevicesData> ItemsSourceBondedDevices
        {
            get { return _itemsSourceBondedDevices; }
            set { SetProperty(ref _itemsSourceBondedDevices, value); }
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

        private PragaBondedDevicesData _itemSelectedBondedDevice { get; set; }
        public PragaBondedDevicesData ItemSelectedBondedDevice
        {
            get { return _itemSelectedBondedDevice; }
            set
            {
                if (_itemSelectedBondedDevice != value)
                {
                    _itemSelectedBondedDevice = value;
                    GetSelectDevice();
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

        public BondedDevicesPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Listado de Lectores";
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
            GetBondedDevices();
            IsRefreshing = false;
        }

        #endregion

        #region Metodos

        private async void GetBondedDevices()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                dps.InitController();

                dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);
                dps.SetDevice(PragaDeviceReaderData.QPOS);
                dps.GetBondedDevices();

            });
        }

        private void GetSelectDevice()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("macAddress", ItemSelectedBondedDevice.address);
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
        private void OnReturnBondedDevices(List<PragaBondedDevicesData> lsPragaBondedDevicesDatas)
        {
            ItemsSourceBondedDevices = lsPragaBondedDevicesDatas;
        }

        #endregion

        #region Page Lifecycle

        public void OnAppearing()
        {
            // Subscribe to Event
            MessagingCenter.Subscribe<PragaErrorData>(this, "OnPragaError", this.OnPragaError);
            MessagingCenter.Subscribe<List<PragaBondedDevicesData>>(this, "OnReturnBondedDevices", this.OnReturnBondedDevices);
        }

        public void OnDisappearing()
        {
            // Eliminar referencia al evento
            MessagingCenter.Unsubscribe<PragaErrorData>(this, "OnPragaError");
            MessagingCenter.Unsubscribe<PragaBondedDevicesData>(this, "OnReturnBondedDevices");

        }

        #endregion

        #region INavigationAware

        override
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //=> Se asigna a las variables
            apiKey = parameters.GetValue<string>("apiKey");
            encryptionKey = parameters.GetValue<string>("encryptionKey");
            userCode = parameters.GetValue<string>("userCode");
            businessID = int.Parse(parameters.GetValue<string>("businessID"));

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(encryptionKey) && !string.IsNullOrEmpty(userCode) && businessID != 0)
            {
                GetBondedDevices();
            }
        }

        override
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        #endregion
    }
}
