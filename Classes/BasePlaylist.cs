using Betawave.Classes;
public class BasePlaylist
{
    private int pkplaylist_id;
    private string _title;
    private string _queue;
    private int  fkaccount_id;
    private string _favourite;
    private List<Playlist_Track> _playlistTracks = new List<Playlist_Track>(); // List to hold Playlist_Track objects

    public BasePlaylist()
    {
        _title = "";
        pkplaylist_id = 0;
        _queue = "";
        _favourite = "";
        _favourite = "";
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
