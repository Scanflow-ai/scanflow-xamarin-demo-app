
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;

namespace Scanflow.Xamarin.Activities
{
    //[Activity (Label = "SettingsActivity")]			
    public class SettingsActivity : DialogFragment
    {
		/*protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            SetContentView(Resource.Layout.SettingsLayout);

			var closeButton = this.FindViewById<ImageView>(Resource.Id.ivClose);
            var applyButton = this.FindViewById<TextView>(Resource.Id.btnApply);
            var cancelButton = this.FindViewById<TextView>(Resource.Id.btnApply);
            closeButton.Click += CloseButton_Click;
            applyButton.Click += ApplyButton_Click;
            cancelButton.Click += CloseButton_Click;
        }*/

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

            // Inflate your popup layout here
            View view = inflater.Inflate(Resource.Layout.SettingsLayout, container, false);

            var closeButton = view.FindViewById<ImageView>(Resource.Id.ivClose);
            var applyButton = view.FindViewById<TextView>(Resource.Id.btnApply);
            var cancelButton = view.FindViewById<TextView>(Resource.Id.btnApply);
            var AutoFlashLight = view.FindViewById<CheckBox>(Resource.Id.cbAutoFlashLight);
            var AutoExposure = view.FindViewById<CheckBox>(Resource.Id.cbAutoExposure);
            var OneTouchZoom = view.FindViewById<CheckBox>(Resource.Id.rbOneTouchZoom);
            var AutoZoom = view.FindViewById<CheckBox>(Resource.Id.rbAutoZoom);
            var ZoomNone = view.FindViewById<CheckBox>(Resource.Id.rbZoomNone);
            AutoFlashLight.Click += AutoFlashLight_Click;
            AutoExposure.Click += AutoExposure_Click;
            closeButton.Click += CloseButton_Click;
            applyButton.Click += ApplyButton_Click;
            cancelButton.Click += CloseButton_Click;
            OneTouchZoom.Click += OneTouchZoom_Click;
            AutoZoom.Click += AutoZoom_Click;
            ZoomNone.Click += ZoomNone_Click;
            return view;
        }

        private void ZoomNone_Click(object sender, EventArgs e)
        {
            
        }

        private void AutoZoom_Click(object sender, EventArgs e)
        {
            
        }

        private void OneTouchZoom_Click(object sender, EventArgs e)
        {
            
        }


        private void AutoExposure_Click(object sender, EventArgs e)
        {
            
        }

        private void AutoFlashLight_Click(object sender, EventArgs e)
        {
           
        }
    }
}

