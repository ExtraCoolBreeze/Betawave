/*
Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This playlist class was created to store the information about playlist objects*/

public class BasePlaylist
{
    private int pkplaylistId;
    private string PlaylistTitle;
    private string isQueue;
    private int  fkaccountId;
    private string isFavourite;
    private List<Song> PlaylistSongs;

    public BasePlaylist()
    {
        PlaylistTitle = "";
        pkplaylistId = 0;
        isQueue = "";
        isFavourite = "";
        PlaylistSongs = new List<Song>();
}

    public void SetPlaylistId(int userinput)
    {
        pkplaylistId = userinput;
    }

    public int GetPlaylistId()
    { 
        return pkplaylistId;
    }

    public void SetTitle(string userInput)
    {
        PlaylistTitle = userInput;
    }

    public string GetTitle()
    {
        return PlaylistTitle;
    }

    public void SetQueue(string inputQueue)
    {
        isQueue = inputQueue;
    }

    public string GetQueue()
    {
        return isQueue;
    }

    public void SetFavourite(string fave)
    {
        isFavourite = fave;
    }

    public string GetFavourite()
    {
        return isFavourite;
    }


    public void SetAccountId(int accountid)
    {
        fkaccountId = accountid;
    }

    public int GetAccountId()
    { 
        return fkaccountId;
    }

    //--------------------------------Start of functions to store the list of playlist songs

    public List<Song> GetPlaylistSongs()
    {
        return PlaylistSongs;
    }

    public void AddSongToPlaylist(Song song)
    {
        if (song == null)
        {
            throw new ArgumentNullException(nameof(song), "Cannot add a null song to the playlist.");
        }

        PlaylistSongs.Add(song);
    }

    public void RemoveSong(Song song)
    {
        if (song == null)
        {
            throw new ArgumentNullException(nameof(song), "Cannot remove a null song from the playlist.");
        }

        PlaylistSongs.Remove(song);
    }

    public void PrintPlaylistDetails()
    {
        Console.WriteLine($"Playlist Title: {GetTitle()}");
        foreach (var song in GetPlaylistSongs())
        {
            Console.WriteLine($"Song: {song.GetSongName()}");
        }
    }
}
