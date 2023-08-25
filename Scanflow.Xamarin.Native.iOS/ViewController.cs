using CoreGraphics;
using CoreLocation;
using CoreVideo;
using Foundation;
using Scanflow.BarcodeCapture.Xamarin.iOS;
using Scanflow.TextCapture.Xamarin.iOS;
using System;
using UIKit;
using Xamarin.Essentials;
using IScanflowCameraManagerDelegate = Scanflow.TextCapture.Xamarin.iOS.IScanflowCameraManagerDelegate;
using OverlayViewApperance = Scanflow.TextCapture.Xamarin.iOS.OverlayViewApperance;
using ScannerMode = Scanflow.TextCapture.Xamarin.iOS.ScannerMode;

namespace Scanflow.Xamarin.Native.iOS
{
    public partial class ViewController : UIViewController, IScanflowCameraManagerDelegate
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public async override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (status != PermissionStatus.Granted)
            {
                // Request camera permission
                status = await Permissions.RequestAsync<Permissions.Camera>();

                if (status == PermissionStatus.Granted)
                {
                    ObjCRuntime.Class.ThrowOnInitFailure = false;
                    ScanflowTextManager scanflowBarCodeManager = new ScanflowTextManager(this.View, ScannerMode.ContainerHorizontal, OverlayViewApperance.ContainerHorizantal, false, UIColor.Red, UIColor.Yellow, UIColor.White, UIColor.Purple, false);
                    scanflowBarCodeManager.ValidateLicense("43a7841d3e4e4595b052f3bdc6e53ea6b125c292");
                    scanflowBarCodeManager.WeakDelegate = this;
                    scanflowBarCodeManager.StartSession();
                    //scanflowBarCodeManager.SetResolution(this.View,new NSDate(),Resolution.HD4K3840x2160);
                }
                else
                    return;
            }
            else
            {
                ObjCRuntime.Class.ThrowOnInitFailure = false;
                ScanflowTextManager scanflowBarCodeManager = new ScanflowTextManager(this.View, ScannerMode.ContainerHorizontal, OverlayViewApperance.ContainerHorizantal, false, UIColor.Red, UIColor.Yellow, UIColor.White, UIColor.Purple, false);
                scanflowBarCodeManager.ValidateLicense("43a7841d3e4e4595b052f3bdc6e53ea6b125c292");
                scanflowBarCodeManager.WeakDelegate = this;
                scanflowBarCodeManager.StartSession();

            }
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
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
           
        }

        public void ShowAlert(string title, string message)
        {
           
        }
    }
}
