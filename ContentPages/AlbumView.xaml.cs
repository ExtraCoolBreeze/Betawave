namespace Betawave;

public partial class AlbumView : ContentPage
{
	public AlbumView()
	{
		InitializeComponent();
	}

    async void AVCreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///UserCreatePlaylist");
    }

    async void AVmyPlaylistsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlaylistView");
    }

    async void AVFavouritesButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///FavouritesView");
    }

    async void AVPlayQueueButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlayQueue");
    }

    async void AVBackButton_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("///LoginScreen");
    }


}