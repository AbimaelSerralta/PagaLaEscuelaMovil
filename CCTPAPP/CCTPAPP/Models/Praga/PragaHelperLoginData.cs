using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.Models.Praga
{
    public class PragaHelperLoginData
    {
        public string user { get; set; }
        public string password { get; set; }
        public string id { get; set; }
        public string taxtType { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string mothersName { get; set; }
        public string commercialName { get; set; }
        public bool cellPhone { get; set; }
        public string rfc { get; set; }
        public string email { get; set; }
        public List<PragaPaymentTypesData> paymentTypes  { get; set; }
    }
}
