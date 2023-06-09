using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content.Res;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using FFImageLoading;

namespace Scanflow.Droid
{
    [Activity(Label = "Scanflow", Icon = "@drawable/appicon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode , ScreenOrientation=ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            if (Resources.Configuration.UiMode.HasFlag(UiMode.NightYes))
                App.AppTheme = "dark";
            else
                App.AppTheme = "light";

            LoadApplication(new App());
            if (Resources.Configuration.UiMode.HasFlag(UiMode.NightYes))
            {
                App.AppTheme = "Dark";
                App.Current.Resources = new Styles.DarkTheme();
            }
            else
            {
                App.AppTheme = "light";
                App.Current.Resources = new Styles.WhiteTheme();

            }
           
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}