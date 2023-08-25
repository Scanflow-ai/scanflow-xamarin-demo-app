
using System;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Com.Scanflow.Datacapture.Config;
using Com.Scanflow.Datacapture.Core.Camera;
using Com.Scanflow.Datacapture.Sftext;
using Com.Scanflow.Datacapture.Text;
using Google.Android.Material.BottomSheet;
using Google.Android.Material.Button;
using Xamarin.Essentials;
using static Com.Scanflow.Datacapture.Core.Camera.ScanflowReader;

namespace Scanflow.Xamarin.Android.Activities
{
    [Activity (Label = "TextScanActivity")]			
	public class TextScanActivity : AppCompatActivity, ScanflowReader.IOnTextScanResultCallback
    {
        public ScanflowReader mTextReader;
        public TextCaptureConfig textCaptureConfig;
        private Timer timer;
        TextView tvResult;
        BottomSheetBehavior bottomSheetBehavior;
        View bottomSheet;
        public ImageView flashBtn;
        public bool Isflash = false;
        public MaterialButton captureBtn;
        public ImageView scannedImage;
        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.TextScanLayout);
            var preview = this.FindViewById<SFCameraView>(Resource.Id.scanflowView);
            var title = this.FindViewById<TextView>(Resource.Id.textView);
            flashBtn = this.FindViewById<ImageView>(Resource.Id.ivFlashLight);
            var settingsBtn = this.FindViewById<ImageView>(Resource.Id.ivSettings);
            var copyBtn = this.FindViewById<ImageView>(Resource.Id.ivCopyResult);
            scannedImage = this.FindViewById<ImageView>(Resource.Id.ivImage);
            var backBtn = this.FindViewById<ImageView>(Resource.Id.ivBack);
            captureBtn = this.FindViewById<MaterialButton>(Resource.Id.btnImageCapture);
            tvResult = this.FindViewById<TextView>(Resource.Id.tvResult);
            bottomSheetBehavior = BottomSheetBehavior.From(this.FindViewById<View>(Resource.Id.bottomSheet));
            bottomSheet = this.FindViewById<View>(Resource.Id.bottomSheet);
            bottomSheet.Click += BottomSheet_Click;
            flashBtn.Click += FlashBtn_Click;
            captureBtn.Click += CaptureBtn_Click;
            copyBtn.Click += CopyBtn_Click;
            backBtn.Click += BackBtn_Click;
            flashBtn.SetImageResource(Resource.Drawable.ic_flash_disable);
            if (Intent.HasExtra("ScanType"))
            {
                string ScanType = Intent.GetStringExtra("ScanType");
                if (ScanType == "Tyre Scanning")
                {
                    textCaptureConfig = TextCaptureConfig.TyreNumber;
                    title.Text = "Tyre Scanning";
                }
                else if (ScanType == "Vertical Container Scanning")
                {
                    textCaptureConfig = TextCaptureConfig.ContainerVertical;
                    title.Text = "Vertical Container Scanning";
                }
                else
                {
                    textCaptureConfig = TextCaptureConfig.ContainerHorizontal;
                    title.Text = "Horizontal Container Scanning";
                }


                mTextReader = SFTextCaptureSession.Instance.CreateScanSession(this, "b0febcacca30d073e104af811f939b9608984b60", preview, textCaptureConfig);
                mTextReader?.SetOnTextScanResultCallback(this);
                mTextReader?.SetEnableLocationTracking(false);
            }


            if (ActivityCompat.CheckSelfPermission(this, Android.Manifest.Permission.Camera) == (int)Permission.Granted)
            {
                mTextReader?.StartCamera();
            }
        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }
        private void CopyBtn_Click(object sender, EventArgs e)
        {
            ClipboardManager clipboardManager = (ClipboardManager)GetSystemService(Context.ClipboardService);
            ClipData clip = ClipData.NewPlainText("CopiedText", tvResult.Text);
            clipboardManager.PrimaryClip = clip;

            Toast.MakeText(this, "Text copied", ToastLength.Short).Show();
        }

        private void CaptureBtn_Click(object sender, EventArgs e)
        {
            captureBtn.Text = "Scanning...";
            mTextReader?.SetIsContinuousScan(true);
        }

        private async void FlashBtn_Click(object sender, EventArgs e)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (mTextReader.IsTorchEnabled)
                {
                    flashBtn.SetImageResource(Resource.Drawable.ic_flash_disable);
                    mTextReader?.EnableTorch(false);
                }
                else
                {
                    flashBtn.SetImageResource(Resource.Drawable.ic_flash_enable);
                    mTextReader?.EnableTorch(true);
                }
            });
        }

        private void BottomSheet_Click(object sender, EventArgs e)
        {
            captureBtn.Text = "Capture";
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

        public void OnScanResultFailure(string error)
        {

        }

        public void OnScanResultSuccess(ScanflowReader.TextScanResult result)
        {
            captureBtn.Text = "Scanned";
            mTextReader?.SetIsContinuousScan(false);
            ShowResultDataNew(result);
        }
        void ShowResultDataNew(TextScanResult result)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            this.RunOnUiThread(() =>
            {
                tvResult.Text = result.Text;
                scannedImage.SetImageBitmap(result.Image);
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

