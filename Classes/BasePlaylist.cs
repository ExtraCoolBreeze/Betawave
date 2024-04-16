using Betawave.Classes;
using Windows.Web.AtomPub;
public class BasePlaylist
{
    private int pkplaylist_id;
    private string _title;
    private string _queue;
    private int  fkaccount_id;
    private string _favourite;
    protected List<Playlist_Track> _playlistTracks = new List<Playlist_Track>(); // List to hold Playlist_Track objects

    public BasePlaylist()
    {
        _title = "";
        pkplaylist_id = 0;
        _queue = "";
        _favourite = "";
        _favourite = 0;
}

    public void SetPlayListId(int userinput)
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

    public void SetFavourite()
    { 
    
    }

    public string GetFavourite()
    {
        return _favourite;
    }

    public virtual void AddToPlaylist(Playlist_Track track)
    {
        _playlistTracks.Add(track);
    }

    public virtual void RemoveFromPlaylist(Playlist_Track track)
    {
        _playlistTracks.Remove(track);
    }

    public List<Playlist_Track> GetTracks()
    {
        return new List<Playlist_Track>(_playlistTracks);
    }

    public virtual List<string> GetTrackLocations()
    {
        List<string> trackLocations = new List<string>();
        foreach (var track in _playlistTracks)
        {
            trackLocations.Add(track.GetTrackUri());
        }
        return trackLocations;
    }

    public virtual void PrintPlaylistDetails()
    {
        Console.WriteLine($"Playlist Title: {GetTitle()}");
        foreach (var track in GetTracks())
        {
            track.PrintPlaylistTrack(); // Assuming Playlist_Track has a method to print its details
        }
    }
}
