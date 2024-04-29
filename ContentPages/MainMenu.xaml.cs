using Betawave;
using Betawave.Classes;
using Betawave.ViewModels;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace Betawave
{
    public partial class MainMenu : ContentPage
    {

        public MainMenu(MainMenuViewModel mainMenuViewModel)
        {
            InitializeComponent();
            BindingContext = mainMenuViewModel;
        }



        async void MMCreatePlaylistButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///UserCreatePlaylist");
        }

        async void MMmyPlaylistsButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///PlaylistView");
        }

        async void MMFavouritesButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///FavouritesView");
        }

        async void MMPlayQueueButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///PlayQueue");
        }


        async void MainMenuButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainMenu");
        }


        async void MMLogoutButton_Clicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("///LoginScreen");
        }
    }
}
