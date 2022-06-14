using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.ViewModels
{
    public class ReaderPageViewModel : BindableBase
    {
        #region Propiedades
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion

        #region Atributos
        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;

        #endregion

        #region Constructor

        public ReaderPageViewModel(INavigationService navigationService)
        {
            Title = "Pago regular";
            _navigationService = navigationService;
        }

        #endregion

        #region ICommands

        public DelegateCommand CardPaymentCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteCardPaymentCommand));
        async void ExecuteCardPaymentCommand()
        {
            await _navigationService.NavigateAsync("");
        }

        #endregion

        #region Metodos

        #endregion
    }
}
