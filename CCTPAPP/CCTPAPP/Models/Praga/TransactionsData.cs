using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.Models.Praga
{
    public class TransactionsData
    {
        [JsonProperty(PropertyName = "pragaListTransactionData")]
        public PragaListTransactionData pragaListTransactionData { get; set; }
        public PragaErrorData pragaErrorData { get; set; }
    }
}
