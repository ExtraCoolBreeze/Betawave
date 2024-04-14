using Betawave;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using NAudio.Wave;


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
                .UseMauiCommunityToolkitMediaElement()
                
                

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                });


            builder.Services.AddSingleton<Player>();

#if DEBUG
            builder.Logging.AddDebug();

            //This is part of the application builder and adding these content pages here allow for dependency injection
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
