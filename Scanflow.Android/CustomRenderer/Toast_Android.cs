using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Scanflow.Droid.CustomRenderer;
using Scanflow.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]
namespace Scanflow.Droid.CustomRenderer
{
    public class Toast_Android : Interface.IToast
    {
       

        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}