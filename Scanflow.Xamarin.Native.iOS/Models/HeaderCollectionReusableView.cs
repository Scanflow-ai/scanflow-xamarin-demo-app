
using System;
using Foundation;
using UIKit;
namespace Scanflow.Xamarin.Native.iOS.Models
{
    public class HeaderCollectionReusableView : UICollectionReusableView
    {
        public static readonly NSString Identifier = new NSString("HeaderCollectionReusableView");
        private readonly UILabel label = new UILabel();

        public void Configure(string title)
        {
            label.TextAlignment = UITextAlignment.Left;
            label.Text = title;
            BackgroundColor = UIColor.Clear;
            AddSubview(label);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            label.Frame = Bounds;
        }
    }

}

