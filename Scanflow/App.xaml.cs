using Scanflow.TextCapture.Xamarin.Forms;
using Scanflow.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



[assembly: ExportFont("HelveticaNowDisplay-Bold.ttf")]
[assembly: ExportFont("HelveticaNowDisplay-Regular.ttf")]
namespace Scanflow
{ 
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
         

            if (App.Current.Properties.ContainsKey("IsLandingPage"))
            {
                RequestCameraPermission();
                MainPage = new NavigationPage(new HomePage());
            }
            else
                MainPage = new NavigationPage(new WelcomePage());
;
        }

       
       
        async void RequestCameraPermission()
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    await Permissions.RequestAsync<Permissions.Camera>();
                }
               
            }
       
        protected override void OnStart()
        {
           
        }
       
        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {

            MainPage = new NavigationPage(new HomePage());
        }
public static string AppTheme
        {
            get; set;
        }

        private void Application_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            if (e.RequestedTheme == OSAppTheme.Dark)
            {
                App.AppTheme = "dark";
                App.Current.Resources = new Styles.DarkTheme();
            }

            else
            {
                App.AppTheme = "white";
                App.Current.Resources = new Styles.WhiteTheme();
            }
        }
    }
}
