
/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the LoginScreen page and is to create and run the page as well as deal with simple interactions   */

using Betawave.ViewModels;

namespace Betawave;

public partial class LoginScreen : ContentPage
{

    public LoginScreen()
    {
        InitializeComponent();
        Appearing += OnAppearingHandler;
    }

    private void OnAppearingHandler(object sender, EventArgs e)
    {
        var viewModel = BindingContext as LoginViewModel;
        if (viewModel != null)
        {
            viewModel.Username = "";  // Clear the username
            viewModel.Password = "";  // Clear the password
        }
    }

    async void CreateAccountButton_Clicked(object sender, EventArgs e)
    {
        // Navigate to the CreateAccount page
        await Shell.Current.GoToAsync("///CreateAccount");
    }

    private void LSCloseButton_Clicked(object sender, EventArgs e)
    {
        // Close the application
        Application.Current.Quit();
    }
}
