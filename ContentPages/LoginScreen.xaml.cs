using Betawave.Classes; // Ensure this using directive is added to access the Account and DatabaseAccess classes

namespace Betawave;

public partial class LoginScreen : ContentPage
{
    private DatabaseAccess dbAccess = new DatabaseAccess(); // Assuming DatabaseAccess is needed for account operations

    public LoginScreen()
    {
        InitializeComponent();
    }

    async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string enteredUsername = usernameEntry.Text;
        string enteredPassword = passwordEntry.Text;

        // Check if the username or password fields are empty or contain only whitespace
        if (string.IsNullOrWhiteSpace(enteredUsername) || string.IsNullOrWhiteSpace(enteredPassword))
        {
            // Inform the user that both fields are required
            await DisplayAlert("Error", "Both username and password are required.", "OK");
            return; // Exit the method early if validation fails
        }

        // Proceed with the login attempt if validation passes
        if (dbAccess.ValidateUser(enteredUsername, enteredPassword))
        {
            await Shell.Current.GoToAsync("///MainMenu"); // Adjust navigation as needed
        }
        else
        {
            // Inform the user about incorrect credentials
            await DisplayAlert("Error", "Username or password incorrect.", "OK");
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
