/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the MainMenu page and is to create and run the page as well as deal with simple interactions   */

using Betawave.ViewModels;

namespace Betawave
{
    //inheriting from content page
    public partial class MainMenu : ContentPage
    {
        //class constructor, initialising and binding to page
        public MainMenu(MainMenuViewModel mainMenuViewModel)
        {
            InitializeComponent();
            BindingContext = mainMenuViewModel;
        }

        // methods for navigation buttons
        async void MMCreatePlaylistButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///UserCreatePlaylist");
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


        async void MainMenuButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainMenu");
        }


        async void MMLogoutButton_Clicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("///LoginScreen");
        }
    }
}
