/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the UserCreatePlaylist page and is to create and run the page as well as deal with simple interactions   */

namespace Betawave;

public partial class UserCreatePlaylist : ContentPage
{
    public UserCreatePlaylist()
    {
        InitializeComponent();
    }

    async void CreatePlaylist_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlaylistView");
    }

    public async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainMenu");
    }
}