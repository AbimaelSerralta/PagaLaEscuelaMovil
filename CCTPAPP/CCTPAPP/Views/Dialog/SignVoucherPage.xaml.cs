using CCTPAPP.ViewModels;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCTPAPP.Views.Dialog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignVoucherPage : StackLayout
    {
        public SignVoucherPage()
        {
            InitializeComponent();
        }
        private async void SendVoucher_Clicked(object sender, EventArgs e)
        {
            try
            {
                var image = await SignatureView.GetImageStreamAsync(SignatureImageFormat.Png);
                if (image != null)
                {
                    var nStream = (MemoryStream)image;
                    byte[] data = nStream.ToArray();
                    string base64Val = Convert.ToBase64String(data);
                    string hexaVal = BitConverter.ToString(data).Replace("-", "");

                    // Notify
                    MessagingCenter.Send<string>(hexaVal, "SingData");
                }
                else
                {
                    MessagingCenter.Send<string>("", "SingData");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}