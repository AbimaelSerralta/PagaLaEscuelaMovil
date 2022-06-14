using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;
using CCTPAPP.Models.Praga;
using Prism.AppModel;
using CCTPAPP.PlatformServices.Abstract;
using Prism.Services;
using System.Threading.Tasks;

namespace CCTPAPP.ViewModels
{
    public class ScannerPageViewModel : ViewModelBase
    {
        #region Propiedades
        private Result _codigoQr;
        public Result CodigoQr
        {
            get { return _codigoQr; }
            set { SetProperty(ref _codigoQr, value); }
        }

        private bool _isScanning;
        public bool IsScanning
        {
            get { return _isScanning; }
            set { SetProperty(ref _isScanning, value); }
        }

        #endregion

        #region Atributos
        IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public DelegateCommand ResultScannerCommand { get; private set; }
        public Func<Task<byte[]>> SignatureFromStream { get; internal set; }
        #endregion

        #region Constructor
        public ScannerPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "¡Escanee su Codigo!";
            _navigationService = navigationService;
            _dialogService = dialogService;
            IsScanning = true;

            ResultScannerCommand = new DelegateCommand(ExecuteResultScannerCommand);
        }
        #endregion

        #region ICommands


        public void ExecuteResultScannerCommand()
        {
            ResultScanner();
        }

        #endregion

        #region Metodos

        private async void ResultScanner()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                Guid newGuid;

                if (Guid.TryParse(CodigoQr.Text, out newGuid))
                {
                    IsScanning = false;

                    string codigo = CodigoQr.Text;

                    CodigoScanner = codigo;

                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("codeQr", codigo);

                    //await _navigationService.NavigateAsync("PaymentPage", navigationParameters);
                    await _navigationService.GoBackAsync(navigationParameters);
                }
                else
                {
                    IsScanning = false;
                    await _dialogService.DisplayAlertAsync("Formato incorrecto", "El código escaneado no es valído, por favor intente con otro.", "OK");
                }
            });

            IsScanning = true;
        }

        //private async void OpenEscaner()
        //{
        //    ZXingScannerPage scannerPage = new ZXingScannerPage();
        //    scannerPage.Title = "¡Listo para Escanear!";

        //    NavigationPage NPZXingScannerPage = new NavigationPage(new EnterSerial());
        //    Application.Current.MainPage = NPZXingScannerPage;
        //    await NPZXingScannerPage.PushAsync(scannerPage);


        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        await NPZXingScannerPage.PopAsync();

        //        Guid newGuid;

        //        if (Guid.TryParse(result.Text, out newGuid))
        //        {
        //            TxtSerialMovil = result.ToString();
        //        }
        //        else
        //        {
        //            this.GenerateMessage("Serial no valido", "El serial ingresado no es valído, por favor intente con otro.", "Aceptar");
        //        }
        //    });
        //}
        #endregion
    }
}
