using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanflow.Droid.Activities
{
    [Activity(Label = "Scanflow", Theme = "@style/SplashTheme", NoHistory = true, MainLauncher = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SplashScreen);
            System.Threading.ThreadPool.QueueUserWorkItem(o => LoadActivity());
        }
        private void LoadActivity()
        {
            /* Simulate a long pause    */
            System.Threading.Thread.Sleep(2000);
            RunOnUiThread(() => StartActivity(typeof(MainActivity)));
        }
    }
}