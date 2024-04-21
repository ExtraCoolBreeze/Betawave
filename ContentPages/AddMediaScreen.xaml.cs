using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace Betawave;

public partial class AddMediaScreen : ContentPage
{

    public AddMediaScreen()
    {
        InitializeComponent();
    }





    async void DashboardButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AdminDashboard");
    }

    async void AddMediaScreenButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AddMediaScreen");
    }

    async void CreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CreatePlaylist");
    }

    async void LogoutButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }

    async void AddMediaButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AddAlbum");
    }

}