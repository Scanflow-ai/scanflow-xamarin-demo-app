using System;
using UIKit;
using Foundation;
using CoreGraphics;
using CoreFoundation;
using Scanflow.BarcodeCapture.Xamarin.iOS;
using Scanflow.TextCapture.Xamarin.iOS;
using Scanflow.Xamarin.Native.iOS.Models;
using System.Timers;
using ScannerMode = Scanflow.TextCapture.Xamarin.iOS.ScannerMode;
using System.Collections.Generic;
using CoreVideo;
using CoreLocation;
using Xamarin.Essentials;
using GlobalToast;
using CarPlay;

namespace Scanflow.Xamarin.Native.iOS
{
    partial class CameraViewController : UIViewController, TextCapture.Xamarin.iOS.IScanflowCameraManagerDelegate
    {

        public static List<HomePageIcons> homepageicons;
        private bool flashLightCheck = false;
        public int screenID = 0;
        public string scannerMode;
        public string overlayViewApperance;
        private string[] codeResults = new string[0];
        private NSTimer resultTimer;
        private Timer flashTimer;
        private Timer tireResultTimer;
        private bool flashOffRequested = false;
        private string[] tireResults;
        private UIImage tireImage;
        private float currentConfidenceLevel;
        public ScanflowBarCodeManager scanflowBarCodeManager;
        private UIView popupView;
        private nfloat initialY;
        public BottomHalfPopup bottomHalfPopup;
        public UIActivityIndicatorView activityIndicator;
        // private ResultViewController resultViewController;

        private ScanflowTextManager scanflowTextManager;
        private ScanflowBarCodeManager scanFlowManager;

        public CameraViewController(IntPtr handle) : base(handle)
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (scannerMode == ScannerMode.ContainerHorizontal || scannerMode == ScannerMode.ContainerVertical || scannerMode == ScannerMode.Tire)
            {
                captureBtn.Hidden = false;
                captureBtn.SetTitle("Capture", UIControlState.Normal);

                ObjCRuntime.Class.ThrowOnInitFailure = false;
                scanflowTextManager = new ScanflowTextManager(scanView, scannerMode, overlayViewApperance, true, UIColor.Red, UIColor.Yellow, UIColor.White, UIColor.Purple, false);
                scanflowTextManager.ValidateLicense("43a7841d3e4e4595b052f3bdc6e53ea6b125c292");
                scanflowTextManager.WeakDelegate = this;
                scanflowTextManager.StartSession();
            }
            else
            {
                captureBtn.Hidden = true;
                ObjCRuntime.Class.ThrowOnInitFailure = false;
                scanflowBarCodeManager = new ScanflowBarCodeManager(scanView, scannerMode, overlayViewApperance, false, UIColor.Red, UIColor.Yellow, UIColor.White, UIColor.Purple, false);
                scanflowBarCodeManager.ValidateLicense("43a7841d3e4e4595b052f3bdc6e53ea6b125c292");
                scanflowBarCodeManager.WeakDelegate = this;
                scanflowBarCodeManager.StartSession();
            }

            SetupUI();
            flashLightButton.SetImage(UIImage.FromBundle(AppImages.FlashOff).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);

            bottomHalfPopup = new BottomHalfPopup(this, scannerMode);

            captureBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
            captureBtn.BackgroundColor = UIColor.Blue; // Customize button appearance

            // Create a UIActivityIndicatorView
            activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.White);
            activityIndicator.HidesWhenStopped = true;
            activityIndicator.Center = new CoreGraphics.CGPoint(captureBtn.Bounds.Width / 1.5, captureBtn.Bounds.Height / 2);

