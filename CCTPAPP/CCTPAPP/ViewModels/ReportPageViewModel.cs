using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using Xamarin.Forms;
using CCTPAPP.Models.Praga;
using Prism.AppModel;
using CCTPAPP.PlatformServices.Abstract;
using Prism.Services;
using System;
using CCTPAPP.Resources;
using Xamarin.Essentials;
using CCTPAPP.Models.Apis;

namespace CCTPAPP.ViewModels
{
    public class ReportPageViewModel : ViewModelBase, IPageLifecycleAware
    {
        #region Propiedades

        //var dps = DependencyService.Get<IPragaService>();
        IPragaService dps = DependencyService.Resolve<IPragaService>();
        IPageDialogService _dialogService;
        bool continuarProcesoPraga = true;

        private List<TransactionReportReader> _itSoReporte;
        public List<TransactionReportReader> ItSoReporte
        {
            get { return _itSoReporte; }
            set { SetProperty(ref _itSoReporte, value); }
        }

        private TransactionReportReader _itemSelectedReportPaymet { get; set; }
        public TransactionReportReader ItemSelectedReportPaymet
        {
            get { return _itemSelectedReportPaymet; }
            set
            {
                if (_itemSelectedReportPaymet != value)
                {
                    _itemSelectedReportPaymet = value;
                    GetReportPaymet();
                }
            }
        }

        private DateTime _fechaInicio;
        public DateTime FechaInicio
        {
            get { return _fechaInicio; }
            set { SetProperty(ref _fechaInicio, value); }
        }
        
        private DateTime _fechaFinal;
        public DateTime FechaFinal
        {
            get { return _fechaFinal; }
            set { SetProperty(ref _fechaFinal, value); }
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

        private bool _isLoading;
        public bool IsLoading
        {
            get { return this._isLoading; }

            set { SetProperty(ref this._isLoading, value); }
        }
        #endregion

        #region Atributos
        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;

        #endregion

        #region Constructor

        public ReportPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Reporte";
            _navigationService = navigationService;
            _dialogService = dialogService;

            FechaInicio = DateTime.Now;
            FechaFinal = DateTime.Now;
            IsPullToRefreshEnabled = true;
            IsLoading = false;

            // Subscribe to Event
            MessagingCenter.Subscribe<TransactionsData>(this, "OnReturnTransactions", this.OnReturnTransactions);

            GetTransactions();
        }

        #endregion

        #region ICommands
        public DelegateCommand RefreshCommand =>
                    _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteRefreshCommand));
        async void ExecuteRefreshCommand()
        {
            IsRefreshing = true;
            GetTransactions();
            IsRefreshing = false;
        }

        #endregion

        #region Metodos

        private async void GetTransactions()
        {
            this.IsLoading = true;
            bool checkInternet = ValidateInternet();

            //Se realiza la consulta para obtener datos y las credenciales
            ApiServices apiServices = new ApiServices("GetPayReader");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("UidAppMovil", "4A1F76A5-AA62-4C5D-8E5B-15A7826BACAC");

            var result = await apiServices.POST<TransactionReportReader>(
            action: "",
            responseType: ApiServices.ResponseType.List,
            parameters: parameters);

            if (result != null && result.IsSuccess)
            {
                List<TransactionReportReader> lsTransactionReportReader = new List<TransactionReportReader>();
                lsTransactionReportReader = (List<TransactionReportReader>)result.Result;
                
                ItSoReporte = lsTransactionReportReader;

                this.IsLoading = false;
            }
            else
            {
                if (checkInternet)
                {
                    await _dialogService.DisplayAlertAsync("Error de conexión", "Lo sentimos, el dispositivo no esta conectado a una red, por favor verifique su conexión.", "Aceptar");
                    //await _dialogService.DisplayAlertAsync("Error de conexión", "Lo sentimos, no hemos tenido conexión con el servicio, favor intentarlo más tarde.", "Aceptar");
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Error de conexión",
                        "Lo sentimos, ha ocurrido un error inesperado."
                        + Environment.NewLine + "Error: " + result.Message, "Aceptar");
                }

                this.IsLoading = false;

                return;
            }
        }

        //Reporte desde la PragaController
        //private async void GetTransactions()
        //{
        //    string apiKey = string.Empty;
        //    string encryptionKey = string.Empty;
        //    string userCode = string.Empty;
        //    int businessID = 0;

        //    //Se realiza la consulta para obtener las credenciales
        //    //=>

        //    //=> El resultado se asigna a las variables
        //    apiKey = "YzFlMzNjMWQtYWUxOS00NjVhLThjMjgtN2Y2OTFlYmIyM2Q3";
        //    encryptionKey = "M0QzOUY5REU1MEIzMkFBNEMwQTQyMjFCMzE1MkE5NTA=";
        //    userCode = "1628578238248";
        //    businessID = 14421;

        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        dps.InitController();

        //        dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);
        //        dps.GetTransactions(FechaInicio.ToString("dd/MM/yyyy"), FechaFinal.ToString("dd/MM/yyyy"));

        //    });
        //}

        private async void GetReportPaymet()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("TransactionReportReader", ItemSelectedReportPaymet);
            await _navigationService.NavigateAsync("ReportVoucherPage", navigationParameters);
        }

        private bool ValidateInternet()
        {
            bool result = false;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _dialogService.DisplayAlertAsync("Error de conexión", "Lo sentimos, el dispositivo no esta conectado a una red, por favor verifique su conexión.", "Aceptar");
                this.IsLoading = false;
                result = true;
            }

            return result;
        }
        #endregion

        #region Events
        /// <summary>
        /// Funcion para capturar evento "OnPragaError"
        /// </summary>
        /// <param name="data"></param>

        private void OnReturnTransactions(TransactionsData transactionsData)
        {
            if (!string.IsNullOrEmpty(transactionsData.pragaErrorData.id) && !string.IsNullOrEmpty(transactionsData.pragaErrorData.description))
            {
                _dialogService.DisplayAlertAsync("Codigo: " + transactionsData.pragaErrorData.id, transactionsData.pragaErrorData.description, "OK");
            }
            else
            {
                //ItSoReporte = transactionsData.pragaListTransactionData.listTransaction;
            }
        }

        #endregion

        #region Page Lifecycle

        public void OnAppearing()
        {

        }

        public void OnDisappearing()
        {
            // Eliminar referencia al evento
            //MessagingCenter.Unsubscribe<PragaErrorData>(this, "OnPragaError");

        }

        #endregion
    }
}