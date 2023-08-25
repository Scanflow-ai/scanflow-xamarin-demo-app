using System;
using Foundation;
using UIKit;

namespace Scanflow.Xamarin.Native.iOS.Models
{
    public static class AppStrings
    {
        public static string DescriptionIdScan => "Scanflow captures multiple data from ID scan";
        public static string DescriptionARmeasurement => "Scanflow captures multiple data from AR measurement";
        public static string DescriptionTextCapture => "Scanflow captures multiple data from Text capture";
        public static string DescriptionBarcode => "Scanflow captures multiple data from Bar code";
        public static string MultipleData => "multiple data";
        public static string IdScan => "ID scan";
        public static string TextCapture => "Text capture";
        public static string ArMeasurement => "AR measurement";
        public static string BarCode => "Bar code";

        public static string Onboarding1 => "OnboardScreen1";
        public static string Onboarding2 => "OnboardScreen2";
        public static string Onboarding3 => "OnboardScreen3";
        public static string Onboarding4 => "OnboardScreen4";

        public static string EnableAutoFlash => "Enable auto flashlight";
        public static string EnableAutoExposure => "Enable auto exposure";
        public static string EnableOneTouchZoom => "Enable one-touch zoom";
        public static string EnableAutoZoom => "Enable auto zoom";
        public static string None => "None";
        public static string SD480P => "SD - 480p";
        public static string HD720P => "HD - 720p";
        public static string FullHD1080P => "Full HD - 1080p";
        public static string FourK => "4K";
        public static string ZoomOptions => "Zoom Options";
        public static string SelectResolution => "Select Resolution";
        public static string SettingsString => "Settings";
        public static string Cancel => "Cancel";
        public static string Apply => "Apply";

        public static string SeeDetails => "See Details";
        public static string Capture => "Capture";
        public static string Scanning => "Scanning...";
        public static string Scanned => "Scanned  ";
        public static string ScannedLabel => "No of scanned: 0";
        public static string ScanMore => "Scan More";
        public static string CopiedSuccessfully => "COPIED";

        public static string ScanflowAppDescription =>
            "Scanflow is an AI scanner on smart devices for data capture & workflow automation kit, that captures multiple data from Barcode, QR code, Text, ID's, Safety codes.";

        public static string SelectScanner => "Select Scanner";

        public static string CameraPermission => "Camera Permission Required";
        public static string NotAllPermissionGiven => "Not accepted all the permissions";
    }

    public static class StringExtensions
    {
        public static NSAttributedString AttributedStringWithColor(
     this string source,
     string[] strings,
     UIColor color,
     uint? characterSpacing = null)
        {
            var attributedString = new NSMutableAttributedString(source);

            foreach (var searchString in strings)
            {
                int startIndex = source.IndexOf(searchString, StringComparison.OrdinalIgnoreCase);
                if (startIndex >= 0)
                {
                    var range = new NSRange(startIndex, searchString.Length);
                    attributedString.AddAttribute(UIStringAttributeKey.ForegroundColor, color, range);
                }
            }

            if (characterSpacing.HasValue)
            {
                attributedString.AddAttribute(UIStringAttributeKey.KerningAdjustment, NSNumber.FromUInt32(characterSpacing.Value), new NSRange(0, attributedString.Length));
            }

            return attributedString;
        }

    }

}

