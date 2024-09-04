using CommunityToolkit.Maui.Alerts;
using Mopups.Services;
using Scanflow.BarcodeCapture.Maui;
using Scanflow.BarcodeCapture.Maui.Models;
using ScanflowMauiDemoApp.Models;

namespace ScanflowMauiDemoApp.Views;

public partial class ScanViewPage : ContentPage
{
    public bool isTorch = true;
    public bool isScanflow = false;

    // for android -> com.scanflowdemo.android
    public string licenseKey = "1e97efbd3817dbd366d829399dc525e4f3e5e958";

    // for iOS -> com.scanflowdemo.ios
    //public string licenseKey = "4d433e83c00eb3b21afec1daa226d0d90ee7284a";

    public ScanViewPage(ScanSelect result)
    {
        try
        {
            InitializeComponent();
            scanTitle.Text = result.Name;
            barcodeCaptureScan.CreateScanSession(licenseKey, DecodeConfig.Any, 0.2f);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void Setting_Tapped(object sender, EventArgs e)
    {
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
        var ScannedResult = result as ScanResult;

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {

            if (!isScanflow)
            {
                isScanflow = true;

                await Toast.Make(ScannedResult.Text, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();

                isScanflow = false;
            }

        });
    }

    private void btnRetryValidation(System.Object sender, System.EventArgs e)
    {
        barcodeCaptureScan.RetryValidationResult(licenseKey);
    }

    private async void BarcodeCaptureScan_OnLicenceOnFailureWithError(string error)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Toast.Make(error).Show();
        });
    }

    private async void BarcodeCaptureScan_OnLicenceOnSuccessWithResponse(string response)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Toast.Make(response).Show();
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        barcodeCaptureScan.StopScanning();
    }
}