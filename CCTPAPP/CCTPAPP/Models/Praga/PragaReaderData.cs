using System;
using System.Collections.Generic;
using System.Text;

namespace CCTPAPP.Models.Praga
{
    public class PragaReaderData
    {
        public string bootLoaderVersion { get; set; }
        public string firmwareVersion { get; set; }
        public string hardwareVersion { get; set; }
        public string levelBattery { get; set; }
        public string serialNumber { get; set; }
        public string modelDevice { get; set; }
        public string markDevice { get; set; }
        public bool isContactless { get; set; }
        public bool isOncharging { get; set; }
    }
}
