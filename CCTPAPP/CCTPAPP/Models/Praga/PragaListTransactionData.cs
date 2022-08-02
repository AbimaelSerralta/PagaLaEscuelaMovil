using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.Models.Praga
{
    public class PragaListTransactionData
    {
        public string code { get; set; }
        public string message { get; set; }
        public string salesCounter { get; set; }
        public string sales { get; set; }
        [JsonProperty(PropertyName = "listTransaction")]
        public List<PragaTransactionData> listTransaction { get; set; }
    }
}
