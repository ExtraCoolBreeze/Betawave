/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the CreatePlaylistAdmin page and is to create and run the page as well as deal with simple interactions  */

namespace Betawave
{
    public partial class CreatePlaylistAdmin : ContentPage
    {
        //page constructor
        public CreatePlaylistAdmin()
        {
            InitializeComponent();
        }

        //Confirm and navigate buttons
        async void CreatePlaylistAdmin_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///PlaylistView");
        }

        public async void BackButtonAdmin_Clicked(object sender, EventArgs e)
        {
          await Shell.Current.GoToAsync("//AdminDashboard");
        }
    }
}
