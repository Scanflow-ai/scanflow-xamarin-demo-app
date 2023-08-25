using Rg.Plugins.Popup.Extensions;
using Scanflow.Helper;
using Scanflow.Interface;
using Scanflow.Models;
using Scanflow.TextCapture.Xamarin.Forms;
using Scanflow.TextCapture.Xamarin.Forms.Models;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scanflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanViewPage : ContentPage
    {
        public bool isTorch = true;
        public ScanViewPage(Models.ScanResult result)
        {
            InitializeComponent();
            scanTitle.Text = result.Name;
            Scanners(result);
           // textCaptureScan.OnScanResult += TextCaptureScan_OnScanResult;
        }
        private void Scanners(Models.ScanResult result)
        {
            switch (result.Name)
            {
                case Helper.ConstantStrings.Horizontal:
                    textCaptureScan.CreateScanSession("347ea26729266ce315097a9484b4ebbe771a1e83", TextCaptureConfig.HorizontalContainer, true);
                    break;

                case Helper.ConstantStrings.Vertical:
                    textCaptureScan.CreateScanSession("347ea26729266ce315097a9484b4ebbe771a1e83", TextCaptureConfig.VerticalContainer, true);
                    break;

                case Helper.ConstantStrings.Tyre:
                    textCaptureScan.CreateScanSession("347ea26729266ce315097a9484b4ebbe771a1e83", TextCaptureConfig.Tire, true);
                    break;

                default:

                    break;
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
         //   textCaptureScan.StartScanning();
            

        }
       


        private async void TextCaptureScan_OnScanResult(Scanflow.TextCapture.Xamarin.Forms.Models.ScanResult result)
        {
            await Device.InvokeOnMainThreadAsync(() =>
             {

                 //scanText.Text = result.Text;
                 //scanImg.Source = result.Image;
               //  OpenDrawer();
                   Navigation.PushModalAsync(new TextCaptureResultPopup(result));
                 btnDrawer.Text = "Capture";
                 progressIndicator.IsRunning = false;

                 btnDrawer.IsEnabled = true;
             });

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            textCaptureScan.StopScanning();
            // StopScanning();


        }




        private bool isScanning = false;
        private void Button_Clicked(object sender, EventArgs e)
        {

            if (!isScanning)
            {
                isScanning = true;
                btnDrawer.IsEnabled = false;

                textCaptureScan.StartScanning();
                btnDrawer.Text = "Scanning...";
                progressIndicator.IsEnabled = true;
                progressIndicator.IsVisible = true;
                progressIndicator.IsRunning = true;

                // Reset the scanning flag and enable the button
                isScanning = false;
                btnDrawer.IsEnabled = true;
            }
            //textCaptureScan.StartScanning();
            //btnDrawer.Text = "Scanning...";
            //progressIndicator.IsEnabled = true;
            //progressIndicator.IsVisible = true;
            //progressIndicator.IsRunning = true;

        }

        //private void OpenDrawer()
        //{
        //    myDrawer.IsVisible = true;
        //    if (myDrawer.Height == 0)
        //    {
        //        void callback(double input) => myDrawer.HeightRequest = input;
        //        double startHeight = 0;
        //        double endHeight = 280;
        //        uint rate = 3;
        //        uint length = 500;
        //        Easing easing = Easing.CubicInOut;
        //        myDrawer.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
        //    }
        //    else
        //    {
        //        {
        //            Action<double> callback = input => myDrawer.HeightRequest = input;
        //            double startHeight = 0;
        //            double endHeight = 280;
        //            uint rate = 3;
        //            uint length = 500;
        //            Easing easing = Easing.CubicOut;
        //            myDrawer.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
        //        }
        //    }
        //}

        private async void Setting_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new SettingPopupPage(textCaptureScan));
        }

        private void FlashLight_Tapped(object sender, EventArgs e)
        {
            if (isTorch)
            {
                isTorch = false;
                torchImage.Source = "resource://Scanflow.Resources.flashOff.svg";
                textCaptureScan.EnableTorch(true);

            }
            else
            {
                isTorch = true;
                torchImage.Source = "resource://Scanflow.Resources.flashOn.svg";
                textCaptureScan.EnableTorch(false);
            }
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    var mainDisplay = DeviceDisplay.MainDisplayInfo;
        //    void callback(double input) => myDrawer.HeightRequest = input;
        //    double startHeight = mainDisplay.Height / 6;
        //    double endHeight = 0;
        //    uint rate = 3;
        //    uint length = 500;
        //    Easing easing = Easing.SinInOut;
        //    myDrawer.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
        //}

        private async void Copy_Tapped(object sender, EventArgs e)
        {
          //  await Clipboard.SetTextAsync(scanText.Text);
            DependencyService.Get<IToast>().Show("Copied");
        }
    }
}