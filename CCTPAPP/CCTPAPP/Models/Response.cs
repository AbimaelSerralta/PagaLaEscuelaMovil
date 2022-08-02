using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.Models
{
    public class Response
    {
        [JsonProperty(PropertyName = "IsSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "Result")]
        public object Result { get; set; }

        [JsonProperty(PropertyName = "IsSuccessValue")]
        public bool IsSuccessValue { get; set; }
    }
}
