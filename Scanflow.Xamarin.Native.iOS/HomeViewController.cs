using System;
using UIKit;
using CoreGraphics;
using Scanflow.Xamarin.Native.iOS.Models;
using Foundation;
using System.Collections.Generic;
using OverlayViewApperance = Scanflow.TextCapture.Xamarin.iOS.OverlayViewApperance;
using ScannerMode = Scanflow.TextCapture.Xamarin.iOS.ScannerMode;

namespace Scanflow.Xamarin.Native.iOS
{
    partial class HomeViewController : UIViewController
    {
        public static List<HomePageIcons> homepageicons;

        public HomeViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupUI();
            var appVersion = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
            versionLabel.Text = $"Version : {appVersion}";
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBarHidden = true;
        }

        private void SetupUI()
        {
            scanFlowTitle.Text = AppStrings.ScanflowAppDescription;
            scanFlowTitle.TextAlignment = UITextAlignment.Justified;

            scanCollectionView.DataSource = new ScanCollectionViewDataSource(this);
            scanCollectionView.Delegate = new ScanCollectionViewDelegate(this);
            scanCollectionView.CollectionViewLayout = new UICollectionViewFlowLayout();

            scanCollectionView.RegisterClassForCell(typeof(ScanFlowCollectionViewCell), "ScanFlowCollectionViewCell");

           
        }

        public static HomeViewController InitWithStory()
        {
            var storyboard = UIStoryboard.FromName("Main", null);
            var vc = storyboard.InstantiateViewController("HomeViewController") as HomeViewController;
            return vc;
        }

        // Other methods and properties...

        internal class ScanCollectionViewDataSource : UICollectionViewDataSource
        {
            readonly HomeViewController parentViewController;

            public ScanCollectionViewDataSource(HomeViewController parentViewController)
            {
                this.parentViewController = parentViewController;
            }

            public override nint GetItemsCount(UICollectionView collectionView, nint section)
            {
                homepageicons = new List<HomePageIcons>
{
    new HomePageIcons("QR Code", UIImage.FromBundle("QR Code")),
    new HomePageIcons("Bar Code", UIImage.FromBundle("BarCode")),
    new HomePageIcons("Any", UIImage.FromBundle("Any")),
    new HomePageIcons("Batch/Inventory", UIImage.FromBundle("BatchInventory")),
    new HomePageIcons("One of many codes", UIImage.FromBundle("OneofMany")),
    new HomePageIcons("Pivot View", UIImage.FromBundle("PivotView")),
    new HomePageIcons("Tire Scanning", UIImage.FromBundle("DoumentScanning")),
    new HomePageIcons("Container Horizontal Scanning", UIImage.FromBundle("ContainerScanning")),
    new HomePageIcons("Container Vertical Scanning", UIImage.FromBundle("ContainerScanning"))
};
                return homepageicons.Count;
            }

            public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
            {
                var cell = (ScanFlowCollectionViewCell)collectionView.DequeueReusableCell("ScanFlowCollectionViewCell", indexPath);
                var item = homepageicons[indexPath.Row];

                cell.Setup(item);

                return cell;
            }

        }

        internal class ScanCollectionViewDelegate : UICollectionViewDelegateFlowLayout
        {
            readonly HomeViewController parentViewController;

            public ScanCollectionViewDelegate(HomeViewController parentViewController)
            {
                this.parentViewController = parentViewController;
            }

            public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
            {
                return new CGSize(90, 120);
            }

           
            public override CGSize GetReferenceSizeForHeader(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
            {
                return new CGSize(122, 40);
            }

            public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
            {
                CameraViewController cameraViewController = CameraViewController.InitWithStory();
                cameraViewController.screenID = indexPath.Row;
                switch (indexPath.Row)
                {
                    case 0:
                        cameraViewController.scannerMode = ScannerMode.QRCode;
                        cameraViewController.overlayViewApperance = OverlayViewApperance.Square;
                        break;
                    case 1:
                        cameraViewController.scannerMode = ScannerMode.Barcode;
                        cameraViewController.overlayViewApperance = OverlayViewApperance.Square;
                        break;
                    case 2:
                        cameraViewController.scannerMode = ScannerMode.Any;
                        cameraViewController.overlayViewApperance = OverlayViewApperance.Hide;
                        break;
                    case 3:
                        cameraViewController.scannerMode = ScannerMode.BatchInventory;
                        cameraViewController.overlayViewApperance = OverlayViewApperance.Hide;
                        break;
                    case 4:
                        cameraViewController.scannerMode = ScannerMode.OneOfMany;
                        cameraViewController.overlayViewApperance = OverlayViewApperance.Hide;
                        break;
                    case 5:
                        cameraViewController.scannerMode = ScannerMode.PivotView;
                        cameraViewController.overlayViewApperance = OverlayViewApperance.Square;
                        break;
                    case 6:
                        cameraViewController.scannerMode = ScannerMode.Tire;
                        cameraViewController.overlayViewApperance = OverlayViewApperance.ContainerHorizantal;
                        break;
                    case 7:
                        cameraViewController.scannerMode = ScannerMode.ContainerHorizontal;
                        cameraViewController.overlayViewApperance = OverlayViewApperance.ContainerHorizantal;
                        break;
                    case 8:
                        cameraViewController.scannerMode = ScannerMode.ContainerVertical;
                        cameraViewController.overlayViewApperance = OverlayViewApperance.ContainerVertical;
                        break;

                }
                if (cameraViewController != null)
                {
                    parentViewController.NavigationController.PushViewController(cameraViewController, true);
                }
            }
        }
    }

}
