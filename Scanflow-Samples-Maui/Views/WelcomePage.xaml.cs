using System.Collections.ObjectModel;

namespace ScanflowMaui.Views;

public partial class WelcomePage : ContentPage
{
  
	public WelcomePage()
	{
		InitializeComponent();
        

    }
    private void MainPageTapped(object sender, EventArgs e)
    {
      //  Preferences.Default.Set("IsLandingPage", true);
   
        Navigation.PushAsync(new HomePage());

    }
    private void CarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
    {
        var view = sender as CarouselView;
        if (view.Position == 0)
        {
            img.Source = "skip1";
        }
        else if (view.Position == 1)
        {
            img.Source = "skip2";
        }
        else if (view.Position == 2)
        {
            img.Source = "skip3";
        }
        else
        {
            img.Source = "skip4";
        }
    }
}