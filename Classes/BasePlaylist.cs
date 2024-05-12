/*
Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This playlist class was created to store the information about playlist objects*/

using Betawave.Classes;

//inheriting from playlist interface
public class BasePlaylist : IPlaylist
{
    //declaring object and variables
    private List<Song> PlaylistSongs;
    private string albumName;
    private string artistName;
    private string imageLocation;

    //class constructor 
    public BasePlaylist()
    {
        //initialising variables
        PlaylistSongs = new List<Song>();
        albumName = "";
        artistName = "";
        imageLocation = "";
    }

    /// <summary>
    /// When called and passed a string it sets that string to the album name variable
    /// </summary>
    /// <param name="nameofalbum"></param>
    public void SetAlbumName(string nameofalbum)
    {
        albumName = nameofalbum;
    }

    /// <summary>
    /// When called this function returns the album name variable
    /// </summary>
    /// <returns></returns>
    public string GetAlbumName()
    {
        return albumName;
    }

    /// <summary>
    /// When called and passed a string, it sets that string to the artist name variable
    /// </summary>
    /// <param name="nameofartist"></param>
    public void SetArtistName(string nameofartist)
    {
        artistName = nameofartist;
    }

    /// <summary>
    /// When called this function returned the artist name variable
    /// </summary>
    /// <returns></returns>
    public string GetArtistName()
    { 
        return artistName;
    }

    /// <summary>
    /// When this method is called and passed a string, it sets the string to the imageLocation variable
    /// </summary>
    /// <param name="image"></param>
    public void SetImageLocation(string image)
    { 
        imageLocation = image;
    }

    /// <summary>
    /// When this function is called it returns the imageLocation variable
    /// </summary>
    /// <returns></returns>
    public string GetImageLocation()
    { 
        return imageLocation;
    }

    /// <summary>
    /// When called this function returns the list of playlist songs
    /// </summary>
    /// <returns></returns>
    public List<Song> GetPlaylistSongs()
    {
        return PlaylistSongs;
    }

    /// <summary>
    /// When called and pass a song object, it adds that song the list of song objects
    /// </summary>
    /// <param name="song"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddToPlaylist(Song song)
    {
        PlaylistSongs.Add(song);
    }

    /// <summary>
    /// When called and passed a song object, it removes that song from the list of song objects
    /// </summary>
    /// <param name="song"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void RemoveFromPlaylist(Song song)
    {
        PlaylistSongs.Remove(song);
    }
}
