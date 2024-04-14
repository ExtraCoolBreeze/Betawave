using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
namespace Betawave;

public partial class MyPlaylists : ContentPage
{

    public MyPlaylists()
    {
        InitializeComponent();
    }


    async void CreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CreatePlaylist");
    }

    async void myPlaylistsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlaylistView");
    }

    async void FavouritesButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///FavouritesView");
    }

    async void PlayQueueButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlayQueue");
    }
    async void LogoutButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainMenu");
    }
}