using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CCTPAPP.Droid.PlatformServices.Implementation;
using CCTPAPP.Droid.Praga;
using CCTPAPP.PlatformServices.Abstract;
using MX.Com.Mitec.Pragaintegration.Controller;
using MX.Com.Mitec.Pragaintegration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidPragaService))]
namespace CCTPAPP.Droid.PlatformServices.Implementation
{
    public class AndroidPragaService : IPragaService
    {
        #region Properties
        protected readonly Context context = Android.App.Application.Context;

        protected PragaController _pragaController = null;
        #endregion

        public AndroidPragaService()
        {

        }

        #region Implementation
        public void InitController()
        {
            this._pragaController = new PragaController(this.context, new AndroidPragaListener(), PragaEnvironment.Qa);
        }

        public void GeneratePaymentLink()
        {
            // TODO:
            System.Threading.Thread.Sleep(5000);
        }

        public void SetCustomerConfiguration(string apiKey, string encryptionKey, string userCode, int businessID)
        {
            _pragaController.SetCustomerConfiguration(apiKey, encryptionKey, userCode, businessID);
        }
        public void SetDevice(Models.Praga.PragaDeviceReaderData deviceReader)
        {
            _pragaController.SetDevice(PragaDeviceReader.Qpos);
        }
        public void GetBondedDevices()
        {
            _pragaController.GetBondedDevices();
        }
        public void DisconnectDevice()
        {
            _pragaController.DisconnectDevice();
        }
        public void ConnectDevice(string mac)
        {
            _pragaController.ConnectDevice(mac);
        }
        public void DoRetail(string amount, string reference, string tipAmount = null)
        {
            _pragaController.DoRetail(amount, reference, tipAmount);
        }
        public void GetDeviceInfo()
        {
            _pragaController.GetDeviceInfo();
        }
        public void GetTransactions(string initDate, string finalDate)
        {
            _pragaController.GetTransactions(initDate, finalDate);
        }
        public void GetMerchants()
        {
            _pragaController.GetMerchants();
        }
        public void SendPay(string merchant)
        {
            _pragaController.SendPay(merchant);
        }
        public void ScanDevices()
        {
            _pragaController.ScanDevices();
        }
        public void SendVoucher(string email, string folio)
        {
            _pragaController.SendVoucher(email, folio);
        }
        public void SignVoucher(string sign, string folio)
        {
            _pragaController.SignVoucher(sign, folio);
        }

        #endregion
    }
}