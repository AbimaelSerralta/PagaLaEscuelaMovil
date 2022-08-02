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
using Xamarin.Essentials;
using CCTPAPP.Resources;
using CCTPAPP.Models;
using CCTPAPP.Models.Apis;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Prism.Services.Dialogs;

namespace CCTPAPP.ViewModels
{
    public class PaymentPageViewModel : ViewModelBase, IPageLifecycleAware, INavigationAware
    {
        #region Propiedades

        //var dps = DependencyService.Get<IPragaService>();
        IPragaService dps = DependencyService.Resolve<IPragaService>();
        IPageDialogService _dialogService;
        IDialogService _iDialogService;
        AESCryptoPraga aesCryptoPraga = new AESCryptoPraga();

        bool continuarProcesoPraga = true;

        private string _codigoQr;
        public string CodigoQr
        {
            get { return _codigoQr; }
            set { SetProperty(ref _codigoQr, value); }
        }

        private bool _statusDeviceConnected;
        public bool StatusDeviceConnected
        {
            get { return _statusDeviceConnected; }
            set { SetProperty(ref _statusDeviceConnected, value); }
        }

        private string _deviceConnected;
        public string DeviceConnected
        {
            get { return _deviceConnected; }
            set { SetProperty(ref _deviceConnected, value); }
        }

        private string _statusTransaction;
        public string StatusTransaction
        {
            get { return _statusTransaction; }
            set { SetProperty(ref _statusTransaction, value); }
        }

        private string _imgStatusTransaction;
        public string ImgStatusTransaction
        {
            get { return _imgStatusTransaction; }
            set { SetProperty(ref _imgStatusTransaction, value); }
        }

        private bool _isVisibleRetry;
        public bool IsVisibleRetry
        {
            get { return _isVisibleRetry; }
            set { SetProperty(ref _isVisibleRetry, value); }
        }
        private bool _isVisibleRetryPayment;
        public bool IsVisibleRetryPayment
        {
            get { return _isVisibleRetryPayment; }
            set { SetProperty(ref _isVisibleRetryPayment, value); }
        }

        private string _paymentAmount;
        public string PaymentAmount
        {
            get { return _paymentAmount; }
            set { SetProperty(ref _paymentAmount, value); }
        }

        private string _reference;
        public string Reference
        {
            get { return _reference; }
            set { SetProperty(ref _reference, value); }
        }
        private string _concept;
        public string Concept
        {
            get { return _concept; }
            set { SetProperty(ref _concept, value); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return this._isLoading; }

            set { SetProperty(ref this._isLoading, value); }
        }

        bool SeguirProceso = false;
        Guid newGuid;
        string apiKey = string.Empty;
        string encryptionKey = string.Empty;
        string userCode = string.Empty;
        int businessID = 0;
        string Email = "";
        decimal amount = 0;
        string strAmount = "0";
        string macAddress = string.Empty;
        #endregion

        #region Atributos
        private readonly INavigationService _navigationService;

        public DelegateCommand GetPaymentDataCommand { get; private set; }
        public DelegateCommand RetryCommand { get; private set; }
        public DelegateCommand RetryPaymentCommand { get; private set; }
        public DelegateCommand OpenScannerCommand { get; private set; }
        #endregion

        #region Constructor

        public PaymentPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDialogService iDialogService)
            : base(navigationService)
        {
            Title = "Proceso de pago";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _iDialogService = iDialogService;

            DeviceConnected = "Lector no conectado";
            StatusTransaction = "Por favor escaneé su codigo QR o bien escriba la referencia";
            ImgStatusTransaction = "terminalNoConectado";
            IsVisibleRetry = false;
            IsVisibleRetryPayment = false;

            IsLoading = false;

            //_dialogService.DisplayAlertAsync("Mensaje: ", "Por favor escaneé su codigo QR o bien escriba la referencia.", "OK");

            //CreateCommand 
            GetPaymentDataCommand = new DelegateCommand(ExcecuteGetPaymentDataCommand);
            RetryCommand = new DelegateCommand(ExcecuteRetryCommand);
            RetryPaymentCommand = new DelegateCommand(ExcecuteRetryPaymentCommand);
            OpenScannerCommand = new DelegateCommand(ExcecuteOpenScannerCommand);
        }

        #endregion

        #region ICommands

        private void ExcecuteGetPaymentDataCommand()
        {
            GetPaymentData();
        }
        private void ExcecuteRetryCommand()
        {
            continuarProcesoPraga = true;
            DeviceConnected = "Conectando...";
            ImgStatusTransaction = "bluetoothConectando";
            GetPaymentData();
        }
        private void ExcecuteRetryPaymentCommand()
        {
            continuarProcesoPraga = true;
            RetryPayment();
        }
        private void ExcecuteOpenScannerCommand()
        {
            continuarProcesoPraga = true;
            _navigationService.NavigateAsync("ScannerPage");
        }

