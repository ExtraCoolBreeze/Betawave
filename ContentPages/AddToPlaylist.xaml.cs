/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the AddToPlaylist page and is to create and run the page as well as deal with simple interactions. It is incomplete and not in use.   */


namespace Betawave;

public partial class AddToPlaylist : ContentPage
{
    public AddToPlaylist()
    {
        InitializeComponent();
    }

    //This function returns a list of playlist objects
    public List<BasePlaylist> GetPlaylists()
    {
        return new List<BasePlaylist>();
    }

    //Navigation buttons
    async void OnAddToPlaylistClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlaylistView");
    }

    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainMenu");
    }
}