using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CCTPAPP.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Propiedades

        #endregion

        #region Atributos

        private readonly INavigationService _navigationService;

        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand RecoveryPasswordCommand { get; private set; }
        #endregion

        #region Constructor
        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login";
            _navigationService = navigationService;

            LoginCommand = new DelegateCommand(ExecuteLoginCommand);
            RecoveryPasswordCommand = new DelegateCommand(ExecuteRecoveryPasswordCommand);
        }
        #endregion

        #region ICommands

        private void ExecuteLoginCommand()
        {
            _navigationService.NavigateAsync("NavigationPage/MenuTabbedPage");
        }

        private void ExecuteRecoveryPasswordCommand()
        {
            
        }

        #endregion

        #region Metodos
        private async void Login()
        {

        }

        private async void RecoveryPassword()
        {

        }
        #endregion
    }
}
