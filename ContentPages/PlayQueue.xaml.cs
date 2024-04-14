namespace Betawave;

public partial class PlayQueue : ContentPage
{
    public PlayQueue()
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

    async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }
}