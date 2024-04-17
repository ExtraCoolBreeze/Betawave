using Betawave.Classes;
using System.Drawing;

public class Playlist_Track
{
    private int fkplaylist_id;
    private int _trackNumber;
    private string _artist;
    private string _title;
    private TimeSpan _duration;
    private string _trackUri;
    private int fksong_id;

    public Playlist_Track(Song song)
    {
        _title = "";
        _trackNumber = 0;
        _artist = "";
        _trackUri = song.GetSongLocation();
        _duration = TimeSpan.Zero; // Initializes duration to 0
    }


    public void SetPlaylistId(int id)
    {
        fkplaylist_id = id;
    }

    public int GetPlaylistId()
    {
        return fkplaylist_id;
    }

    public void SetTrackNumber(string userInput)
    {
        if (int.TryParse(userInput, out int parsedTrackNumber))
        {
            _trackNumber = parsedTrackNumber;
        }
        else
        {
            Console.WriteLine("Invalid input for track number.");
        }
    }

    public int GetTrackNumber()
    {
        return _trackNumber;
    }

    public void SetTitle(string title)
    {
        _title = title;
    }

    public string GetTitle()
    { 
        return _title;
    }

    public void SetArtist(string artist)
    {
        _artist = artist;
    }

    public string GetArtist()
    { 
        return _artist;
    }

    public void SetDuration(TimeSpan duration)
    {
        _duration = duration;
    }

    public TimeSpan GetDuration()
    { 
        return _duration;
    }



    public void SetTrackUri(string trackuri)
    {
        _trackUri = trackuri;
    }

    public string GetTrackUri()
    { 
        return _trackUri;
    }

    public int GetSongId()
    {
        return fksong_id;
       
    }

    public void SetSongId( int songid)
    {
        fksong_id = songid;
    }

    public void PrintPlaylistTrack()
    {
        Console.WriteLine($"Title: {_title}, Track Number: {GetTrackNumber()}, Artist: {_artist}, Duration: {_duration}");
    }
}

