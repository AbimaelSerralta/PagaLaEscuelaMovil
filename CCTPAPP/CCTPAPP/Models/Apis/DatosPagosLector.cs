using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.Models.Apis
{
    public class DatosPagosLector
    {
        [JsonProperty(PropertyName = "businessID")]
        public string businessID { get; set; }

        [JsonProperty(PropertyName = "userCode")]
        public string userCode { get; set; }

        [JsonProperty(PropertyName = "encryptionKey")]
        public string encryptionKey { get; set; }

        [JsonProperty(PropertyName = "apiKey")]
        public string apiKey { get; set; }

        [JsonProperty(PropertyName = "reference")]
        public string reference { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public string amount { get; set; }

        [JsonProperty(PropertyName = "concept")]
        public string concept { get; set; }

        [JsonProperty(PropertyName = "emailCustomer")]
        public string emailCustomer { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string message { get; set; }
    }
}
