﻿namespace Betawave
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //This is the routing registry for navigating between pages in the AppShell 
            Routing.RegisterRoute(nameof(LoginScreen), typeof(LoginScreen));
            Routing.RegisterRoute(nameof(CreateAccount), typeof(CreateAccount));
            Routing.RegisterRoute(nameof(MainMenu), typeof(MainMenu));
            Routing.RegisterRoute(nameof(CreatePlaylist), typeof(CreatePlaylist));
            Routing.RegisterRoute(nameof(MyPlaylists), typeof(MyPlaylists));
            Routing.RegisterRoute(nameof(PlayQueue), typeof(PlayQueue));
        }
    }
}
