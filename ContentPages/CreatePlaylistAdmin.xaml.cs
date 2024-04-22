using Betawave.Classes;
using Microsoft.Maui.Controls;
using System;

namespace Betawave
{
    public partial class CreatePlaylistAdmin : ContentPage
    {

        public CreatePlaylistAdmin()
        {
            InitializeComponent();
        }

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
