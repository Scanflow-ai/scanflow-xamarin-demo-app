using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.Widget;
using Scanflow.Xamarin.Android.Activities;
using Scanflow.Xamarin.Android.Helper;
using System.Drawing;
using static Android.Icu.Text.Transliterator;

namespace Scanflow.Xamarin.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //public static int[] SampleImages = { Resource.Drawable.onboardImage1a, Resource.Drawable.onboardimage_b, Resource.Drawable.onboardimage_c, Resource.Drawable.onboardimage_d };

        ViewPager viewPager;
    
        ImageAdapter imageAdapter;

         ImageView imageView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);

            string userAlreadyLogin = sharedPreferences.GetString("Done", "");
            if (string.IsNullOrEmpty(userAlreadyLogin))
            {
                SetContentView(Resource.Layout.activity_main);

                viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
                imageAdapter = new ImageAdapter(this);
                viewPager.Adapter = imageAdapter;

                imageView = FindViewById<ImageView>(Resource.Id.skip);
                imageView.Click += (sender, e) =>
                {
                    ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);

                    ISharedPreferencesEditor editor = sharedPreferences.Edit();

                    editor.PutString("Done", "Done");

                    editor.Apply();

                    StartActivity(typeof(HomeActivity));
                };

                viewPager.AddOnPageChangeListener(new AddOnPageChangeListener(imageView));
            }
            else
                StartActivity(typeof(HomeActivity));

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private class AddOnPageChangeListener : ViewPager.SimpleOnPageChangeListener
        {
            private ImageView imageView;
            public AddOnPageChangeListener(ImageView imageView)
            {
                this.imageView = imageView;
            }
            public override void OnPageSelected(int position)
            {
               
                switch (position)
                {
                    case 0:
                        imageView.SetImageResource(Resource.Drawable.skip1);
                        break;
                    case 1:
                        imageView.SetImageResource(Resource.Drawable.skip2);
                        break;
                    case 2:
                        imageView.SetImageResource(Resource.Drawable.skip3);
                        break;
                    case 3:
                        imageView.SetImageResource(Resource.Drawable.skip4);
                        break;
                       
                }
            }
        }


    }
   
}