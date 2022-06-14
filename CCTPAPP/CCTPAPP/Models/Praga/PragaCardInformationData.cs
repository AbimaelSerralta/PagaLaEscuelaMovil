using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.Models.Praga
{
    public class PragaCardInformationData
    {
        public string cardHolderName { get; set; }
        public string maskedPan { get; set; }
        public string cardExpiration { get; set; }
        public string appIdLabel { get; set; }
        public string readingType { get; set; }
    }
}
