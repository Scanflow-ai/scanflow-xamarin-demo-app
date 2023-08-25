using Mopups.Pages;
using Mopups.Services;
using Scanflow.Helper;
using Scanflow.TextCapture.Maui;


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
    //   textCaptureScan.OnScanResult += TextCaptureScan_OnScanResult;

    }
    
    private void Scanners(ScanResult result)
    {
        switch (result.Name)
        {
            case ConstantStrings.Horizontal:
                textCaptureScan.CreateScanSession("1a5c7076358dda4060bcfc45fb00f574aa314b78", TextCaptureConfig.HorizontalContainer, false);
                break;

            case ConstantStrings.Vertical:
                textCaptureScan.CreateScanSession("1a5c7076358dda4060bcfc45fb00f574aa314b78", TextCaptureConfig.VerticalContainer, false);
                break;

            case ConstantStrings.Tyre:
                textCaptureScan.CreateScanSession("1a5c7076358dda4060bcfc45fb00f574aa314b78", TextCaptureConfig.Tire, false);
                break;

            default:

                break;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
      
    

      
    }
 
    private async void TextCaptureScan_OnScanResult(TextScanResult result)
    {
     await MainThread.InvokeOnMainThreadAsync(() =>
        {
            if(isScanflow)
            {

                MopupService.Instance.PushAsync(new TextCaptureResultPopup(result));
                btnDrawer.Text = "Capture";
                progressIndicator.IsRunning = false;
                btnDrawer.IsEnabled = true;
                isScanflow=false;
            }
           
        });
       
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        textCaptureScan.StopScanning();
       

    }
    private bool isScanning = false;
    

    private  void Button_Clicked(object sender, EventArgs e)
    {
       
        if (!isScanning)
        {
            isScanning = true;
            btnDrawer.IsEnabled = false;
            isScanflow = true;
            textCaptureScan.StartScanning();
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
            textCaptureScan.EnableTorch(false);

        }
        else
        {
            isTorch = true;
            torchImage.Source = "flashon";
            textCaptureScan.EnableTorch(true);
        }
    }
    
}

