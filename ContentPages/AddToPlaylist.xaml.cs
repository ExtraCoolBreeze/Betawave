/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the AddToPlaylist page and is to create and run the page as well as deal with simple interactions   */


namespace Betawave;

public partial class AddToPlaylist : ContentPage
{
    public AddToPlaylist()
    {
        InitializeComponent();
        GeneratePlaylistButtons();
    }

    private void GeneratePlaylistButtons()
    {
        var playlists = GetPlaylists();

        foreach (var playlist in playlists)
        {
            var button = new Button
            {
                Text = playlist.GetTitle(),
                Margin = new Thickness(5),
                BackgroundColor = Color.FromArgb("#333333"),
                TextColor = Color.FromArgb("#FFFFFF")
            };

            button.Clicked += Button_Clicked;

            // Storing the playlist in the button's CommandParameter for retrieval in the click event handler
            button.CommandParameter = playlist;

            PlaylistsStackLayout.Children.Add(button);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is BasePlaylist selectedPlaylist)
        {
            PlaylistButtonClicked(selectedPlaylist);
        }
    }

    private async void PlaylistButtonClicked(BasePlaylist selectedPlaylist)
    {
        Console.WriteLine($"Playlist Selected: {selectedPlaylist.GetTitle()}");
        await Shell.Current.GoToAsync($"playlistdetails?playlistId={selectedPlaylist.GetPlaylistId()}");
    }

    private List<BasePlaylist> GetPlaylists()
    {
        return new List<BasePlaylist>();
    }

    private async void OnAddToPlaylistClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlaylistView");
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainMenu");
    }
}