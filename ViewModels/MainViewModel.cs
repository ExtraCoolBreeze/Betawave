using System.Windows.Input;
using Betawave.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Betawave.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private readonly DatabaseAccess dbAccess = new DatabaseAccess();
        public event PropertyChangedEventHandler PropertyChanged;


        public ICommand LoginCommand { get; private set; }

        public MainViewModel()
        {
            LoginCommand = new Command(ExecuteLoginCommand);
        }


        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public async void ExecuteLoginCommand()
        {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Both username and password are required.", "OK");
                return;
            }

            if (await dbAccess.ValidateUser(Username, Password))
            {
                // Check if the logged in user is an admin
                if (dbAccess.IsAdmin(Username)) // Removed the incorrect assignment (=) and used the correct comparison
                {
                    // If user is an admin, navigate to an admin-specific page or perform admin-specific actions
                    await Shell.Current.GoToAsync("///AdminDashboard");
                }
                else
                {
                    // If user is not an admin, navigate to the main menu or standard user page
                    await Shell.Current.GoToAsync("///MainMenu");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Username or password incorrect.", "OK");
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}