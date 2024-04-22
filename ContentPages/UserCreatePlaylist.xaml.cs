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
        await Shell.Current.GoToAsync("//AdminDashboard");
    }
}