using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.Models.Praga
{
    public class PragaTransactionData
    {
        [PrimaryKey] //Se usa para identificar el pago en Sqlite
        public Guid UidPragaTransactionData { get; set; }

        public string pragaTypeTransactionData { get; set; }
        public string reference { get; set; }
        public string folio { get; set; }
        public string auth { get; set; }
        public string amount { get; set; }
        public string errorCode { get; set; }
        public string errorDescription { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string company { get; set; }
        public string merchantName { get; set; }
        public string address { get; set; }
        public string ccType { get; set; }
        public string operationType { get; set; }
        public string ccName { get; set; }
        public string ccNumber { get; set; }
        public string ccExpMonth { get; set; }
        public string ccExpYear { get; set; }
        public string cardBank { get; set; }
        public string ccMark { get; set; }
        public string idPragaTransaction { get; set; }
        public string businessID { get; set; }
        public bool isApproved { get; set; }
        public bool isCancel { get; set; }
        public bool isReversal { get; set; }
        public string memberShip { get; set; }
        public string currency { get; set; }
        public string cardMasked { get; set; }
        public string serialNumber { get; set; }
        public string status { get; set; }
        public string url { get; set; }
        public string product { get; set; }
        public string response { get; set; }
        public bool isQPS { get; set; }
        public bool isChipPin { get; set; }
        public string frameBackgroundColor { get; set; }
    }
}
