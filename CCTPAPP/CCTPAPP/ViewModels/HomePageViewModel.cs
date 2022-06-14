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
       
        #endregion

        #region Constructor
        public HomePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Inicio";
            _navigationService = navigationService;
        }
        #endregion

        #region ICommands

        public DelegateCommand PaymentPageCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecutePaymentPageCommand));
        async void ExecutePaymentPageCommand()
        {
            await _navigationService.NavigateAsync("PaymentPage");
        }

        #endregion

        #region Metodos

        #endregion
    }
}
