using Android.App;
using Android.Content.PM;
using Android.OS;
using CCTPAPP.Droid.Dependencies;
using MX.Com.Mitec.Pragaintegration.Beans;
using MX.Com.Mitec.Pragaintegration.Controller;
using MX.Com.Mitec.Pragaintegration.Enums;
using Prism;
using Prism.Ioc;
using System.Collections.Generic;

namespace CCTPAPP.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] Permission =
            {
            //Android.Manifest.Permission.CallPhone,
            //Android.Manifest.Permission.AccessCoarseLocation,
            //Android.Manifest.Permission.AccessFineLocation,
            Android.Manifest.Permission.Camera
        };

        const int RequestApp = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            RequestPermissions(Permission, RequestApp);

            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();

            #region ejemplo de intancia de SERVICIOS DE PRGA

            PragaController pragaController = new PragaController(this, new PragaServices(), PragaEnvironment.Qa);
            //pragaController.SetCustomerConfiguration("ZDMxYzc3MGItZjEyMS00OTRhLTkxNmQtYmE5Yjk0M2YzYzlm", "NDQ1MUI0QTJFQkE5RTNENDlFNzk4MUZEMjQ2NEMzNjE=", "1610137579779", 13131);
            ////pragaController.SetCustomerConfiguration("YzFlMzNjMWQtYWUxOS00NjVhLThjMjgtN2Y2OTFlYmIyM2Q3", "3D39F9DE50B32AA4C0A4221B3152A950", "1628578238248", 14421);
            //////pragaController.ScanDevices();
            //pragaController.SetDevice(PragaDeviceReader.Qpos);
            //pragaController.GetBondedDevices();
            //pragaController.ConnectDevice("98:27:82:4A:B9:C1");
            //pragaController.GetDeviceInfo();
            #endregion

            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

