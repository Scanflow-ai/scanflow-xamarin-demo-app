using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using Scanflow.TextCapture.Maui;

namespace ScanflowMaui;

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
			});
#if ANDROID
       builder.UseScanflow();
#endif



		return builder.Build();
	}
}
