using ScanflowMauiDemoApp.Models;
using ScanflowMauiDemoApp.ViewModels;

namespace ScanflowMauiDemoApp.Views;

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
        try
        {
            if (e.CurrentSelection == null) return;
            if (e.CurrentSelection.Count > 0)
            {
                var result = e.CurrentSelection.FirstOrDefault() as ScanSelect;
                await Navigation.PushAsync(new ScanViewPage(result));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}