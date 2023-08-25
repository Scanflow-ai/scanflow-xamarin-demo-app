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
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		UIKit.UIButton applyButton { get; set; }

		[Outlet]
		UIKit.UIButton cancelButton { get; set; }

		[Outlet]
		UIKit.UIButton closeButton { get; set; }

		[Outlet]
		UIKit.UIButton enableAutoExposureBtn { get; set; }

		[Outlet]
		UIKit.UIButton enableAutoFlashLight { get; set; }

		[Action ("applyBtnTapped:")]
		partial void applyBtnTapped (Foundation.NSObject sender);

		[Action ("cancelBtnTapped:")]
		partial void cancelBtnTapped (Foundation.NSObject sender);

		[Action ("closeBtnTapped:")]
		partial void closeBtnTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (applyButton != null) {
				applyButton.Dispose ();
				applyButton = null;
			}

			if (cancelButton != null) {
				cancelButton.Dispose ();
				cancelButton = null;
			}

			if (closeButton != null) {
				closeButton.Dispose ();
				closeButton = null;
			}

			if (enableAutoFlashLight != null) {
				enableAutoFlashLight.Dispose ();
				enableAutoFlashLight = null;
			}

			if (enableAutoExposureBtn != null) {
				enableAutoExposureBtn.Dispose ();
				enableAutoExposureBtn = null;
			}
		}
	}
}
