namespace ScanflowMaui;

public partial class App : Application
{
    //public static double ScreenWidth { get; set; }
    //public static double ScreenHeight { get; set; }
    public App()
	{
        //var size = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo;
        InitializeComponent();


        if (Preferences.Default.ContainsKey("IsLandingPage"))
        {
           RequestCameraPermission();
            MainPage = new NavigationPage(new HomePage());
        }
        else
            MainPage = new NavigationPage(new WelcomePage());
       // ResourceSource();

    }

    //private void ResourceSource()
    //{
    //    App.Current.Resources["ProgressHeight"] = PercentageWithScreen(2);
    //    App.Current.Resources["ProgressPadding"] = new Thickness(0, PercentageWithScreen(5), 0, 0);
    //    App.Current.Resources["ProgressPaddingimg"] = new Thickness(PercentageWithScreen(1), PercentageWithScreen(1), PercentageWithScreen(1), PercentageWithScreen(1));
    //    App.Current.Resources["ProgressHeightFrame"] = PercentageWithScreen(20);
    //    App.Current.Resources["ProgressHeightFrameCicle"] = PercentageWithScreen(8);
    //    App.Current.Resources["ProgressWithFrameCicle"] = PercentageWithScreen(8);
    //    App.Current.Resources["ProgressPaddingText"] = new Thickness(PercentageWithScreen(3), 0, 0, 0);
    //}
    //public static double PercentageWithScreen(double value)
    //{
    //    var result = (ScreenWidth * value) / 100;
    //    return result;
    //}
    //public static double PercentageWithScreenHeight(double value)
    //{
    //    var result = (ScreenHeight * value) / 100;
    //    return result;
    //}
    private async void RequestCameraPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            await Permissions.RequestAsync<Permissions.Camera>();
        }

    }
    protected override void OnResume()
    {

      //  MainPage = new NavigationPage(new HomePage());
    }
}
