using System;
using System.Collections.Generic;
using Betawave.Classes; // Assuming Playlist_Track is in this namespace

public abstract class BasePlaylist
{
    // Fields
    private int playlistId = 0;
    private string title = "";
    protected List<Playlist_Track> playlistTracks = new List<Playlist_Track>(); // List to hold Playlist_Track objects

    // Constructor
    protected BasePlaylist()
    {
        playlistId = 0;
        title = "";
    }

    // Method to set the title
    public void SetTitle(string userInput)
    {
        title = userInput;
    }

    // Function to get the title
    public string GetTitle()
    {
        return title;
    }

    // Method to increment and set the playlist ID
    public void SetPlaylistId()
    {
        playlistId += 1;
    }

    // Function to get the playlist ID
    public int GetPlaylistId()
    {
        return playlistId;
    }

    //Abstract method to get the track locations for playing music
    public abstract void GetTrackLocations();

    // Abstract method to add a Playlist_Track to the playlist
    public abstract void AddToPlaylist(Playlist_Track track);

    // Abstract method to remove a Playlist_Track from the playlist
    public abstract void RemoveFromPlaylist(Playlist_Track track);

    // Abstract method to print playlist details
    public abstract void PrintPlaylistDetails();


}
