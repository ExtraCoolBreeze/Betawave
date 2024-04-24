using Betawave.ViewModels;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace Betawave;

public partial class PlaylistView : ContentPage
{

    public PlaylistView(AudioViewModel audioViewModel)
	{
		InitializeComponent();
        BindingContext = audioViewModel;
    }

    async void CreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///UserCreatePlaylist");
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
	async void BackButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///LoginScreen");
	}
}