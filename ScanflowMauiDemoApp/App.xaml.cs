using ScanflowMauiDemoApp.Helpers;
using ScanflowMauiDemoApp.Models;
using ScanflowMauiDemoApp.Views;

namespace ScanflowMauiDemoApp
{
    public partial class App : Application
    {
        private ScanSelect scanSelect { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ContentPage());
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await InitializeApp();
        }

        private async Task InitializeApp()
        {
            if (Preferences.Default.ContainsKey("IsLandingPage"))
            {
                await RequestCameraPermissionAndNavigate(new HomePage());
            }
            else
            {
                SetScanSelect();
                await RequestCameraPermissionAndNavigate(new ScanViewPage(scanSelect));
            }
        }

        private async Task RequestCameraPermissionAndNavigate(Page nextPage)
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
            }

            if (status == PermissionStatus.Granted)
            {
                MainPage = new NavigationPage(nextPage);
            }
            else
            {
                // Handle the case where permission is denied (optional)
                // You can show a message or navigate to an error page
                //MainPage = new NavigationPage(new PermissionDeniedPage());
            }
        }

        private void SetScanSelect()
        {
            scanSelect = new ScanSelect
            {
                Name = ConstantStrings.Any,
                Image = "any"
            };
        }
    }
}
