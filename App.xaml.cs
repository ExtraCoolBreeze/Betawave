using CommunityToolkit.Maui.Views;

namespace Betawave
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }

}
