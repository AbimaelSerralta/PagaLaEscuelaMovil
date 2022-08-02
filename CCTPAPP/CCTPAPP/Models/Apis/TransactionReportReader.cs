using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.Models.Apis
{
    public class TransactionReportReader
    {
        [JsonProperty(PropertyName = "pragaTypeTransactionData")]
        public string pragaTypeTransactionData { get; set; }

        [JsonProperty(PropertyName = "reference")]
        public string reference { get; set; }

        [JsonProperty(PropertyName = "folio")]
        public string folio { get; set; }

        [JsonProperty(PropertyName = "auth")]
        public string auth { get; set; }

        [JsonProperty(PropertyName = "concept")]
        public string concept { get; set; }
        
        [JsonProperty(PropertyName = "amount")]
        public decimal amount { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public string errorCode { get; set; }

        [JsonProperty(PropertyName = "errorDescription")]
        public string errorDescription { get; set; }

        [JsonProperty(PropertyName = "time")]
        public string time { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime date { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string company { get; set; }
        
        [JsonProperty(PropertyName = "bussiness")]
        public string bussiness { get; set; }

        [JsonProperty(PropertyName = "merchantName")]
        public string merchantName { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string address { get; set; }

        [JsonProperty(PropertyName = "ccType")]
        public string ccType { get; set; }

        [JsonProperty(PropertyName = "operationType")]
        public string operationType { get; set; }

        [JsonProperty(PropertyName = "ccName")]
        public string ccName { get; set; }

        [JsonProperty(PropertyName = "ccNumber")]
        public string ccNumber { get; set; }

        [JsonProperty(PropertyName = "ccExpMonth")]
        public string ccExpMonth { get; set; }

        [JsonProperty(PropertyName = "ccExpYear")]
        public string ccExpYear { get; set; }

        [JsonProperty(PropertyName = "cardBank")]
        public string cardBank { get; set; }

        [JsonProperty(PropertyName = "ccMark")]
        public string ccMark { get; set; }

        [JsonProperty(PropertyName = "idPragaTransaction")]
        public string idPragaTransaction { get; set; }

        [JsonProperty(PropertyName = "businessID")]
        public string businessID { get; set; }

        [JsonProperty(PropertyName = "isApproved")]
        public bool isApproved { get; set; }

        [JsonProperty(PropertyName = "isCancel")]
        public bool isCancel { get; set; }

        [JsonProperty(PropertyName = "isReversal")]
        public bool isReversal { get; set; }

        [JsonProperty(PropertyName = "memberShip")]
        public string memberShip { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string currency { get; set; }

        [JsonProperty(PropertyName = "cardMasked")]
        public string cardMasked { get; set; }

        [JsonProperty(PropertyName = "serialNumber")]
        public string serialNumber { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string status { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string url { get; set; }

        [JsonProperty(PropertyName = "product")]
        public string product { get; set; }

        [JsonProperty(PropertyName = "response")]
        public string response { get; set; }

        [JsonProperty(PropertyName = "isQPS")]
        public bool isQPS { get; set; }

        [JsonProperty(PropertyName = "isChipPin")]
        public bool isChipPin { get; set; }
        public string frameBackgroundColor { get; set; }
        public string singVoucher { get; set; }
    }
}
