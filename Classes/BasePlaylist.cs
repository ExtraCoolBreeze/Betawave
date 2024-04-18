using Betawave.Classes;
using Org.BouncyCastle.Utilities;
public class BasePlaylist
{
    private int pkplaylist_id;
    private string _title;
    private string _queue;
    private int  fkaccount_id;
    private string _favourite;
    private List<Playlist_Track> _playlistTracks = new List<Playlist_Track>(); // List to hold Playlist_Track objects
    private List<Song> PlaylistSongs;

    public BasePlaylist()
    {
        _title = "";
        pkplaylist_id = 0;
        _queue = "";
        _favourite = "";
        _favourite = "";
        PlaylistSongs = new List<Song>();
}

    public void SetPlaylistId(int userinput)
    {
        pkplaylist_id = userinput;
    }

    public int GetPlaylistId()
    { 
        return pkplaylist_id;
    }

    public void SetTitle(string userInput)
    {
        _title = userInput;
    }

    public string GetTitle()
    {
        return _title;
    }

    public void SetQueue(string inputQueue)
    {
        _queue = inputQueue;
    }

    public string GetQueue()
    {
        return _queue;
    }

    public void SetFavourite(string fave)
    {
        _favourite = fave;
    }

    public string GetFavourite()
    {
        return _favourite;
    }


    public void SetAccountId(int accountid)
    {
        fkaccount_id = accountid;
    }

    public int GetAccountId()
    { 
        return fkaccount_id;
    }

    //--------------------------------Start of functions to store the list of playlist songs

    public List<Song> GetPlaylistSongs()
    {
        // You could add logic here to modify what is returned
        return PlaylistSongs;
    }

    public void SetPlaylistSongs(List<Song> value)
    {
        // You could add logic here to modify what is set
        PlaylistSongs = value;
    }

    public void AddPlaylistSong(Song song)
    {
        if (song == null)
            throw new ArgumentNullException(nameof(song), "Cannot add a null song to the playlist.");
        PlaylistSongs.Add(song);
    }


    //--------------------------------End of functions to store the list of playlist songs



    public void AddToPlaylist(Playlist_Track track)
    {
        _playlistTracks.Add(track);
    }

    public void RemoveFromPlaylist(Playlist_Track track)
    {
        _playlistTracks.Remove(track);
    }

    public List<Playlist_Track> GetTracks()
    {
        return _playlistTracks;
    }

    public List<string> GetTrackLocations()
    {
        return _playlistTracks.Select(track => track.GetTrackUri()).ToList();
    }

    public void PrintPlaylistDetails()
    {
        Console.WriteLine($"Playlist Title: {GetTitle()}");
        foreach (var track in GetTracks())
        {
            track.PrintPlaylistTrack();
        }
    }
}
