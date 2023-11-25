using Android.Content;
using CommunityToolkit.Maui.Alerts;
using Mopups.Pages;
using Mopups.Services;
using Scanflow.BarcodeCapture.Maui;
using ScanflowMaui.CustomControl;

using System.Threading;
using System.Threading.Tasks;

namespace ScanflowMaui.Views;

public partial class BarcodeResultPopup : PopupPage
{

    public BarcodeResultPopup(string Result)
    {
        InitializeComponent();


        scanText.Text = Result;

    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();


    }
    private async void Copy_Tapped(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(scanText.Text);
        string text = "copied";
        var toast = Toast.Make(text);
        await toast.Show();
    }
    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }


}