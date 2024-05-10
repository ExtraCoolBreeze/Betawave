/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: The song class was created to hold information about an individual song */

using Betawave.Classes;

//declairing class and variables
public class Song
{
    private int pksongId;
    private string songName;
    private string songLocation;

    //song constuctor
    public Song()
    {
        pksongId = 0;
        songName = "";
        songLocation = "";
    }

    /// <summary>
    /// Returns the song id
    /// </summary>
    /// <returns></returns>
    public int GetSongId()
    {
        return pksongId;
    }

    /// <summary>
    /// When passed an int it sets that to the songid variable
    /// </summary>
    /// <param name="value"></param>
    public void SetSongId(int value)
    {
        pksongId = value;
    }

    /// <summary>
    /// Returns the song name
    /// </summary>
    /// <returns></returns>
    public string GetSongName()
    {
        return songName;
    }

    /// <summary>
    /// When passed a string, it sets that to the song name variable
    /// </summary>
    /// <param name="value"></param>
    public void SetSongName(string value)
    {
        songName = value;
    }
    /// <summary>
    /// returns the song location
    /// </summary>
    /// <returns></returns>
    public string GetSongLocation()
    {
        return songLocation;
    }

    /// <summary>
    /// when passed a string, it sets that to the song location
    /// </summary>
    /// <param name="value"></param>
    public void SetSongLocation(string value)
    {
        songLocation = value;
    }
}
