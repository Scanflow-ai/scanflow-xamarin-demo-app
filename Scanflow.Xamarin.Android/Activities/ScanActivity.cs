using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.Fragment.App;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Com.Scanflow.Datacapture.Barcode;
using Com.Scanflow.Datacapture.Barcode.Config;
using Com.Scanflow.Datacapture.Core.Camera;
using Com.Scanflow.Datacapture.Model;
using Com.Scanflow.Datacapture.Sfbarcode;
using Google.Android.Material.BottomSheet;
using Java.Util;
using Scanflow.Xamarin.Activities;
using Scanflow.Xamarin.Android;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using Xamarin.Essentials;
using Context = Android.Content.Context;
using Timer = System.Timers.Timer;
using Android.Support.V4.App;

namespace Scanflow.Xamarin
{
    [Activity(Label = "ScanActivity")]
    public class ScanActivity : AppCompatActivity, ScanflowReader.IOnBarcodeScanResultCallback
    {
        public ScanflowReader mBarcodeReader;
        public DecodeConfig decodeConfig;
        private Timer timer;
        TextView tvResult;
        BottomSheetBehavior bottomSheetBehavior;
        View bottomSheet;
        public ImageView flashBtn;
        public bool Isflash = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                SetContentView(Resource.Layout.BarcodeScanLayout);
                var preview = this.FindViewById<SFCameraView>(Resource.Id.scanflowView);
                var title = this.FindViewById<TextView>(Resource.Id.textView);
                flashBtn = this.FindViewById<ImageView>(Resource.Id.ivFlashLight);
                var backBtn = this.FindViewById<ImageView>(Resource.Id.ivBack);
                //var settingsBtn = this.FindViewById<ImageView>(Resource.Id.ivSettings);
                tvResult = this.FindViewById<TextView>(Resource.Id.tvResult);
                var copyBtn = this.FindViewById<ImageView>(Resource.Id.ivCopyResult);
                bottomSheetBehavior = BottomSheetBehavior.From(this.FindViewById<View>(Resource.Id.bottomSheet));
                bottomSheet = this.FindViewById<View>(Resource.Id.bottomSheet);
                bottomSheet.Click += BottomSheet_Click;
                flashBtn.Click += FlashBtn_Click;
                copyBtn.Click += CopyBtn_Click;
                backBtn.Click += BackBtn_Click;
                //settingsBtn.Click += SettingsBtn_Click;
                flashBtn.SetImageResource(Resource.Drawable.ic_flash_disable);
                if (Intent.HasExtra("ScanType"))
                {
                    string ScanType = Intent.GetStringExtra("ScanType");
                    if (ScanType == "QR CODE")
                    {
                        decodeConfig = DecodeConfig.Qrcode;
                        title.Text = "QR CODE";
                    }
                    else if (ScanType == "Barcode")
                    {
                        decodeConfig = DecodeConfig.Barcode;
                        title.Text = "Barcode";
                    }
                    else
                    {
                        decodeConfig = DecodeConfig.Any;
                        title.Text = "Any";
                    }

                    //Note : Install the latest version of Xamarin.AndroidX.Camera.View NuGet Package to avoid GetInstance errors
                    
                    mBarcodeReader = SFBarcodeCaptureSession.Instance.CreateScanSession(this, "Your License Key", preview, decodeConfig, 0.4f);
                    mBarcodeReader?.SetOnBarcodeScanResultCallback(this); // Result Call Back
                    mBarcodeReader?.SetEnableLocationTracking(false);
                }

               // Note:  Check Camera permission is allowed before starting a camera.
                if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Permission.Granted)
                {
                    mBarcodeReader?.StartCamera();
                }
            }
            catch(Exception Ex)
            {
                Toast.MakeText(this, Ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            var dialogFragment = new SettingsActivity();
            dialogFragment.Show(transaction, "my_popup");
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void FlashBtn_Click(object sender, System.EventArgs e)
        {
            if (Isflash)
            {
                Isflash = false;
                flashBtn.SetImageResource(Resource.Drawable.ic_flash_disable);
                mBarcodeReader.EnableTorch(false);
            }
            else
            {
                Isflash = true;
                flashBtn.SetImageResource(Resource.Drawable.ic_flash_enable);
                mBarcodeReader.EnableTorch(true);
            }
        }

        private void BottomSheet_Click(object sender, System.EventArgs e)
        {
            this.RunOnUiThread(() =>
            {
                bottomSheetBehavior.State = BottomSheetBehavior.StateCollapsed;
                bottomSheet.Visibility = ViewStates.Gone;
            });
        }


        private void CopyBtn_Click(object sender, EventArgs e)
        {
            ClipboardManager clipboardManager = (ClipboardManager)GetSystemService(Context.ClipboardService);
            ClipData clip = ClipData.NewPlainText("CopiedText", tvResult.Text);
            clipboardManager.PrimaryClip = clip;

            Toast.MakeText(this, "Text copied", ToastLength.Short).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }



        #region [Scan Results]

        public void OnBatchScanResultSuccess(IList<ScanResultSuccess> result) { }
       
        public void OnOneOfManyCodeResult(HashSet results) { }
        
        public void OnOneofManyCodeRemoved(OneOFManyCodesScanResults result) { }
        
        public void OnOneofManyCodeSelected(OneOFManyCodesScanResults result) { }
       
        public void OnScanBarcodeDetection(bool isDetected) { }
      
        public void OnScanResultFailure(string error) { }
        

        // Barcode , QR , Any Results CallBack
        public void OnScanResultSuccess(ScanResultSuccess result)
        {
            ShowResultDataNew(result.Text);
        }

        void ShowResultDataNew(string result)
        {
          
            this.RunOnUiThread(() =>
            {
                tvResult.Text = result;

                if (bottomSheetBehavior.State != BottomSheetBehavior.StateHalfExpanded)
                {
                    this.RunOnUiThread(() =>
                    {
                        bottomSheetBehavior.State = BottomSheetBehavior.StateHalfExpanded;
                        bottomSheet.Visibility = ViewStates.Visible;
                    });
                }
               
            });
        }

        #endregion

    }
}