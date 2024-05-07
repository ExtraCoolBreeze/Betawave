/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the AdminDashboard page and is to create and run the page as well as deal with simple interactions   */

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