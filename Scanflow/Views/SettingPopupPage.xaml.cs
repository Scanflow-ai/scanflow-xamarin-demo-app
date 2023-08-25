using Plugin.InputKit.Shared.Controls;
using Rg.Plugins.Popup.Extensions;
using Scanflow.Models;
using Scanflow.TextCapture.Xamarin.Forms;
using Scanflow.TextCapture.Xamarin.Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace Scanflow.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private int selectedResolutionIndex;
        public SettingPopupPage (CameraPreview cameraPreview)
		{		
            InitializeComponent();
            camera=cameraPreview;
          
		}
        public CameraPreview camera;

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private void RadioButtonGroupView_SelectedItemChanged(object sender, EventArgs e)
        {
            RadioButtonGroupView result = (RadioButtonGroupView)sender;
            selectedResolutionIndex = result.SelectedIndex;        
           
        }

        private async void Apply_Clicked(object sender, EventArgs e)
        {
            switch (selectedResolutionIndex)
            {
                case 0:
                    camera.SetCameraResolution(CameraResolutionConfig.Sd480p);
                    break;
                case 1:
                    camera.SetCameraResolution(CameraResolutionConfig.Hd720p);
                    break;
                case 2:
                    camera.SetCameraResolution(CameraResolutionConfig.FullHd1080p);
                    break;
                default:
                    camera.SetCameraResolution(CameraResolutionConfig.Uhd4k);
                    break;
            }
            await Navigation.PopPopupAsync();
        }

        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {

        }
    }
}