using Betawave.Classes;

public class Song
{
    private int pksongId;
    private string songName;
    private int artistId;
    private string songLocation;
    private Artist artist;

    public Song()
    {
        pksongId = 0;
        songName = "";
        artistId = 0;
        songLocation = "";
    }

    public int GetSongId()
    {
        return pksongId;
    }

    public void SetSongId(int value)
    {
        pksongId = value;
    }

    public string GetName()
    {
        return songName;
    }

    public void SetName(string value)
    {
        songName = value;
    }

    public int GetArtistId()
    {
        return artistId;
    }

    public void SetArtistId(int value)
    {
        artistId = value;
    }

    public string GetSongLocation()
    {
        return songLocation;
    }

    public void SetSongLocation(string value)
    {
        songLocation = value;
    }

    public void SetArtist(Artist data)
    {
        artist = data;
    }

    public Artist GetArtist()
    {
       return artist;
    }


    // Method to print the details of the song
    public void PrintSong()
    {
        Console.WriteLine($"Name: {songName}, Location: {songLocation}");
    }
}
