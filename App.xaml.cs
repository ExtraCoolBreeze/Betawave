using Betawave;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using NAudio.Wave;

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
