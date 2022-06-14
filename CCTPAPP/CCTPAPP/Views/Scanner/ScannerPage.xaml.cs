using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCTPAPP.Views.Scanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        public ScannerPage()
        {
            InitializeComponent();
        }

        //private void scanView_OnScanResult(ZXing.Result result)
        //{
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        await DisplayAlert("Resultado", result.Text, "OK");
        //    });
        //}
    }
}