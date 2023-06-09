using CommunityToolkit.Maui.Alerts;
using Mopups.Pages;
using Mopups.Services;
using Scanflow.TextCapture.Maui;
using ScanflowMaui.CustomControl;

using System.Threading;
using System.Threading.Tasks;

namespace ScanflowMaui.Views;

public partial class TextCaptureResultPopup : PopupPage
{
   
    public TextCaptureResultPopup(TextScanResult Result)
	{
		InitializeComponent();
     
      
        scanImage.Source = Result.Image;
        scanText.Text = Result.Text;
        //scanText.Text = result.Name;
        //scanImage.Source = result.Image;
       

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