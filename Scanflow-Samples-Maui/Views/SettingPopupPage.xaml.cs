using Mopups.Pages;
using Mopups.Services;

namespace ScanflowMaui.Views;

public partial class SettingPopupPage : PopupPage
{
	public SettingPopupPage()
	{
		InitializeComponent();
	}
    private async void Cancel_Clicked(object sender, EventArgs e)
    {
      await  MopupService.Instance.PopAsync();
    }
    private void CheckBox_CheckChanged(object sender, EventArgs e)
    {

    }
}