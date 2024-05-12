/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the AlbumView page and is to create and run the page as well as deal with simple interactions. Tis page is currently unused   */

using Betawave.ViewModels;

namespace Betawave;

//declaring class and inheriting from content page
public partial class AlbumView : ContentPage
{
    //class constructor and initialising, passing the view model and binding
	public AlbumView(AudioViewModel audioViewModel)
	{
		InitializeComponent();
        BindingContext = audioViewModel;
	}

    //Methods for navigation linked to buttons pressed
    async void AVCreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///UserCreatePlaylist");
    }

    async void AVmyPlaylistsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlaylistView");
    }

    async void AVFavouritesButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///FavouritesView");
    }

    async void AVPlayQueueButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlayQueue");
    }

    async void MainMenuButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainMenu");
    }

    async void AVBackButton_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("///LoginScreen");
    }


}