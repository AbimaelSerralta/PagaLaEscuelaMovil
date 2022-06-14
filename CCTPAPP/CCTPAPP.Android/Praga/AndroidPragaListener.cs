using CCTPAPP.Models.Praga;
using Java.Interop;
using MX.Com.Mitec.Pragaintegration.Beans;
using MX.Com.Mitec.Pragaintegration.Controller;
using MX.Com.Mitec.Pragaintegration.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CCTPAPP.Droid.Praga
{
    /// <summary>
    /// Implementación de  interfaz <see cref="IPragaListener"/>
    /// </summary>
    public class AndroidPragaListener : Java.Lang.Object, IPragaListener
    {
        public void OnDeviceConnected(bool p0)
        {
            // Notify
            MessagingCenter.Send<string>(p0.ToString(), "OnDeviceConnected");
        }

        public void OnDoLoginResult(PragaHelperLogin p0, PragaError p1)
        {

        }

        public void OnPragaError(PragaError p0)
        {
            // Notify
            MessagingCenter.Send<PragaErrorData>(new PragaErrorData()
            {
                id = p0.Id,
                description = p0.Description
            }, "OnPragaError");
        }

        public void OnRequestCVVAmex()
        {

        }

        public void OnReturnBondedDevices(IList<PragaBondedDevices> p0)
        {
            List<PragaBondedDevicesData> lsPragaBondedDevicesDatas = new List<PragaBondedDevicesData>();

            foreach (var item in p0)
            {
                lsPragaBondedDevicesDatas.Add(new PragaBondedDevicesData()
                {
                    name = item.Name,
                    address = item.Address,
                    bonded = item.Bonded
                });
            }

            MessagingCenter.Send<List<PragaBondedDevicesData>>(lsPragaBondedDevicesDatas, "OnReturnBondedDevices");
        }

        public void OnReturnCancelTransaction(PragaTransaction p0, PragaError p1)
        {

        }

        public void OnReturnCardInformation(PragaCardInformation p0)
        {
            // Notify
            MessagingCenter.Send<PragaCardInformationData>(new PragaCardInformationData()
            {
                cardHolderName = p0.CardHolderName,
                maskedPan = p0.MaskedPan,
                cardExpiration = p0.CardExpiration,
                appIdLabel = p0.AppIdLabel,
                readingType = p0.ReadingType.ToString()
            }, "OnReturnCardInformation");
        }

        public void OnReturnDeviceInfo(PragaReader p0)
        {
            // Notify
            MessagingCenter.Send<PragaReaderData>(new PragaReaderData
            {
                bootLoaderVersion = p0.BootLoaderVersion,
                firmwareVersion = p0.FirmwareVersion,
                hardwareVersion = p0.HardwareVersion,
                levelBattery = p0.LevelBattery,
                serialNumber = p0.SerialNumber,
                modelDevice = p0.ModelDevice,
                markDevice = p0.MarkDevice,
                isContactless = p0.Contactless,
                isOncharging = p0.OnCharging
            }, "OnReturnDeviceInfo");
        }

        public void OnReturnEMVApplications(IList<string> p0)
        {

        }

        public void OnReturnFirmwareProcessResult(int p0, ProcessStatus p1)
        {

        }

        public void OnReturnMerchants(IList<string> p0)
        {

        }

        public void OnReturnScannedDevices(ScanProcess p0, IList<PragaBondedDevices> p1)
        {

        }

        public void OnReturnShutdownTimeResult(bool p0)
        {

        }

        public void OnReturnSingVoucher(bool p0, PragaError p1)
        {
            PragaErrorData pragaErrorData = new PragaErrorData();

            if (p1 != null)
            {
                pragaErrorData = new PragaErrorData()
                {
                    id = p1.Id,
                    description = p1.Description
                };
            }

            // Notify
            MessagingCenter.Send<ReturnVoucherData>(
                new ReturnVoucherData()
                {
                    sent = p0,
                    pragaErrorData = pragaErrorData
                }
                , "OnReturnSingVoucher");
        }

        public void OnReturnSleepTimeResult(bool p0)
        {

        }

        public void OnReturnStatusTransaction(string p0)
        {
            // Notify
            MessagingCenter.Send<string>(p0, "OnReturnStatusTransaction");
        }

        public void OnReturnTransactionResult(PragaTransaction p0)
        {
            // Notify
            MessagingCenter.Send<PragaTransactionData>(new PragaTransactionData()
            {
                pragaTypeTransactionData = p0.PragaTypeTransaction.ToString(),
                reference = p0.Reference,
                folio = p0.Folio,
                auth = p0.Auth,
                amount = p0.Amount,
                errorCode = p0.ErrorCode,
                errorDescription = p0.ErrorDescription,
                time = p0.Time,
                date = p0.Date,
                company = p0.Company,
                merchantName = p0.MerchantName,
                address = p0.Address,
                ccType = p0.CcType,
                operationType = p0.OperationType,
                ccName = p0.CcName,
                ccNumber = p0.CcNumber,
                ccExpMonth = p0.CcExpMonth,
                ccExpYear = p0.CcExpYear,
                cardBank = p0.CardBank,
                //ccMark = p0.ccMark
                idPragaTransaction = p0.IdTransaction,
                businessID = p0.BusinessID,
                memberShip = p0.MemberShip,
                currency = p0.Currency,
                cardMasked = p0.CardMasked,
                serialNumber = p0.SerialNumber,
                status = p0.Status,
                url = p0.Url,
                product = p0.Product,
                response = p0.Response,
                QPS = p0.QPS,
                chipPin = p0.ChipPin

            }, "OnReturnTransactionResult");
        }

        public void OnReturnTransactions(PragaListTransaction p0, PragaError p1)
        {
            PragaListTransactionData pragaListTransactionData = new PragaListTransactionData();
            PragaErrorData pragaErrorData = new PragaErrorData();
            List<PragaTransactionData> listTransactionData = new List<PragaTransactionData>();

            if (p0 != null)
            {
                foreach (var item in p0.LisTransaction)
                {
                    string FrameBackgroundColor = "";

                    if (item.Response == "APROBADA")
                    {
                        FrameBackgroundColor = "#b0f2c2";
                    }
                    else if (item.Response == "denegado")
                    {
                        FrameBackgroundColor = "#f17a0a";
                    }
                    else if (item.Response == "error")
                    {
                        FrameBackgroundColor = "#ffb6ae";
                    }

                    listTransactionData.Add(new PragaTransactionData()
                    {
                        //pragaTypeTransactionData = item.PragaTypeTransaction.,
                        reference = item.Reference,
                        folio = item.Folio,
                        auth = item.Auth,
                        amount = decimal.Parse(item.Amount).ToString("N2"),
                        errorCode = item.ErrorCode,
                        errorDescription = item.ErrorDescription,
                        time = item.Time,
                        date = item.Date,
                        company = item.Company,
                        merchantName = item.MerchantName,
                        address = item.Address,
                        ccType = item.CcType,
                        operationType = item.OperationType,
                        ccName = item.CcName,
                        ccNumber = item.CcNumber,
                        ccExpMonth = item.CcExpMonth,
                        ccExpYear = item.CcExpYear,
                        cardBank = item.CardBank,
                        //ccMark = item.cc
                        idPragaTransaction = item.IdTransaction,
                        businessID = item.BusinessID,
                        memberShip = item.MemberShip,
                        currency = item.Currency,
                        cardMasked = item.CardMasked,
                        serialNumber = item.SerialNumber,
                        status = item.Status,
                        url = item.Url,
                        product = item.Product,
                        response = item.Response,
                        QPS = item.QPS,
                        chipPin = item.ChipPin,
                        frameBackgroundColor = FrameBackgroundColor
                    });
                }

                pragaListTransactionData = new PragaListTransactionData()
                {
                    code = p0.Code,
                    message = p0.Message,
                    salesCounter = p0.SalesCounter,
                    sales = p0.Sales,
                    listTransaction = listTransactionData
                };
            }

            if (p1 != null)
            {
                pragaErrorData = new PragaErrorData()
                {
                    id = string.IsNullOrEmpty(p1.Id) ? "" : p1.Id,
                    description = string.IsNullOrEmpty(p1.Description) ? "" : p1.Description
                };
            }

            // Notify
            MessagingCenter.Send<TransactionsData>(
                new TransactionsData()
                {
                    pragaListTransactionData = pragaListTransactionData,
                    pragaErrorData = pragaErrorData
                },
                "OnReturnTransactions");
        }

        public void OnReturnUrlPayment(string p0, PragaError p1)
        {

        }

        public void OnReturnVoucher(bool p0, PragaError p1)
        {
            PragaErrorData pragaErrorData = new PragaErrorData();

            if (p1 != null)
            {
                pragaErrorData = new PragaErrorData()
                {
                    id = p1.Id,
                    description = p1.Description
                };
            }

            // Notify
            MessagingCenter.Send<ReturnVoucherData>(
                new ReturnVoucherData()
                {
                    sent = p0,
                    pragaErrorData = pragaErrorData
                }
                , "onReturnVoucher");
        }
    }
}