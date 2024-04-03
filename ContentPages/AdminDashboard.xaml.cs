namespace Betawave;

public partial class AdminDashboard : ContentPage
{
	public AdminDashboard()
	{
        InitializeComponent();
	}

    async void ADDashboardButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AdminDashboard");
    }

    async void ADmyPlaylistsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlaylistView");
    }

    async void ADCreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PlayQueue");
    }

    async void ADBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }
}