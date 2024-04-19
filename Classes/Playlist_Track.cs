﻿using Betawave.Classes;
using System.Drawing;

public class Playlist_Track
{
    private int fkplaylistId;
    private int trackNumber;
    private int fksongId;
    private Song Song;

    public Playlist_Track()
    {
        fkplaylistId = 0;
        trackNumber = 0;
        fksongId = 0;
        Song = null;
    }


    public void SetPlaylistId(int id)
    {
        fkplaylistId = id;
    }

    public int GetPlaylistId()
    {
        return fkplaylistId;
    }

    public void SetTrackNumber(string userInput)
    {
        if (int.TryParse(userInput, out int parsedTrackNumber))
        {
            trackNumber = parsedTrackNumber;
        }
        else
        {
            Console.WriteLine("Invalid input for track number.");
        }
    }

    public int GetTrackNumber()
    {
        return trackNumber;
    }

    public int GetSongId()
    {
        return fksongId;
       
    }

    public void SetSongId( int songid)
    {
        fksongId = songid;
    }

    public Song GetSong()
    {
        return Song;
    }

    public void SetSong(Song value)
    {
        Song = value;
    }

    public void PrintPlaylistTrackDetails()
    {
        Console.WriteLine($"Playlist ID: {GetPlaylistId()}, Track Number: {GetTrackNumber()}, Song ID: {GetSongId()}");
        if (Song != null)
        {
            Console.WriteLine($"Song Details: Name: {Song.GetName()}, Duration: {Song.GetDuration()}");
        }
    }
}

