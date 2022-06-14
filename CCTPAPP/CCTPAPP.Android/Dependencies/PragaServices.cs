using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CCTPAPP.Dependencies;
using Java.Interop;
using MX.Com.Mitec.Pragaintegration.Beans;
using MX.Com.Mitec.Pragaintegration.Controller;
using MX.Com.Mitec.Pragaintegration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCTPAPP.Droid.Dependencies
{
    public class PragaServices : Java.Lang.Object, IPragaListener
    {

        public void OnDeviceConnected(bool p0)
        {
            bool rs = p0;
        }

        public void OnDoLoginResult(PragaHelperLogin p0, PragaError p1)
        {
            string Id = p1.Id;
            string Mnsj = p1.Description;
        }

        public void OnPragaError(PragaError p0)
        {
            string Id = p0.Id;
            string Mnsj = p0.Description;
        }

        public void OnRequestCVVAmex()
        {
            throw new NotImplementedException();
        }

        public void OnReturnBondedDevices(IList<PragaBondedDevices> p0)
        {
            bool bl = false;
            string mac = "";

            foreach (var item in p0)
            {
                bl = item.Bonded;
                mac = item.Address;

            }
        }

        public void OnReturnCancelTransaction(PragaTransaction p0, PragaError p1)
        {
            throw new NotImplementedException();
        }

        public void OnReturnCardInformation(PragaCardInformation p0)
        {
            throw new NotImplementedException();
        }

        public void OnReturnDeviceInfo(PragaReader p0)
        {
            throw new NotImplementedException();
        }

        public void OnReturnEMVApplications(IList<string> p0)
        {
            throw new NotImplementedException();
        }

        public void OnReturnFirmwareProcessResult(int p0, ProcessStatus p1)
        {
            throw new NotImplementedException();
        }

        public void OnReturnMerchants(IList<string> p0)
        {
            throw new NotImplementedException();
        }

        public void OnReturnScannedDevices(ScanProcess p0, IList<PragaBondedDevices> p1)
        {
            throw new NotImplementedException();
        }

        public void OnReturnShutdownTimeResult(bool p0)
        {
            throw new NotImplementedException();
        }

        public void OnReturnSingVoucher(bool p0, PragaError p1)
        {
            throw new NotImplementedException();
        }

        public void OnReturnSleepTimeResult(bool p0)
        {
            throw new NotImplementedException();
        }

        public void OnReturnStatusTransaction(string p0)
        {
            string msnj = "Iniciando transacción...";
        }

        public void OnReturnTransactionResult(PragaTransaction p0)
        {
            throw new NotImplementedException();
        }

        public void OnReturnTransactions(PragaListTransaction p0, PragaError p1)
        {
            throw new NotImplementedException();
        }

        public void OnReturnUrlPayment(string p0, PragaError p1)
        {
            throw new NotImplementedException();
        }

        public void OnReturnVoucher(bool p0, PragaError p1)
        {
            throw new NotImplementedException();
        }
    }
}