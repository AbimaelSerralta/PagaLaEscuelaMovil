using CCTPAPP.Models.Praga;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace CCTPAPP.ViewModels
{
    public class TransactionsPageViewModel : ViewModelBase
    {
        #region Propiedades

        IPageDialogService _dialogService;

        private List<PragaTransactionData> _itSourceTransactions;
        public List<PragaTransactionData> ItSourceTransactions
        {
            get { return _itSourceTransactions; }
            set { SetProperty(ref _itSourceTransactions, value); }
        }

        private PragaTransactionData _itemSelectedTransaction { get; set; }
        public PragaTransactionData ItemSelectedTransaction
        {
            get { return _itemSelectedTransaction; }
            set
            {
                if (_itemSelectedTransaction != value)
                {
                    _itemSelectedTransaction = value;
                    GetTransaction();
                }
            }
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

        public TransactionsPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Transacciones Erroneas";
            _navigationService = navigationService;
            _dialogService = dialogService;

            IsPullToRefreshEnabled = true;
            IsLoading = false;

            // Subscribe to Event
            

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


            List<PragaTransactionData> lsPragaTransactionData = await App.PragaTransactionDataService.GetTransactionsDatasAsync();

            if (lsPragaTransactionData.Count >= 1)
            {
                ItSourceTransactions = lsPragaTransactionData;

                this.IsLoading = false;
            }
            else
            {
                this.IsLoading = false;

                return;
            }
        }
        private async void GetTransaction()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("apiKey", "dd");
            navigationParameters.Add("encryptionKey", "dd");
            navigationParameters.Add("userCode", "dd");
            navigationParameters.Add("businessID", "1");
            navigationParameters.Add("PragaTransactionData", ItemSelectedTransaction);
            navigationParameters.Add("Concept", "ihsiu shiu shius");
            navigationParameters.Add("Email", "");

            await _navigationService.NavigateAsync("TransactionsStatusPage", navigationParameters);
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
    }
}
