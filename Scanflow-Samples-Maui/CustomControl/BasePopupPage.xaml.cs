using System.Windows.Input;

namespace ScanflowMaui.CustomControl;

public partial class BasePopupPage : ContentPage
{
	public BasePopupPage()
	{
		InitializeComponent();
        this.BackgroundColor = Color.FromArgb("#80000000");
    }
    public static readonly BindableProperty PopupContentProperty = BindableProperty.Create(
        propertyName: nameof(PopupContent),
        returnType: typeof(View),
        declaringType: typeof(BasePopupPage),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: PopupContentPropertyChanged);

    private static void PopupContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var controls = (BasePopupPage)bindable;
        if (newValue != null)
            controls.ContentView.Content = (View)newValue;
    }

    public View PopupContent
    {
        get => (View)GetValue(PopupContentProperty);
        set { SetValue(PopupContentProperty, value); }
    }

    public ICommand PopModelCommand => new Command(async () =>
    {
        await App.Current.MainPage.Navigation.PopModalAsync();
    });
}