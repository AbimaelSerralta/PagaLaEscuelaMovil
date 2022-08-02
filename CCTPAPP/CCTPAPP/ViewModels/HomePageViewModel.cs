using CCTPAPP.Models.Praga;
using CCTPAPP.PlatformServices.Abstract;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CCTPAPP.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        #region Propiedades

        #endregion

        #region Atributos
        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;

        public DelegateCommand PaymentPageCommand { get; private set; }
        public DelegateCommand TransactionsPageCommand { get; private set; }
        #endregion

        #region Constructor
        public HomePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Inicio";
            _navigationService = navigationService;

            PaymentPageCommand = new DelegateCommand(ExecutePaymentPageCommand);
            TransactionsPageCommand = new DelegateCommand(ExecuteTransactionsPageCommand);
        }
        #endregion

        #region ICommands
        async void ExecutePaymentPageCommand()
        {
            await _navigationService.NavigateAsync("PaymentPage");
        }
        async void ExecuteTransactionsPageCommand()
        {
            await _navigationService.NavigateAsync("TransactionsPage");
        }

        #endregion

        #region Metodos

        #endregion
    }
}
