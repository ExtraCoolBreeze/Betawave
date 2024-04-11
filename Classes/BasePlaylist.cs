using Betawave.Classes;
public abstract class BasePlaylist
{
    private int _playlist_id;
    private string _title = "";
    protected List<Playlist_Track> _playlistTracks = new List<Playlist_Track>(); // List to hold Playlist_Track objects

    protected BasePlaylist()
    {
        _title = "";
    }

    public void SetPlayListId(int userinput)
    {
        _playlist_id = _playlist_id;
    }

    public int GetPlaylistId()
    { 
        return _playlist_id;
    }

    public void SetTitle(string userInput)
    {
        _title = userInput;
    }

    public string GetTitle()
    {
        return _title;
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
