using Betawave.Classes;
using System;
using System.Threading.Tasks;

public class DatabaseManager
{
    private DatabaseAccess dbAccess;
    public Account CurrentUser { get; private set; }
    public int CurrentUserRole { get; private set; }

    // Manager instances

    private SongManager songManager;
    private AlbumManager albumManager;
    private ArtistManager artistManager;
    private PlaylistManager playlistManager;

    public DatabaseManager(DatabaseAccess access)
    {
        dbAccess = access;
        songManager = new SongManager(dbAccess, artistManager);
        albumManager = new AlbumManager(dbAccess, artistManager);
        artistManager = new ArtistManager(dbAccess);
        playlistManager = new PlaylistManager(dbAccess, artistManager);
    }

    // Initialize all data from database
    public async Task LoadAllDataAsync()
    {
        await artistManager.LoadArtistsAsync();
        await songManager.LoadSongsIntoProgramAsync();
        await albumManager.LoadAlbumsAsync();
        await playlistManager.LoadPlaylistsAsync();
    }

    // Handling user login and role retrieval
    public async Task<bool> LoginUserAsync(string username, string password)
    {
        if (await dbAccess.ValidateUser(username, password))
        {
            CurrentUser = await dbAccess.GetUserByUsername(username);
            if (CurrentUser != null)
            {
                CurrentUserRole = dbAccess.GetRoleForAccountId(CurrentUser.GetAccountId());
                return true;
            }
        }
        return false;
    }

    private int GetRoleForAccount(int accountId)
    {
        return dbAccess.GetRoleForAccountId(accountId); // Adjust this to fetch directly or process in the manager
    }

    public async Task<bool> ValidateUser(string username, string password)
    {
        return await dbAccess.ValidateUser(username, password);
    }

    public async Task<bool> IsAdmin(string username)
    {
        return dbAccess.IsAdmin(username);
    }

    public void PrintUserDetails()
    {
        Console.WriteLine($"User: {CurrentUser.GetUsername()}, Role ID: {CurrentUserRole}");
    }


}

