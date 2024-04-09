using Betawave;
using Betawave.Classes;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace Betawave;

public partial class MainMenu : ContentPage
{
    private Player player;

    public MainMenu()
	{
		InitializeComponent();
        var mediaElement = new MediaElement(); // This is a dummy MediaElement for demonstration
        player = new Player(mediaElement); // Initialize the Player instance

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

    private void PlayPauseButton_Clicked(object sender, EventArgs e)
    {
        // Toggle play/pause based on the player's state
        if (player.MediaElement.CurrentState == MediaElementState.Playing)
        {
            player.PauseMusic();
            // Update the Play button's icon to show "play"
        }
        else
        {
            player.PlayMusic();
            // Update the Play button's icon to show "pause"
        }
    }

    private void SkipToNextButton_Clicked(object sender, EventArgs e)
    {
        player.PlayNextTrack();
        // Update UI as necessary, e.g., track name label
    }

    // This is the new event handler for the volume control Slider's ValueChanged event.
    private void OnVolumeSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        player.SetVolume(e.NewValue);
    }
}