using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using Xamarin.Forms;
using CCTPAPP.Models.Praga;
using Prism.AppModel;
using CCTPAPP.PlatformServices.Abstract;
using Prism.Services;
using System;

namespace CCTPAPP.ViewModels
{
    public class ReportPageViewModel : ViewModelBase, IPageLifecycleAware
    {
        #region Propiedades

        //var dps = DependencyService.Get<IPragaService>();
        IPragaService dps = DependencyService.Resolve<IPragaService>();
        IPageDialogService _dialogService;
        bool continuarProcesoPraga = true;

        private List<PragaTransactionData> _itSoReporte;
        public List<PragaTransactionData> ItSoReporte
        {
            get { return _itSoReporte; }
            set { SetProperty(ref _itSoReporte, value); }
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
            string apiKey = string.Empty;
            string encryptionKey = string.Empty;
            string userCode = string.Empty;
            int businessID = 0;

            //Se realiza la consulta para obtener las credenciales
            //=>

            //=> El resultado se asigna a las variables
            apiKey = "ZDMxYzc3MGItZjEyMS00OTRhLTkxNmQtYmE5Yjk0M2YzYzlm";
            encryptionKey = "NDQ1MUI0QTJFQkE5RTNENDlFNzk4MUZEMjQ2NEMzNjE=";
            userCode = "1610137579779";
            businessID = 13131;

            Device.BeginInvokeOnMainThread(() =>
            {
                dps.InitController();

                dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);
                dps.GetTransactions(FechaInicio.ToString("dd/MM/yyyy"), FechaFinal.ToString("dd/MM/yyyy"));

            });
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
                ItSoReporte = transactionsData.pragaListTransactionData.listTransaction;
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