        #endregion

        #region Metodos

        //private async void GetPaymentData()
        //{
        //    this.IsLoading = true;
        //    ValidateInternet();

        //    if (Guid.TryParse(CodigoQr, out newGuid))
        //    {
        //        string codigo = CodigoQr;

        //        //Se realiza la consulta para obtener datos y las credenciales

        //        //=> El resultado se asigna a las variables

        //        Reference = "pago prueba";
        //        apiKey = "YzFlMzNjMWQtYWUxOS00NjVhLThjMjgtN2Y2OTFlYmIyM2Q3";
        //        encryptionKey = "M0QzOUY5REU1MEIzMkFBNEMwQTQyMjFCMzE1MkE5NTA=";
        //        userCode = "1628578238248";
        //        businessID = int.Parse("14421");
        //        Email = "serralta@compuandsoft.com";
        //        amount = decimal.Parse("50");
        //        Concept = "pago prueba promociones";

        //        PaymentAmount = "$50.00";

        //        SeguirProceso = true;

        //        this.IsLoading = false;
        //    }

        //    if (SeguirProceso)
        //    {
        //        dps.InitController();

        //        dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);

        //        if (continuarProcesoPraga)
        //        {
        //            dps.SetDevice(PragaDeviceReaderData.QPOS);

        //            dps.GetBondedDevices();
        //        }

        //    }
        //}

