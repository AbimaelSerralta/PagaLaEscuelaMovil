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
using CCTPAPP.PlatformServices.Sqlite;
using System.IO;
using System;
using CCTPAPP.Views.Transactions;

namespace CCTPAPP
{
    public partial class App
    {
        public static PragaTransactionDataService _pragaTransactionDataService;
        public static PragaTransactionDataService PragaTransactionDataService
        {
            get
            {
                if (_pragaTransactionDataService == null)
                {
                    _pragaTransactionDataService = new PragaTransactionDataService(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PragaTransactionData.db3"));
                }
                return _pragaTransactionDataService;
            }
        }

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

            //RegisterForNavigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

            containerRegistry.RegisterForNavigation<MenuTabbedPage>();
            
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<ReportPage, ReportPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportVoucherPage, ReportVoucherPageViewModel>();
            //containerRegistry.RegisterForNavigation<PageOther>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            
            containerRegistry.RegisterForNavigation<ScannerPage, ScannerPageViewModel>();
            containerRegistry.RegisterForNavigation<PaymentPage, PaymentPageViewModel>();
            containerRegistry.RegisterForNavigation<PaymentStatusPage, PaymentStatusPageViewModel>();
            containerRegistry.RegisterForNavigation<BondedDevicesPage, BondedDevicesPageViewModel>();
            containerRegistry.RegisterForNavigation<MerchantsPage, MerchantsPageViewModel>();
            containerRegistry.RegisterForNavigation<TransactionsPage, TransactionsPageViewModel>();
            containerRegistry.RegisterForNavigation<TransactionsStatusPage, TransactionsStatusPageViewModel>();

            containerRegistry.RegisterForNavigation<ReaderPage, ReaderPageViewModel>();

            //RegisterDialog
            containerRegistry.RegisterDialog<MerchantsDialogPage, MerchantsDialogPageViewModel>();
            containerRegistry.RegisterDialog<SignVoucherPage, SignVoucherPageViewModel>();
        }
    }
}
