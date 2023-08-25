﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
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
using System;
using System.Collections.Generic;
using Timer = System.Timers.Timer;

namespace Scanflow.Xamarin.Android.Activities
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
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BarcodeScanLayout);
            var preview = this.FindViewById<SFCameraView>(Resource.Id.scanflowView);
            var title = this.FindViewById<TextView>(Resource.Id.textView);
            flashBtn = this.FindViewById<ImageView>(Resource.Id.ivFlashLight);
            var backBtn = this.FindViewById<ImageView>(Resource.Id.ivBack);
            var settingsBtn = this.FindViewById<ImageView>(Resource.Id.ivSettings);
            tvResult = this.FindViewById<TextView>(Resource.Id.tvResult);
            var copyBtn = this.FindViewById<ImageView>(Resource.Id.ivCopyResult);
            bottomSheetBehavior = BottomSheetBehavior.From(this.FindViewById<View>(Resource.Id.bottomSheet));
            bottomSheet = this.FindViewById<View>(Resource.Id.bottomSheet);
            bottomSheet.Click += BottomSheet_Click;
            flashBtn.Click += FlashBtn_Click;
            copyBtn.Click += CopyBtn_Click;
            backBtn.Click += BackBtn_Click;
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
                else if (ScanType == "Any")
                {
                    decodeConfig = DecodeConfig.Any;
                    title.Text = "Any";
                }
                else if (ScanType == "Batch/Inventory")
                {
                    decodeConfig = DecodeConfig.BatchInventory;
                    title.Text = "Batch/Inventory";
                }
                else if (ScanType == "One of many codes")
                {
                    decodeConfig = DecodeConfig.OneOfMany;
                    title.Text = "One of many codes";
                }
                else
                {
                    decodeConfig = DecodeConfig.PivotView;
                    title.Text = "Pivot View";
                }

                mBarcodeReader = SFBarcodeCaptureSession.Instance.CreateScanSession(this, "b0febcacca30d073e104af811f939b9608984b60", preview, decodeConfig, 0.4f);
                mBarcodeReader?.SetOnBarcodeScanResultCallback(this);
                mBarcodeReader?.SetEnableLocationTracking(true);
            }
            if (ActivityCompat.CheckSelfPermission(this, Android.Manifest.Permission.Camera) == (int)Permission.Granted)
            {
                mBarcodeReader?.StartCamera();
            }

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
                mBarcodeReader.EnableTorch(false);
            }
        }

        private void BottomSheet_Click(object sender, System.EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);



            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }



        public void OnBatchScanResultSuccess(IList<ScanResultSuccess> result)
        {

        }



        public void OnOneOfManyCodeResult(HashSet results)
        {

        }



        public void OnOneofManyCodeRemoved(OneOFManyCodesScanResults result)
        {

        }



        public void OnOneofManyCodeSelected(OneOFManyCodesScanResults result)
        {

        }



        public void OnScanBarcodeDetection(bool isDetected)
        {

        }



        public void OnScanResultFailure(string error)
        {

        }



        public void OnScanResultSuccess(ScanResultSuccess result)
        {
            ShowResultDataNew(result);
        }

        void ShowResultDataNew(ScanResultSuccess result)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            this.RunOnUiThread(() =>
            {
                tvResult.Text = result.Text;

                timer = new Timer(1000);
                timer.Elapsed += (sender, e) =>
                {
                    if (bottomSheetBehavior.State != BottomSheetBehavior.StateHalfExpanded)
                    {
                        this.RunOnUiThread(() =>
                        {
                            bottomSheetBehavior.State = BottomSheetBehavior.StateHalfExpanded;
                            bottomSheet.Visibility = ViewStates.Visible;
                        });
                    }
                };
                timer.AutoReset = true;
                timer.Start();
            });
        }

       

    }
}