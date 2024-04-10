using Betawave.Classes;
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
        // Assuming you have a method GetPlaylists that returns a list of playlists
        // This is just an example. Replace with your actual method to fetch playlists
        var playlists = GetPlaylists();

        foreach (var playlist in playlists)
        {
            var button = new Button
            {
                Text = playlist.GetTitle(), // Display the playlist title on the button
                Margin = new Thickness(5),
                BackgroundColor = Color.FromArgb("#333333"),
                TextColor = Color.FromArgb("#FFFFFF")
            };

            // Set the event handler for the button click
            button.Clicked += (sender, args) => PlaylistButtonClicked(playlist);

            // Add the button to the stack layout
            PlaylistsStackLayout.Children.Add(button);
        }
    }

    async void PlaylistButtonClicked(BasePlaylist selectedPlaylist)
    {
        // Logic to handle playlist selection
        // You might set the selected playlist to a property, navigate to another page, etc.
        Console.WriteLine($"Playlist Selected: {selectedPlaylist.GetTitle()}");
        await Shell.Current.GoToAsync($"playlistdetails?playlistId={selectedPlaylist.GetPlaylistId()}");
    }

    // Example method to fetch playlists
    // Replace this with your actual implementation
    private List<BasePlaylist> GetPlaylists()
    {
        // This should return a list of your playlists
        // For demonstration, returning an empty list
        return new List<BasePlaylist>();
    }

    async void OnAddToPlaylistClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AddToPlaylist");
    }

    async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AddToPlaylist");
    }
}