using Betawave.Classes;
using Org.BouncyCastle.Utilities;
public class BasePlaylist
{
    private int pkplaylistId;
    private string PlaylistTitle;
    private string isQueue;
    private int  fkaccountId;
    private string isFavourite;
    private List<Playlist_Track> PlaylistTracks = new List<Playlist_Track>();
    private List<Song> PlaylistSongs;

    public BasePlaylist()
    {
        PlaylistTitle = "";
        pkplaylistId = 0;
        isQueue = "";
        isFavourite = "";
        isFavourite = "";
        PlaylistSongs = new List<Song>();
}

    public void SetPlaylistId(int userinput)
    {
        pkplaylistId = userinput;
    }

    public int GetPlaylistId()
    { 
        return pkplaylistId;
    }

    public void SetTitle(string userInput)
    {
        PlaylistTitle = userInput;
    }

    public string GetTitle()
    {
        return PlaylistTitle;
    }

    public void SetQueue(string inputQueue)
    {
        isQueue = inputQueue;
    }

    public string GetQueue()
    {
        return isQueue;
    }

    public void SetFavourite(string fave)
    {
        isFavourite = fave;
    }

    public string GetFavourite()
    {
        return isFavourite;
    }


    public void SetAccountId(int accountid)
    {
        fkaccountId = accountid;
    }

    public int GetAccountId()
    { 
        return fkaccountId;
    }

    //--------------------------------Start of functions to store the list of playlist songs

    public List<Song> GetPlaylistSongs()
    {
        // You could add logic here to modify what is returned
        return PlaylistSongs;
    }

    public void SetPlaylistSongs(List<Song> value)
    {
        PlaylistSongs = value;
    }

    public void AddPlaylistSong(Song song)
    {
        if (song == null)
        {
            throw new ArgumentNullException(nameof(song), "Cannot add a null song to the playlist.");
        }

        PlaylistSongs.Add(song);
    }


    //--------------------------------End of functions to store the list of playlist songs



/*    public void AddToPlaylist(Playlist_Track track)
    {
        PlaylistTracks.Add(track);
    }

    public void RemoveFromPlaylist(Playlist_Track track)
    {
        PlaylistTracks.Remove(track);
    }

    public List<Playlist_Track> GetTracks()
    {
        return PlaylistTracks;
    }

    public List<string> GetTrackLocations()
    {
        return PlaylistTracks.Select(track => track.GetTrackUri()).ToList();
    }*/

    public void PrintPlaylistDetails()
    {
        Console.WriteLine($"Playlist Title: {GetTitle()}");
        foreach (var song in GetPlaylistSongs())
        {
            Console.WriteLine($"Song: {song.GetName()}");
        }
    }
}
