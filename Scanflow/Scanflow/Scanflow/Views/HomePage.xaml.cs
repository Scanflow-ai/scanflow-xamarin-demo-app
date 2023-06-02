
using Scanflow.Models;
using Scanflow.ViewModels;
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
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
            BindingContext = new HomePageViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RequestCameraPermission();
        }
        async void RequestCameraPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.Camera>();
            }

        }
        private async void MainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null) return;
            if(e.CurrentSelection.Count > 0)
            {
                var result = e.CurrentSelection.FirstOrDefault() as ScanResult;
                await Navigation.PushAsync(new ScanViewPage(result));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}