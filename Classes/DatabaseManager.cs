/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to initialise all the manager classes and loads in all data from the database to be used within the application */

using Betawave.Classes;

public class DatabaseManager
{
    private DatabaseAccess dbAccess;

    // Manager instances

    private ArtistManager artistManager;
    private AlbumManager albumManager;
    private SongManager songManager;

    public DatabaseManager(DatabaseAccess access)
    {
        dbAccess = access;
        artistManager = new ArtistManager(dbAccess);
        songManager = new SongManager(dbAccess);
        albumManager = new AlbumManager(dbAccess);
    }

    // Initialize all data from database
    public async Task LoadInAllManagerClassData()
    {
        try
        {
            await artistManager.LoadArtists();
            await albumManager.LoadAlbums();
            await songManager.LoadSongsIntoProgram();
        }
        catch (Exception ex) 
        {
            Console.WriteLine("Failed to load data: " + ex.Message);
        }
    }
}

