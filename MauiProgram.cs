using Microsoft.Extensions.Logging;
using Parcial_Moviles.Services;
using Parcial_Moviles.ViewModels;
using Parcial_Moviles.Views;

namespace Parcial_Moviles
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            
            builder.Services.AddHttpClient<IApiService, ApiService>();

             
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<DetalleViewModel>();
            
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<DetallePage>();

            return builder.Build();
        }
    }
}
