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
	[Register ("ScanFlowCollectionViewCell")]
	partial class ScanFlowCollectionViewCell
	{
		[Outlet]
		UIKit.UIImageView iconImgView { get; set; }

		[Outlet]
		UIKit.UILabel iconNameLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
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
