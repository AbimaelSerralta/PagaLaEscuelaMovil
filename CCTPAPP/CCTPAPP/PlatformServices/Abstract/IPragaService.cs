using CCTPAPP.Models.Praga;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.PlatformServices.Abstract
{
    /// <summary>
    /// Abstracción de funciones para la implementación de la libreria de Praga
    /// </summary>
    /// <remarks>
    /// La librería de PRAGA se implementa de forma nativa en cada plataforma
    /// </remarks>
    public interface IPragaService
    {
        /// <summary>
        /// Inicializar PragaController
        /// </summary>
        void InitController();

        /// <summary>
        /// Generar liga de pago
        /// </summary>
        void GeneratePaymentLink();

        void SetCustomerConfiguration(string apiKey, string encryptionKey, string userCode, int businessID);
        void SetDevice(PragaDeviceReaderData deviceReader);
        void GetBondedDevices();
        void DisconnectDevice();
        void ConnectDevice(string mac);
        void DoRetail(string amount, string reference, string tipAmount = null);
        void GetDeviceInfo();
        void GetTransactions(string initDate, string finalDate);
        void GetMerchants();
        void SendPay(string merchant);
        void ScanDevices();
        void SendVoucher(string email, string folio);
        void SignVoucher(string sign, string folio);
    }
}
