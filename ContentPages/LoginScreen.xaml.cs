
/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the LoginScreen page and is to create and run the page as well as deal with simple interactions   */

using Betawave.ViewModels;

namespace Betawave;

//declaring class and inheriting
public partial class LoginScreen : ContentPage
{
    //initialising and subscribing to event
    public LoginScreen()
    {
        InitializeComponent();
        Appearing += OnAppearingHandler;
    }

    //Method to clear the user text entry boxes for logging in
    private void OnAppearingHandler(object sender, EventArgs e)
    {
        LoginViewModel viewModel = BindingContext as LoginViewModel;
        if (viewModel != null)
        {
            viewModel.Username = "";
            viewModel.Password = "";
        }
    }

    //navigation button
    async void CreateAccountButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CreateAccount");
    }

    //method for button click to close application
    private void LSCloseButton_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}
