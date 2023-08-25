using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scanflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }
        private void MainPageTapped(object sender, EventArgs e)
        {
            Application.Current.Properties["IsLandingPage"] = true;
            Navigation.PushAsync(new HomePage());
        }

        private void CarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            var view = sender as CarouselView;
            if (view.Position == 0)
            {
                imgView.Source = "resource://Scanflow.Resources.skip1.svg";
            }
            else if (view.Position == 1)
            {
                imgView.Source = "resource://Scanflow.Resources.skip2.svg";
            }
            else if (view.Position == 2)
            {
                imgView.Source = "resource://Scanflow.Resources.skip3.svg";
            }
            else
            {
                imgView.Source = "resource://Scanflow.Resources.skip4.svg";
            }
        }
    }
}