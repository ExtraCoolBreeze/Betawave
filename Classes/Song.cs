using Betawave.Classes;

public class Song
{
    private int pksongId;
    private string _name;
    private List<Featured_Artists> _featuredArtists = new List<Featured_Artists>();
    private string _duration;
    private string _songLocation;

    public Song()
    {
        pksongId = 0;
        _name = "";
        _duration = "";
        _songLocation = "";
    }

    public int GetSongId()
    {
        return pksongId;
    }

    public void SetSongId(int songId)
    {
        pksongId = songId;
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string value)
    {
        _name = value;
    }

    public List<Featured_Artists> GetFeaturedArtists()
    {
        return _featuredArtists;
    }

    public string GetDuration()
    {
        return _duration;
    }

    public void SetDuration(string value)
    {
        _duration = value;
    }

    public string GetSongLocation()
    {
        return _songLocation;
    }

    public void SetSongLocation(string value)
    {
        _songLocation = value;
    }

    public void AddFeaturedArtist(Artist artist)
    {
        var featuredArtist = new Featured_Artists();
        featuredArtist.SetSong(this);
        featuredArtist.SetArtist(artist);

        _featuredArtists.Add(featuredArtist);
        artist.FeaturedSongs.Add(featuredArtist);
    }

    // Method to print the details of the song
    public void PrintSong()
    {
        Console.WriteLine($"Name: {_name}, Duration: {_duration}, Location: {_songLocation}");
    }
}
