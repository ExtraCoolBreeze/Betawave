using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace Betawave;

public partial class AddMediaScreen : ContentPage
{
    private Player player;

    public AddMediaScreen()
	{
		InitializeComponent();
        var mediaElement = new MediaElement(); // This is a dummy MediaElement for demonstration
        player = new Player(mediaElement); // Initialize the Player instance
    }

    async void DashboardButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AdminDashboard");
    }

    async void AddMediButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AddMediaScreen");
    }

    async void CreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CreatePlaylist");
    }

    async void LogoutButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }

    private void PlayPauseButton_Clicked(object sender, EventArgs e)
    {
        // Toggle play/pause based on the player's state
        if (player.GetMediaElement().CurrentState == MediaElementState.Playing)
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