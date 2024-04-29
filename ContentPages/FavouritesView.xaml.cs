using Betawave.ViewModels;

namespace Betawave;

public partial class FavouritesView : ContentPage
{
	public FavouritesView(AudioViewModel audioViewModel)
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

    async void MainMenuButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainMenu");
    }

  
    async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }
}