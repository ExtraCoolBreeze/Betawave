namespace BetaWaveMultiplatform;

public partial class MyPlaylists : ContentPage
{
	public MyPlaylists()
	{
		InitializeComponent();
	}


    async void MPCreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CreatePlaylist");
    }

    async void MPmyPlaylistsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlaylistView");
    }

    async void MPFavouritesButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///FavouritesView");
    }

    async void MPPlayQueueButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlayQueue");
    }
	async void MPBackButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///MainMenu");
	}
}