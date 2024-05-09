/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: The song class was created to hold information about an individual song */

using Betawave.Classes;

public class Song
{
    private int pksongId;
    private string songName;
    private int artistId;
    private string songLocation;
    private Artist artist;
    private Album album;

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

    public string GetSongName()
    {
        return songName;
    }

    public void SetSongName(string value)
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

    public void SetAlbum(Album value)
    {
        album = value;
    }

    public Album GetAlbum()
    {
        return album;
    }


    // Method to print the details of the song
    public void PrintSong()
    {
        Console.WriteLine($"Name: {songName}, Location: {songLocation}");
    }
}
