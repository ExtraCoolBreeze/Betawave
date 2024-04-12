using Betawave;
using Betawave.Classes;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace Betawave
{
    public partial class MainMenu : ContentPage
    {
        private Player player;

        public MainMenu()
        {
            InitializeComponent();
           // player = new Player();
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

        async void MMtestButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MyPlaylists");
        }

        private void PlayPauseButton_Clicked(object sender, EventArgs e)
        {
            if (player.IsPlaying())
            {
                player.PauseMusic();
                // Update the Play button's icon to show "play"
            }
            else
            {
                player.LoadAndPlayMusic("Resources/Music/CoralFang/03DieOnARope.m4a");
                // Update the Play button's icon to show "pause"
            }
        }

        private void SkipToNextButton_Clicked(object sender, EventArgs e)
        {
            player.PlayNextTrack();
            // Update UI as necessary, e.g., track name label
        }

        private void ShuffleToggle_Clicked(object sender, EventArgs e)
        {
            // Toggle the shuffle state
            player.SetShuffle(!player.GetShuffle());

            // Update UI to reflect the current shuffle state using the new getter method
            Shuffle.Source = player.GetShuffle() ? "shuffle_enabled_icon.png" : "shuffle_disabled_icon.png";
        }

        private void OnVolumeSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            player.SetVolume(e.NewValue);
        }
    }
}
