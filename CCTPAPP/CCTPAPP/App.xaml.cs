using Prism;
using Prism.Ioc;
using CCTPAPP.ViewModels;
using CCTPAPP.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using CCTPAPP.Views.TabbedPages;
using CCTPAPP.Views.Scanner;
using CCTPAPP.Views.Dialog;

namespace CCTPAPP
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //await NavigationService.NavigateAsync("MainPage");
            await NavigationService.NavigateAsync("LoginPage");
            //await NavigationService.NavigateAsync("TabbedPageMenu?selectedTab=PageReport");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

            containerRegistry.RegisterForNavigation<MenuTabbedPage>();
            containerRegistry.RegisterForNavigation<ReportPage, ReportPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<ReportPage>();
            //containerRegistry.RegisterForNavigation<PageOther>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            
            containerRegistry.RegisterForNavigation<ScannerPage, ScannerPageViewModel>();
            containerRegistry.RegisterForNavigation<PaymentPage, PaymentPageViewModel>();
            containerRegistry.RegisterForNavigation<PaymentStatusPage, PaymentStatusPageViewModel>();
            containerRegistry.RegisterForNavigation<BondedDevicesPage, BondedDevicesPageViewModel>();
            containerRegistry.RegisterDialog<SignVoucherPage, SignVoucherPageViewModel>();

            containerRegistry.RegisterForNavigation<ReaderPage, ReaderPageViewModel>();
        }
    }
}
