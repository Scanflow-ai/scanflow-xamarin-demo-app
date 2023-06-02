using Scanflow.Helper;
using Scanflow.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scanflow.ViewModels
{
    public class HomePageViewModel
    {
       public ObservableCollection<ScanResult> ScanModels { get; set; }= new ObservableCollection<ScanResult>
       {
           new ScanResult
                {
                    Name= ConstantStrings.QR_Code,
                    Image="resource://Scanflow.Resources.qrCode.svg"
                }, new ScanResult
                {
                    Name=ConstantStrings.Barcode,
                    Image="resource://Scanflow.Resources.barCode.svg"
                }, new ScanResult
                {
                    Name=ConstantStrings.Any,
                    Image="resource://Scanflow.Resources.any.svg"
                }, new ScanResult
                {
                    Name=ConstantStrings.Batch,
                    Image="resource://Scanflow.Resources.batch.svg"
                }, new ScanResult
                {
                    Name=ConstantStrings.ManyCode,
                    Image="resource://Scanflow.Resources.manyCode.svg"
                }, new ScanResult
                {
                    Name=ConstantStrings.Pivot,
                    Image="resource://Scanflow.Resources.pivot.svg"
                }, new ScanResult
                {
                    Name=ConstantStrings.IDcard,
                    Image="resource://Scanflow.Resources.qrCode.svg"
                }, new ScanResult
                {
                    Name=ConstantStrings.Tyre,
                    Image="resource://Scanflow.Resources.qrCode.svg"
                }, new ScanResult
                {
                    Name=ConstantStrings.Vertical,
                    Image="resource://Scanflow.Resources.qrCode.svg"
                },
                new ScanResult
                {
                    Name=ConstantStrings.Horizontal,
                   Image="resource://Scanflow.Resources.qrCode.svg"
                },
       };
        public HomePageViewModel()
        {
            
        }

    }

}
