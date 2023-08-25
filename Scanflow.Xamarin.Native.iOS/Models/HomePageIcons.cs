using System;
using UIKit;

namespace Scanflow.Xamarin.Native.iOS.Models
{
    public struct HomePageIcons
    {
        public string Title { get; }
        public UIImage Image { get; }

        public HomePageIcons(string title, UIImage image)
        {
            Title = title;
            Image = image;
        }
    }

    public static class IconData
    {
        public static HomePageIcons[] homepageicons = new HomePageIcons[]
        {
        new HomePageIcons("QR Code", UIImage.FromBundle("QR Code") ?? new UIImage()),
        new HomePageIcons("Bar Code", UIImage.FromBundle("BarCode") ?? new UIImage()),
        new HomePageIcons("Any", UIImage.FromBundle("Any") ?? new UIImage()),
        new HomePageIcons("Batch/Inventory", UIImage.FromBundle("BatchInventory") ?? new UIImage()),
        new HomePageIcons("One of Many Codes", UIImage.FromBundle("OneofMany") ?? new UIImage()),
        new HomePageIcons("Pivot View", UIImage.FromBundle("PivotView") ?? new UIImage()),
        new HomePageIcons("Tire Scanning", UIImage.FromBundle("DocumentScanning") ?? new UIImage())
        };
    }

}

