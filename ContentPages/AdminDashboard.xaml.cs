using Betawave.ViewModels;

namespace Betawave;

public partial class AdminDashboard : ContentPage
{
	public AdminDashboard(AudioViewModel audioViewModel)
	{
        InitializeComponent();
        BindingContext=audioViewModel;
	}

    async void ADDashboardButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AdminDashboard");
    }

    async void ADmyAddMediaButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AddMediaScreen");
    }

    async void ADCreatePlaylistButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CreatePlaylistAdmin");
    }

    async void ADBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }
}