        private async void GetPaymentData()
        {
            this.IsLoading = true;
            ValidateInternet();

            if (Guid.TryParse(CodigoQr, out newGuid))
            {
                string codigo = CodigoQr;

                //Se realiza la consulta para obtener datos y las credenciales
                ApiServices apiServices = new ApiServices("GetDataPayReader");

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("strResponse", codigo);

                var result = await apiServices.POST<DatosPagosLector>(
                action: "",
                responseType: ApiServices.ResponseType.Object,
                parameters: parameters);

                if (result != null && result.IsSuccess)
                {
                    //=> El resultado se asigna a las variables
                    DatosPagosLector datosPagosLector = new DatosPagosLector();

                    datosPagosLector = (DatosPagosLector)result.Result;

                    Reference = datosPagosLector.reference;
                    apiKey = datosPagosLector.apiKey;
                    encryptionKey = aesCryptoPraga.Base64Encode(datosPagosLector.encryptionKey);
                    userCode = datosPagosLector.userCode;
                    businessID = int.Parse(datosPagosLector.businessID);
                    Email = datosPagosLector.emailCustomer;
                    amount = decimal.Parse(datosPagosLector.amount);
                    strAmount = datosPagosLector.amount;
                    Concept = datosPagosLector.concept;

                    PaymentAmount = "$" + amount.ToString("N2");

                    SeguirProceso = true;

                    this.IsLoading = false;
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Error de conexión", "Lo sentimos, no hemos tenido conexión con el servicio, favor intentarlo más tarde.", "Aceptar");
                    this.IsLoading = false;
                    //this.IsEnabled = true;
                    return;
                }
            }

            if (SeguirProceso)
            {
                dps.InitController();

                dps.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);

                if (continuarProcesoPraga)
                {
                    dps.SetDevice(PragaDeviceReaderData.QPOS);

                    dps.GetBondedDevices();

                    //if (continuarProcesoPraga)
                    //{
                    //    dps.ConnectDevice("98:27:82:4A:B9:C1");

                    //    if (continuarProcesoPraga)
                    //    {
                    //        System.Threading.Thread.Sleep(5000);

                    //        dps.DoRetail(strAmount, Reference);

                    //        ////dps.GetDeviceInfo();
                    //    }
                    //}
                }

            }
        }
        private void RetryPayment()
        {
            if (continuarProcesoPraga)
            {
                dps.SetDevice(PragaDeviceReaderData.QPOS);

                if (continuarProcesoPraga)
                {
                    dps.ConnectDevice(macAddress);

                    if (continuarProcesoPraga)
                    {
                        System.Threading.Thread.Sleep(3000);

                        dps.DoRetail(strAmount, Reference);
                    }
                }
            }
        }
        private void RetryPaymentSelectedDevice()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (continuarProcesoPraga)
                {
                    dps.ConnectDevice(macAddress);

                    if (continuarProcesoPraga)
                    {
                        System.Threading.Thread.Sleep(3000);

                        dps.DoRetail(strAmount, Reference);
                    }
                }
            });
        }

        private async void SendPay(string EncryptedResponse)
        {
            //Se realiza la consulta para obtener datos y las credenciales
            ApiServices apiServices = new ApiServices("ReaderPayCard");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("strResponse", EncryptedResponse);

            var result = await apiServices.POST<DatosPagosLector>(
            action: "",
            responseType: ApiServices.ResponseType.Object,
            parameters: parameters);
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

        private void CloseMerchantsDialogPageCallback(IDialogResult obj)
        {
            if (obj.Parameters.GetValue<PragaMerchants>("merchantSelected") != null)
            {
                PragaMerchants pragaMerchants = obj.Parameters.GetValue<PragaMerchants>("merchantSelected");
                //OnReturnVoucher(returnVoucherData);

                if (pragaMerchants != null)
                {
                    dps.SendPay(pragaMerchants.Value);
                }
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Funcion para capturar eventos "AndroidPragaListener"
        /// </summary>
        /// <param name="data"></param>
        private void OnPragaError(PragaErrorData data)
        {
            if (data.id == "PRAGA_E039")
            {
                ImgStatusTransaction = "cancelado";
                IsVisibleRetryPayment = true;
            }
            else if (data.id == "PRAGA_E007")
            {
                DeviceConnected = "Lector no conectado";
                ImgStatusTransaction = "terminalNoConectado";
                IsVisibleRetry = true;
            }
            else if (data.id == "PRAGA_E004")
            {
                DeviceConnected = "Lector no conectado";
                ImgStatusTransaction = "terminalNoConectado";
                IsVisibleRetry = true;
            }

            _dialogService.DisplayAlertAsync("Codigo: " + data.id, data.description, "OK");

            continuarProcesoPraga = false;
        }
        private void OnReturnDeviceInfo(PragaReaderData pragaReader)
        {
            _dialogService.DisplayAlertAsync(
                "Mnsj: ",
                "Bateria: " + pragaReader.levelBattery +
                "\nVersion: " + pragaReader.firmwareVersion +
                "\nNS: " + pragaReader.serialNumber
                , "OK");
        }
        private void OnReturnStatusTransaction(string statusTransaction)
        {
            if (StatusDeviceConnected)
            {
                StatusTransaction = statusTransaction;
            }
        }
        private void OnDeviceConnected(string deviceConnected)
        {
            StatusDeviceConnected = bool.Parse(deviceConnected);

            if (StatusDeviceConnected)
            {
                DeviceConnected = "Lector conectado";
                ImgStatusTransaction = "conectarTerminal";
                IsVisibleRetry = false;
                IsVisibleRetryPayment = false;
            }
            else
            {
                DeviceConnected = "Lector no conectado";
                ImgStatusTransaction = "terminalNoConectado";
                IsVisibleRetry = true;
                IsVisibleRetryPayment = false;
            }
        }
        private void OnReturnCardInformation(PragaCardInformationData pragaCardInformationData)
        {
            //_dialogService.DisplayAlertAsync(
            //"OnReturnCardInformation: ",
            //"CHN: " + pragaCardInformationData.cardHolderName +
            //"\nMP: " + pragaCardInformationData.maskedPan +
            //"\nCE: " + pragaCardInformationData.cardExpiration +
            //"\nAId: " + pragaCardInformationData.appIdLabel +
            //"\nRT: " + pragaCardInformationData.readingType
            //, "OK");

            //System.Threading.Thread.Sleep(3000);

            dps.GetMerchants();
        }
        private void OnReturnTransactionResult(PragaTransactionData pragaTransactionData)
        {
            string response = "error";

            if (pragaTransactionData.isApproved)
            {
                response = "approved";
            }
            else if(pragaTransactionData.isCancel)
            {
                response = "denied";
            }
            else if(pragaTransactionData.isReversal)
            {
                response = "error";
            }


            Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        pragaTransactionData.response = response;

                        string json = JsonConvert.SerializeObject(pragaTransactionData);
                        string encryptedString = aesCryptoPraga.encrypt(json, aesCryptoPraga.DecodeBase64ToString(encryptionKey));

                        SendPay(encryptedString);
                    });
                });

            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("apiKey", apiKey);
            navigationParameters.Add("encryptionKey", encryptionKey);
            navigationParameters.Add("userCode", userCode);
            navigationParameters.Add("businessID", businessID);
            navigationParameters.Add("PragaTransactionData", pragaTransactionData);
            navigationParameters.Add("Concept", Concept);
            navigationParameters.Add("Email", Email);

            _navigationService.NavigateAsync("PaymentStatusPage", navigationParameters);

            dps.DisconnectDevice();

            //_dialogService.DisplayAlertAsync(
            //"OnReturnTransactionResult: ",
            //"Refe: " + pragaTransactionData.reference +
            //"\nAmount: " + pragaTransactionData.amount +
            //"\nTarjeta: " + pragaTransactionData.cardMasked +
            //"\nError: " + pragaTransactionData.errorCode +
            //"\nMnsj: " + pragaTransactionData.errorDescription +
            //"\nEstatus: " + pragaTransactionData.status +
            //"\nPago: " + pragaTransactionData.response
            //, "OK");
        }
        private void OnReturnBondedDevices(List<PragaBondedDevicesData> lsPragaBondedDevicesDatas)
        {
            if (lsPragaBondedDevicesDatas.Count >= 2)
            {
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add("apiKey", apiKey);
                navigationParameters.Add("encryptionKey", encryptionKey);
                navigationParameters.Add("userCode", userCode);
                navigationParameters.Add("businessID", businessID);

                _navigationService.NavigateAsync("BondedDevicesPage", navigationParameters);
            }
            else
            {
                macAddress = lsPragaBondedDevicesDatas[0].address;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (continuarProcesoPraga)
                    {
                        dps.ConnectDevice(macAddress);

                        if (continuarProcesoPraga)
                        {
                            System.Threading.Thread.Sleep(3000);

                            dps.DoRetail(strAmount, Reference);
                        }
                    }
                });
            }
        }
        private void OnReturnMerchants(List<PragaMerchants> lsPragaMerchants)
        {
            //var navigationParameters = new NavigationParameters();
            //navigationParameters.Add("lsPragaMerchants", lsPragaMerchants);
            //_navigationService.NavigateAsync("MerchantsPage", navigationParameters);

            _iDialogService.ShowDialog("MerchantsDialogPage", new DialogParameters
                        {
                            {"lsPragaMerchants", lsPragaMerchants }
                        }, CloseMerchantsDialogPageCallback);
        }
        #endregion

        #region Page Lifecycle

        public void OnAppearing()
        {
            // Subscribe to Event
            MessagingCenter.Subscribe<PragaErrorData>(this, "OnPragaError", this.OnPragaError);
            MessagingCenter.Subscribe<string>(this, "OnDeviceConnected", this.OnDeviceConnected);
            MessagingCenter.Subscribe<PragaReaderData>(this, "OnReturnDeviceInfo", this.OnReturnDeviceInfo);
            MessagingCenter.Subscribe<string>(this, "OnReturnStatusTransaction", this.OnReturnStatusTransaction);
            MessagingCenter.Subscribe<PragaCardInformationData>(this, "OnReturnCardInformation", this.OnReturnCardInformation);
            MessagingCenter.Subscribe<PragaTransactionData>(this, "OnReturnTransactionResult", this.OnReturnTransactionResult);
            MessagingCenter.Subscribe<List<PragaBondedDevicesData>>(this, "OnReturnBondedDevices", this.OnReturnBondedDevices);
            MessagingCenter.Subscribe<List<PragaMerchants>>(this, "OnReturnMerchants", this.OnReturnMerchants);
        }

        public void OnDisappearing()
        {
            // Eliminar referencia al evento
            MessagingCenter.Unsubscribe<PragaErrorData>(this, "OnPragaError");
            MessagingCenter.Unsubscribe<string>(this, "OnDeviceConnected");
            MessagingCenter.Unsubscribe<PragaReaderData>(this, "OnReturnDeviceInfo");
            MessagingCenter.Unsubscribe<string>(this, "OnReturnStatusTransaction");
            MessagingCenter.Unsubscribe<PragaCardInformationData>(this, "OnReturnCardInformation");
            MessagingCenter.Unsubscribe<PragaTransactionData>(this, "OnReturnTransactionResult");
            MessagingCenter.Unsubscribe<List<PragaBondedDevicesData>>(this, "OnReturnBondedDevices");
            MessagingCenter.Unsubscribe<List<PragaMerchants>>(this, "OnReturnMerchants");
        }

        #endregion

        #region INavigationAware

        override
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.GetValue<string>("codeQr")))
            {
                CodigoQr = parameters.GetValue<string>("codeQr");
                DeviceConnected = "Conectando...";
                StatusTransaction = "";
                ImgStatusTransaction = "bluetoothConectando";
                IsVisibleRetry = false;

                GetPaymentData();
            }

            if (!string.IsNullOrEmpty(parameters.GetValue<string>("macAddress")))
            {
                macAddress = parameters.GetValue<string>("macAddress");
                DeviceConnected = "Conectando...";
                StatusTransaction = "";
                ImgStatusTransaction = "bluetoothConectando";
                IsVisibleRetry = false;

                continuarProcesoPraga = true;
                RetryPaymentSelectedDevice();
            }

            //if (parameters.GetValue<PragaMerchants>("merchantSelected") != null)
            //{
            //    PragaMerchants pragaMerchants = parameters.GetValue<PragaMerchants>("merchantSelected");

            //    dps.SendPay(pragaMerchants.Value);
            //}
        }

        override
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        #endregion
    }
}
