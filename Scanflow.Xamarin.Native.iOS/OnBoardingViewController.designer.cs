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
	[Register ("OnBoardingViewController")]
	partial class OnBoardingViewController
	{
		[Outlet]
		UIKit.UIPageControl pageControl { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollView { get; set; }

		[Outlet]
		UIKit.UIImageView skipBtn { get; set; }

		[Outlet]
		UIKit.UIView tabView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (skipBtn != null) {
				skipBtn.Dispose ();
				skipBtn = null;
			}

			if (pageControl != null) {
				pageControl.Dispose ();
				pageControl = null;
			}

			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}

			if (tabView != null) {
				tabView.Dispose ();
				tabView = null;
			}
		}
	}
}
