using Betawave.Classes;
using Betawave.ViewModels;

namespace Betawave;

public partial class CreateAccount : ContentPage
{
    public CreateAccount()
    {
        InitializeComponent();
        Appearing += OnAppearingHandler;
    }

    public void OnAppearingHandler(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CreateAccountViewModel;
        if (viewModel != null)
        {
            viewModel.Username = "";
            viewModel.Password = "";
        }
    }

    async void CABackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }
}
 