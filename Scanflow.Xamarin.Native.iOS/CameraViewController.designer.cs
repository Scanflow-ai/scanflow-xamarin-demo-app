// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Scanflow.Xamarin.Native.iOS
{
	[Register ("CameraViewController")]
	partial class CameraViewController
	{
		[Outlet]
		UIKit.UIButton backButton { get; set; }

		[Outlet]
		UIKit.UIButton captureBtn { get; set; }

		[Outlet]
		UIKit.UIButton flashLightButton { get; set; }

		[Outlet]
		UIKit.UILabel functionTitle { get; set; }

		[Outlet]
		UIKit.UIButton oneTouchZoomButton { get; set; }

		[Outlet]
		UIKit.UILabel scannedLabel { get; set; }

		[Outlet]
		UIKit.UIView scanView { get; set; }

		[Outlet]
		UIKit.UIButton settingsButton { get; set; }

		[Action ("backButtonTapped:")]
		partial void backButtonTapped (Foundation.NSObject sender);

		[Action ("captureBtnTapped:")]
		partial void captureBtnTapped (Foundation.NSObject sender);

		[Action ("flashLightBtnTapped:")]
		partial void flashLightBtnTapped (Foundation.NSObject sender);

		[Action ("settingsBtnTapped:")]
		partial void settingsBtnTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (backButton != null) {
				backButton.Dispose ();
				backButton = null;
			}

			if (functionTitle != null) {
				functionTitle.Dispose ();
				functionTitle = null;
			}

			if (settingsButton != null) {
				settingsButton.Dispose ();
				settingsButton = null;
			}

			if (flashLightButton != null) {
				flashLightButton.Dispose ();
				flashLightButton = null;
			}

			if (scanView != null) {
				scanView.Dispose ();
				scanView = null;
			}

			if (oneTouchZoomButton != null) {
				oneTouchZoomButton.Dispose ();
				oneTouchZoomButton = null;
			}

			if (scannedLabel != null) {
				scannedLabel.Dispose ();
				scannedLabel = null;
			}

			if (captureBtn != null) {
				captureBtn.Dispose ();
				captureBtn = null;
			}
		}
	}
}
