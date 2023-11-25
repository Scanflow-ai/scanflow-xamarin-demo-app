using Scanflow.Helper;
using System.Collections.ObjectModel;

namespace ScanflowMaui.ViewModels
{
    public class HomePageViewModel
    {
        public ObservableCollection<ScanResult> ScanModels { get; set; } = new ObservableCollection<ScanResult>
       {
                new ScanResult
                {
                    Name= ConstantStrings.QR_Code,
                    Image="qrcode"
                }, new ScanResult
                {
                    Name=ConstantStrings.Barcode,
                    Image="barcode"
                }, new ScanResult
                {
                    Name=ConstantStrings.Any,
                    Image="any"
                }, new ScanResult
                {
                    Name=ConstantStrings.Batch,
                    Image="batch"
                }, new ScanResult
                {
                    Name=ConstantStrings.ManyCode,
                    Image="manycode"
                }, new ScanResult
                {
                    Name=ConstantStrings.Pivot,
                    Image="pivot"
                }, new ScanResult
                {
                    Name=ConstantStrings.IDcard,
                    Image="qrcode"
                }, new ScanResult
                {
                    Name=ConstantStrings.Tyre,
                    Image="qrcode"
                }, new ScanResult
                {
                    Name=ConstantStrings.Vertical,
                    Image="qrcode"
                },
                new ScanResult
                {
                    Name=ConstantStrings.Horizontal,
                   Image="qrcode"
                },
       };
        public HomePageViewModel()
        {

        }
    }
}
