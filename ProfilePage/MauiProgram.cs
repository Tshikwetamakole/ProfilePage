using ProfilePage.Services;

namespace ProfilePage;

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

        // Register services
        builder.Services.AddSingleton<ProfileService>();

        // Register pages
        builder.Services.AddSingleton<Views.ProfilePage>();
        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
    }
}