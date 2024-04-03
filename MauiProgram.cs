using Betawave;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace Betawave
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                });

#if DEBUG
    		builder.Logging.AddDebug();

            builder.Services.AddTransient<LoginScreen>();
            builder.Services.AddTransient<CreateAccount>();
            builder.Services.AddTransient<MainMenu>();
            builder.Services.AddTransient<CreatePlaylist>();
            builder.Services.AddTransient<AddToPlaylist>();
            builder.Services.AddTransient<MyPlaylists>();
            builder.Services.AddTransient<PlaylistView>();
            builder.Services.AddTransient<FavouritesView>();
            builder.Services.AddTransient<AlbumView>();
            builder.Services.AddTransient<AdminDashboard>();
            builder.Services.AddTransient<AddMediaScreen>();
            builder.Services.AddTransient<PlayQueue>();

#endif

            return builder.Build();
        }
    }
}
