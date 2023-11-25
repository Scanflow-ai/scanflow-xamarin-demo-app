using Mopups.Pages;
using Mopups.Services;
using Scanflow.Helper;
using Scanflow.BarcodeCapture.Maui;


namespace ScanflowMaui.Views;

public partial class ScanViewPage : ContentPage
{
    public bool isTorch = true;
    public bool isScanflow = false;
    public ScanViewPage(ScanResult result)
	{
		InitializeComponent();
        scanTitle.Text = result.Name;
        Scanners(result);
    }
    
    private void Scanners(ScanResult result)
    {
        switch (result.Name)
        {
            case ConstantStrings.Barcode:
                barcodeCaptureScan.CreateScanSession("YOUR LICENSE KEY", DecodeConfig.Barcode, false);
                break;

            case ConstantStrings.QR_Code:
                barcodeCaptureScan.CreateScanSession("YOUR LICENSE KEY", DecodeConfig.QRCode, false);
                break;

            case ConstantStrings.Any:
                barcodeCaptureScan.CreateScanSession("YOUR LICENSE KEY", DecodeConfig.Any, false);
                break;

            default:

                break;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
      
    }
 
  
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        barcodeCaptureScan.StopScanning();
       

    }
    private bool isScanning = false;
    

    private  void Button_Clicked(object sender, EventArgs e)
    {
       
        if (!isScanning)
        {
            isScanning = true;
            btnDrawer.IsEnabled = false;
            isScanflow = true;
            barcodeCaptureScan.StartScanning();
            btnDrawer.Text = "Scanning...";
            progressIndicator.IsEnabled = true;
            progressIndicator.IsVisible = true;
            progressIndicator.IsRunning = true;

           
            // Reset the scanning flag and enable the button)
            isScanning = false;
            btnDrawer.IsEnabled = true;
        }
       
       

    }
    private async void Setting_Tapped(object sender, EventArgs e)
    {
       await MopupService.Instance.PushAsync(new SettingPopupPage());
    }

    private void FlashLight_Tapped(object sender, EventArgs e)
    {

        if (isTorch)
        {
            isTorch = false;
            torchImage.Source = "flashoff";
            barcodeCaptureScan.EnableTorch(false);

        }
        else
        {
            isTorch = true;
            torchImage.Source = "flashon";
            barcodeCaptureScan.EnableTorch(true);
        }
    }

    async void barcodeCaptureScan_OnScanResult(System.Object result)
    {
        var ScannedResult = result as string;
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            if (isScanflow)
            {

                MopupService.Instance.PushAsync(new BarcodeResultPopup(ScannedResult));
                btnDrawer.Text = "Capture";
                progressIndicator.IsRunning = false;
                btnDrawer.IsEnabled = true;
                isScanflow = false;
            }

        });
    }
}

