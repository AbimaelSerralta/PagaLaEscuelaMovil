using CCTPAPP.Models.Apis;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.ViewModels
{
    public class ReportVoucherPageViewModel : ViewModelBase, IPageLifecycleAware, INavigatedAware
    {
        #region Propiedades

        IPageDialogService _dialogService;

        private List<TransactionReportReader> _itemsSourceReportVoucher;
        public List<TransactionReportReader> ItemsSourceReportVoucher
        {
            get { return _itemsSourceReportVoucher; }
            set { SetProperty(ref _itemsSourceReportVoucher, value); }
        }

        #endregion


        #region Atributos
        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;

        #endregion

        #region Constructor

        public ReportVoucherPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Reporte Voucher";
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        #endregion

        #region ICommands


        #endregion

        #region Metodos

        private void GetReportPaymet()
        {

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

        #region INavigationAware

        override
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            List<TransactionReportReader> lsTransactionReportReader = new List<TransactionReportReader>();

            TransactionReportReader transactionReportReader = parameters.GetValue<TransactionReportReader>("TransactionReportReader");
            transactionReportReader.singVoucher = "https://qaag.mitec.com.mx/praga-ws/mobilePayment/getSignature/" + transactionReportReader.businessID + "/" + transactionReportReader.folio;

            lsTransactionReportReader.Add(transactionReportReader);

            ItemsSourceReportVoucher = lsTransactionReportReader;
        }

        override
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        #endregion
    }
}
