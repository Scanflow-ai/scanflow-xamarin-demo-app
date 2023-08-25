using Rg.Plugins.Popup.Pages;
using Scanflow.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scanflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextCaptureResultPopup : PopupPage
    {
        public TextCaptureResultPopup(Scanflow.TextCapture.Xamarin.Forms.Models.ScanResult Result)
        {
            InitializeComponent();
            scanImage.Source = Result.Image;
            scanText.Text = Result.Text;
            BackgroundColor = Color.Transparent;
        }
        private async void Copy_Tapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(scanText.Text);
            DependencyService.Get<IToast>().Show("Copied");
        }
        protected override bool OnBackgroundClicked()
        {
            // Close the popup when the background is clicked
            return base.OnBackgroundClicked();
        }
    }
}