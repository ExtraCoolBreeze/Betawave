/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to initialise all the manager classes and loads in all data from the database to be used within the application */

using Betawave.Classes;

public class DatabaseManager
{
    //declaring classes
    private DatabaseAccess dbAccess;
    private ArtistManager artistManager;
    private AlbumManager albumManager;
    private SongManager songManager;
    private ErrorLogger errorLogger;

    //class constructor
    public DatabaseManager(DatabaseAccess access)
    {   //initialising with non null data
        dbAccess = access;
        artistManager = new ArtistManager(dbAccess);
        songManager = new SongManager(dbAccess);
        albumManager = new AlbumManager(dbAccess);
        errorLogger = new ErrorLogger("C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\BetawaveErrorLog.txt");
    }

    // Initialize all data from database
    public async Task LoadInAllManagerClassData()
    {
        try
        {
            await artistManager.LoadArtists();
            await albumManager.LoadAlbums();
            await songManager.LoadSongsIntoProgram();
        }//catching errors
        catch (Exception ex) 
        {
            errorLogger.LogError(ex);
        }
    }
}

