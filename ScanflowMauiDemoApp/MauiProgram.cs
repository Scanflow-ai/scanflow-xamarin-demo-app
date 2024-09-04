using CommunityToolkit.Maui;
using Mopups.Hosting;
using Scanflow.BarcodeCapture.Maui;

namespace ScanflowMauiDemoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMopups()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit()
                .ConfigureMauiHandlers(handler =>
                {
                    InitializeScanflow.UseScanflow(handler);
                });

            return builder.Build();
        }
    }
}
