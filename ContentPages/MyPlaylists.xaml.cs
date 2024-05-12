/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the MyPlaylists page and is to create and run the page as well as deal with simple interactions   */

using Betawave.ViewModels;
namespace Betawave;

//Not testing as this is unused doe to cuts but leaving in for proof

//declaring class and inheriting
public partial class MyPlaylists : ContentPage
{
    //page constructor, initialising and binding
    public MyPlaylists(AudioViewModel audioViewModel)
    {
        InitializeComponent();
        BindingContext = audioViewModel;
    }

    //Methods for navigation buttons tied to navigation shell page
    async void CreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///UserCreatePlaylist");
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

    async void MainMenuButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainMenu");
    }


    async void LogoutButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }
}