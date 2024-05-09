/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to initialise all the manager classes and load in all data from the database to be used within the application */

using Betawave.Classes;

public class DatabaseManager
{
    private DatabaseAccess dbAccess;
    public Account CurrentUser { get; private set; }
    public int CurrentUserRole { get; private set; }

    // Manager instances

    private ArtistManager artistManager;
    private AlbumManager albumManager;
    private SongManager songManager;
    private PlaylistManager playlistManager;

    public DatabaseManager(DatabaseAccess access)
    {
        dbAccess = access;
        artistManager = new ArtistManager(dbAccess);
        songManager = new SongManager(dbAccess, artistManager);
        albumManager = new AlbumManager(dbAccess, artistManager);
        playlistManager = new PlaylistManager(dbAccess, artistManager);
    }

    // Initialize all data from database
    public async Task LoadInAllManagerClassData()
    {
        try
        {
            await artistManager.LoadArtists();
            await albumManager.LoadAlbumsAsync();
            await songManager.LoadSongsIntoProgram();
            await playlistManager.LoadPlaylistsAsync();
        }
        catch (Exception ex) 
        {
            Console.WriteLine("Failed to load data: " + ex.Message);
        }
    }

    // Handling user login and role retrieval
    public async Task<bool> LoginUser(string username, string password)
    {
        if (await dbAccess.ValidateUser(username, password))
        {
            CurrentUser = await dbAccess.GetUserByUsername(username);
                return true;
        }
        return false;
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

