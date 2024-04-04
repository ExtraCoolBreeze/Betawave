using Betawave;
using Betawave.Classes;
using CommunityToolkit.Maui;

namespace Betawave;

public partial class MainMenu : ContentPage
{

    public MainMenu()
	{
		InitializeComponent();

    }

    async void MMCreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CreatePlaylist");
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

    async void MMLogoutButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }

    // This is the new event handler for the volume control Slider's ValueChanged event.
    private void OnVolumeSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Assuming your MediaElement has an x:Name of "musicPlayer" as per the XAML update.
        return;
    }
}