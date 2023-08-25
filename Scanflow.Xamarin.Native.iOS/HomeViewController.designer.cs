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
	[Register ("HomeViewController")]
	partial class HomeViewController
	{
		[Outlet]
		UIKit.UIImageView iconImgView { get; set; }

		[Outlet]
		UIKit.UILabel iconNameLbl { get; set; }

		[Outlet]
		UIKit.UICollectionView scanCollectionView { get; set; }

		[Outlet]
		UIKit.UILabel scanFlowTitle { get; set; }

		[Outlet]
		UIKit.UILabel versionLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (versionLabel != null) {
				versionLabel.Dispose ();
				versionLabel = null;
			}

			if (scanFlowTitle != null) {
				scanFlowTitle.Dispose ();
				scanFlowTitle = null;
			}

			if (scanCollectionView != null) {
				scanCollectionView.Dispose ();
				scanCollectionView = null;
			}

			if (iconNameLbl != null) {
				iconNameLbl.Dispose ();
				iconNameLbl = null;
			}

			if (iconImgView != null) {
				iconImgView.Dispose ();
				iconImgView = null;
			}
		}
	}
}
