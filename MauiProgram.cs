/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class is the main program class and is used for initialising and building the application  */

using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Betawave.ViewModels;


namespace Betawave
{

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            //adding toolkits to builder
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                
                
                //adding custom fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                });


#if DEBUG
            builder.Logging.AddDebug();

            //This is part of the application builder and adding these content pages here allow for dependency injection
            builder.Services.AddSingleton<Player>();
            builder.Services.AddSingleton<AudioViewModel>();

            builder.Services.AddTransient<LoginScreen>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<CreateAccount>();
            builder.Services.AddTransient<CreateAccountViewModel>();

            builder.Services.AddTransient<MainMenu>();
            builder.Services.AddTransient<MainMenuViewModel>();

            builder.Services.AddTransient<CreatePlaylistAdmin>();

            builder.Services.AddTransient<AddToPlaylist>();

            builder.Services.AddTransient<MyPlaylists>();

            builder.Services.AddTransient<PlaylistView>();

            builder.Services.AddTransient<FavouritesView>();

            builder.Services.AddTransient<AlbumView>();


            builder.Services.AddTransient<AdminDashboard>();

            builder.Services.AddTransient<AddMediaScreen>();
            builder .Services.AddTransient<AddMediaViewModel>();

            builder.Services.AddTransient<PlayQueue>();

            builder.Services.AddTransient<AddAlbum>();
            builder.Services.AddTransient<AddAlbumViewModel>();

            builder.Services.AddTransient<UserCreatePlaylist>();
#endif

            return builder.Build();
        }
    }
}
