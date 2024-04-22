namespace Betawave
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
            Routing.RegisterRoute(nameof(CreatePlaylistAdmin), typeof(CreatePlaylistAdmin));
            Routing.RegisterRoute(nameof(MyPlaylists), typeof(MyPlaylists));
            Routing.RegisterRoute(nameof(PlaylistView), typeof(PlaylistView));
            Routing.RegisterRoute(nameof(FavouritesView), typeof(FavouritesView));
            Routing.RegisterRoute(nameof(PlayQueue), typeof(PlayQueue));
            Routing.RegisterRoute(nameof(AlbumView), typeof(AlbumView));
            Routing.RegisterRoute(nameof(AdminDashboard), typeof(AdminDashboard));
            Routing.RegisterRoute(nameof(AddMediaScreen), typeof(AddMediaScreen));
            Routing.RegisterRoute(nameof(AddAlbum), typeof(AddAlbum));
            Routing.RegisterRoute(nameof(AddToPlaylist), typeof(AddToPlaylist));
            Routing.RegisterRoute(nameof(UserCreatePlaylist), typeof(UserCreatePlaylist));
        }
    }
}