            // Add the UIActivityIndicatorView as a subview of the UIButton
            captureBtn.AddSubview(activityIndicator);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBarHidden = true;
        }
        public static CameraViewController InitWithStory()
        {
            UIStoryboard storyboard = UIStoryboard.FromName("Main", null);
            CameraViewController vc = storyboard.InstantiateViewController("CameraViewController") as CameraViewController;
            return vc;
        }


        private void SetupUI()
        {
            homepageicons = new List<HomePageIcons>{ new HomePageIcons("QR Code", UIImage.FromBundle("QR Code")), new HomePageIcons("Bar Code", UIImage.FromBundle("BarCode")), new HomePageIcons("Any", UIImage.FromBundle("Any")),new HomePageIcons("Batch/Inventory", UIImage.FromBundle("BatchInventory")), new HomePageIcons("One of many codes", UIImage.FromBundle("OneofMany")), new HomePageIcons("Pivot View", UIImage.FromBundle("PivotView")),new HomePageIcons("Tire Scanning", UIImage.FromBundle("DoumentScanning")),  new HomePageIcons("Container Horizontal Scanning", UIImage.FromBundle("DoumentScanning")),
           new HomePageIcons("Container Vertical", UIImage.FromBundle("DoumentScanning"))};
            functionTitle.Text = homepageicons[screenID].Title;
            scannedLabel.Text = AppStrings.ScannedLabel;
            scannedLabel.Layer.CornerRadius = 20;
            scannedLabel.TextColor = UIColor.White;
            scannedLabel.Hidden = true;
            scannedLabel.Layer.MasksToBounds = true;
            scannedLabel.TextAlignment = UITextAlignment.Center;
        }


        partial void backButtonTapped(Foundation.NSObject sender)
        {
            NavigationController.PopViewController(true);

        }
        partial void captureBtnTapped(Foundation.NSObject sender)
        {
            if (!activityIndicator.IsAnimating)
            {
                activityIndicator.StartAnimating();
                captureBtn.SetTitle("Scanning...", UIControlState.Normal);
            }
            scanflowTextManager.StartCaptureData();
        }
        partial void flashLightBtnTapped(Foundation.NSObject sender)
        {
            flashLightCheck = !flashLightCheck;
            flashLightButton.SetImage(UIImage.FromBundle(flashLightCheck ? AppImages.FlashOn : AppImages.FlashOff).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
            if (scanflowTextManager == null)
                scanflowBarCodeManager.FlashLight(flashLightCheck);
            else
                scanflowTextManager.FlashLight(flashLightCheck);
        }

        partial void settingsBtnTapped(Foundation.NSObject sender)
        {
            UIStoryboard storyboard = UIStoryboard.FromName("Main", null);
            SettingsViewController settingsViewController = storyboard.InstantiateViewController("SettingsViewController") as SettingsViewController;
            if (settingsViewController != null)
            {
                var navigationController = storyboard.InstantiateViewController("navigationController") as UINavigationController;
                navigationController.PushViewController(settingsViewController, animated: false); 
                this.PresentViewController(navigationController, true, null); 
            }
        }

      
        public void PresentCameraPermissionsDeniedAlert()
        {

        }

        public void LocationAccessDeniedAlert()
        {

        }

        public void PresentVideoConfigurationErrorAlert()
        {

        }

        public void SessionRunTimeErrorOccurred()
        {

        }

        public void SessionWasInterrupted(bool resumeManually)
        {

        }

        public void SessionWasInterrupted()
        {

        }

        public void Captured(CVPixelBuffer originalframe, CGRect overlayFrame, UIImage croppedImage)
        {

        }

        public void CapturedOutput(string result, string codeType, string[] results, UIImage processedImage, CLLocation location)
        {
            if (codeType == "batchInventory")
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    activityIndicator.StopAnimating();
                    captureBtn.SetTitle("Scanned", UIControlState.Normal);
                    if (results.Length > 0)
                    {
                        foreach (var item in results)
                        {
                            bottomHalfPopup.PopupText = result;
                        }
                        bottomHalfPopup.ShowPopup();
                        captureBtn.SetTitle("Capture", UIControlState.Normal);

                    }
                });
            }
            else
            {
                if (!string.IsNullOrEmpty(result))
                {
                    MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        if (scannerMode == ScannerMode.ContainerHorizontal || scannerMode == ScannerMode.ContainerVertical || scannerMode == ScannerMode.Tire)
                        {
                            captureBtn.SetTitle("Scanned", UIControlState.Normal);
                            bottomHalfPopup.PopupText = result;
                            bottomHalfPopup.PopupImage = processedImage;
                        }
                        else
                        {
                            bottomHalfPopup.PopupText = result;
                        }

                        // Show the popup
                        bottomHalfPopup.ShowPopup();
                        captureBtn.SetTitle("Capture", UIControlState.Normal);
                    });
                }
            }
        }

        public void ShowAlert(string title, string message)
        {

        }


    }

    public class BottomHalfPopup
    {
        private UIView popupView;
        private UIViewController parentViewController;
        public string PopupText { get; set; }
        public UIImage PopupImage { get; set; }
        public UILabel resultText;
        public UIImageView resultImage;
        public UIImageView copyImage;

        public BottomHalfPopup(UIViewController parentViewController, string scannerType)
        {
            this.parentViewController = parentViewController;
            CreatePopupView(scannerType);
        }

        private void CreatePopupView(string scannerType)
        {

            if (scannerType == ScannerMode.ContainerHorizontal || scannerType == ScannerMode.ContainerVertical || scannerType == ScannerMode.Tire)
            {
                popupView = new UIView(new CGRect(0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height / 2.5));
                copyImage = new UIImageView(new CGRect(popupView.Frame.Width - 80, 20, 40, 40));
                resultText = new UILabel(new CGRect(20, 20, 150, 30));
            }
            else
            {
                popupView = new UIView(new CGRect(0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height / 4));
                copyImage = new UIImageView(new CGRect(popupView.Frame.Width - 80, 50, 40, 40));
                resultText = new UILabel(new CGRect(20, 20, 250, 100));
            }

            popupView.BackgroundColor = UIColor.White;

            resultText.Text = PopupText;
            resultText.TextColor = UIColor.Black;
            resultText.Lines = 0;
            resultText.LineBreakMode = UILineBreakMode.WordWrap;
            resultText.TextAlignment = UITextAlignment.Left;
            popupView.AddSubview(resultText);

            copyImage.Image = UIImage.FromBundle("CopyIcon");
            copyImage.ContentMode = UIViewContentMode.ScaleAspectFit;
            popupView.AddSubview(copyImage);

            var tapGestureRecognizer = new UITapGestureRecognizer(CopyTapped);

            // Attach the tap gesture recognizer to the imageView
            copyImage.UserInteractionEnabled = true;
            copyImage.AddGestureRecognizer(tapGestureRecognizer);

            // Create a UIImageView
            resultImage = new UIImageView(new CGRect(20, 80, popupView.Frame.Width - 30, 150));
            resultImage.Image = PopupImage;
            resultImage.ContentMode = UIViewContentMode.ScaleAspectFit;
            popupView.AddSubview(resultImage);

            // Add a pan gesture recognizer to the popup view
            var panGestureRecognizer = new UIPanGestureRecognizer(HandlePan);
            popupView.AddGestureRecognizer(panGestureRecognizer);
        }
        private void CopyTapped()
        {
            Clipboard.SetTextAsync(PopupText);
            Toast.MakeToast("Text Copied.").Show();

        }
        public void ShowPopup()
        {
            resultText.Text = PopupText;
            resultImage.Image = PopupImage;
            parentViewController.View.AddSubview(popupView);
            AnimatePopupIn();
        }

        private void HandlePan(UIPanGestureRecognizer recognizer)
        {
            AnimatePopupOut();
        }

        private void AnimatePopupIn()
        {
            UIView.Animate(0.3, () =>
            {
                popupView.Frame = new CGRect(0, UIScreen.MainScreen.Bounds.Height - popupView.Frame.Height, UIScreen.MainScreen.Bounds.Width, popupView.Frame.Height);
            });
        }

        private void AnimatePopupOut()
        {
            UIView.Animate(0.3, () =>
            {
                popupView.Frame = new CGRect(0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width, popupView.Frame.Height);
            }, () =>
            {
                popupView.RemoveFromSuperview();
            });
        }
    }



}
