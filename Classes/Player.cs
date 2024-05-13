/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This player class was created to store the information and functions based around playing media */

using Betawave.Classes;
using MySqlX.XDevAPI.CRUD;
using NAudio.Wave;

//declaring class and variables
public class Player
{
    //declaring class objects and variables
    private IWavePlayer BetawavePlayer;
    private AudioFileReader audioFileReader;
    private BasePlaylist currentPlaylist;
    private DatabaseAccess dbAccess;
    private Random random = new Random();
    private bool shuffle = false;
    private bool repeat = false;
    private int currentTrackIndex = -1;
    private ErrorLogger errorLogger;

    //creating event handler for other classes to subscribe to
    public event EventHandler<StoppedEventArgs> PlaybackStopped;
    public event EventHandler TrackChanged;

    //class constructor and initialising objects
    public Player()
    {   
        BetawavePlayer = new WaveOutEvent();
        currentPlaylist = new BasePlaylist();
        dbAccess = new DatabaseAccess();
        errorLogger = new ErrorLogger("C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\BetawaveErrorLog.txt");

        //subscribing to changes in state
        BetawavePlayer.PlaybackStopped += OnPlaybackStopped; 
    }

    /// <summary>
    /// when this method is called and passed a file path, it calls the load music function and passes the file to load the player, it then calls the play music method
    /// </summary>
    /// <param name="filePath"></param>
    public void LoadAndPlayMusic(string filePath)
    {
        LoadMusic(filePath);
        PlayMusic();
    }

    /// <summary>
    /// When called and passed a file path, it loads the music player will the file
    /// </summary>
    /// <param name="filePath"></param>
    public void LoadMusic(string filePath)
    {   //calls stops music playing so that another file can be loaded
        StopMusic();
        //if there is a song loaded it unloads that file
        if (audioFileReader != null)
        {
            audioFileReader.Dispose();
            audioFileReader = null;
        }
        
        try
        {   //the file new file path is then loaded into the player and track change event is triggered
            audioFileReader = new AudioFileReader(filePath);
            BetawavePlayer.Init(audioFileReader);
            TrackChanged.Invoke(this, EventArgs.Empty);
        }//catching errors in this action
        catch (Exception ex)
        {
            errorLogger.LogError(ex);
        }
    }

    /// <summary>
    /// This method checks if music has been loaded and then plays the file, if not able to it logs an error.
    /// </summary>
    public void PlayMusic()
    {
        if (audioFileReader != null)
        {
            BetawavePlayer.Play();
        }
        else
        {
            errorLogger.Log("Audio file is not loaded.");
        }
    }

    /// <summary>
    /// When called it returns the playback state of the player
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return BetawavePlayer.PlaybackState == PlaybackState.Playing;
    }

    /// <summary>
    /// When called the player pauses playback
    /// </summary>
    public void PauseMusic()
    {
        BetawavePlayer.Pause();
    }

    /// <summary>
    /// When called the music player stops playing music and unloads the player
    /// </summary>
    public void StopMusic()
    {
        BetawavePlayer.Stop();
        audioFileReader?.Dispose();
        audioFileReader = null;
    }

    /// <summary>
    /// When called and passed the volume, of the player is playing music it changes the volume
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        if (audioFileReader != null)
        {
            audioFileReader.Volume = volume;
        }
    }

    /// <summary>
    /// This function returns the current volume of the player
    /// </summary>
    /// <returns></returns>
    public float GetVolume()
    {
        //if the player is loaded return the volume
        if (audioFileReader != null)
        {

            return audioFileReader.Volume;
        }
        else
        {
            // If player is null, return 0
            return 0f;
        }
    }

    /// <summary>
    /// When called this method checks shuffle is on, if so it plays a random track and if not plays the next track
    /// </summary>
    public void SkipNextTrack()
    {
        if (shuffle == true)
        {
            PlayRandomTrack();
        }
        else
        {
            PlayNextTrack();
        }
    }

    /// <summary>
    /// This method when called and passed a playlist, it saves that playlist and loads that playlist into the player
    /// </summary>
    /// <param name="playlist"></param>
    public void SetPlaylist(BasePlaylist playlist)
    {
        currentPlaylist = playlist;
        currentTrackIndex = 0;

        PlayCurrentTrack();
    }

    /// <summary>
    /// When called this function return the current saved playlist
    /// </summary>
    /// <returns></returns>
    public BasePlaylist GetCurrentPlaylist()
    {
        return currentPlaylist;
    }

    /// <summary>
    /// This method when called checks if the current playlist is null or has more than 0 tracks, loads it into the player and plays the music
    /// </summary>
    public void PlayCurrentTrack()
    {       //performing check
        if (currentPlaylist != null && currentPlaylist.GetPlaylistSongs().Count > 0)
        {   //loading into song object, passing to loadmusic method and then calling play music method
            Song track = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            LoadMusic(track.GetSongLocation());
            PlayMusic();
        }
        else
        {   //logs error
            errorLogger.Log("Playlist is empty or not set.");
        }
    }

    /// <summary>
    /// This method turns shuffle on and off
    /// </summary>
    public void ToggleShuffle()
    {
        shuffle = !shuffle;
    }

    /// <summary>
    /// This method turns repeat on and off
    /// </summary>
    public void ToggleRepeat()
    {
        repeat = !repeat;
    }

    /// <summary>
    /// When called this method plays the next track, or a random one depending if random track is true
    /// </summary>
    public void PlayNextTrack()
    {   //checks the playlist is null and if the playlist is empty
        if (currentPlaylist == null || currentPlaylist.GetPlaylistSongs().Count == 0)
        {   //logs error
            errorLogger.Log("Playlist is empty or not set.");
            return;
        }
        //is shuffle is true, play random track
        if (shuffle == true)
        {
            PlayRandomTrack();
        }
        else
        {   //get size of the playlist and then caluclate the new index
            int sizeofPlaylist = currentPlaylist.GetPlaylistSongs().Count;
            currentTrackIndex = (currentTrackIndex + 1) % sizeofPlaylist;
            //if the playlist is done playing, stop the music
            if (currentTrackIndex == 0 && !repeat)
            {
                StopMusic();
            }
            else
            {   //or play another song
                PlayTrackAtCurrentIndex();
            }
        }
    }

    /// <summary>
    /// This method checks if the playlist is null or empty before changing the currentTrackIndex and then playing the track at that index.
    /// </summary>
    public void PlayPreviousTrack()
    {   //performing check
        if (currentPlaylist != null && currentPlaylist.GetPlaylistSongs().Count > 0)
        {   //counting the songs in the playlist, setting a new index and playing track at new index
            int sizeofPlaylist = currentPlaylist.GetPlaylistSongs().Count;
            currentTrackIndex = (currentTrackIndex - 1 + sizeofPlaylist) % sizeofPlaylist;
            PlayTrackAtCurrentIndex();
        }
    }

    /// <summary>
    /// When called and passed a bool, it sets that to shuffle variable
    /// </summary>
    /// <param name="enable"></param>
    public void SetShuffle(bool enable)
    {
        shuffle = enable;
    }

    /// <summary>
    /// This function returns the shuffle
    /// </summary>
    /// <returns></returns>
    public bool GetShuffle()
    {
        return shuffle;
    }

    /// <summary>
    /// When called this function plays a random track
    /// </summary>
    public void PlayRandomTrack()
    {   //checks playlist is not null and empty
        if (currentPlaylist != null && currentPlaylist.GetPlaylistSongs().Count > 0)
        {   //creates random track variable
            int randomTrackIndex;
            do
            {   //generates a random track index and adds in check so that the same track is not played twice
              randomTrackIndex = random.Next(currentPlaylist.GetPlaylistSongs().Count);
            } while (currentPlaylist.GetPlaylistSongs().Count > 1 && randomTrackIndex == currentTrackIndex);
            //plays the current track at index
            currentTrackIndex = randomTrackIndex;
            PlayTrackAtCurrentIndex();
        }
    }

    /// <summary>
    /// This method checks the playlist and index and then returns the value
    /// </summary>
    /// <returns></returns>
    public string GetCurrentTrackName()
    {       //checks both playlist and index
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {   //adds songs to temp playlist then checks temp playlist
            List<Song> tempPlaylist = currentPlaylist.GetPlaylistSongs();
            if (tempPlaylist != null && currentTrackIndex < tempPlaylist.Count)
            {   //returns current name of song
                Song currentSong = tempPlaylist[currentTrackIndex];
                return currentSong.GetSongName();
            }
        }  //Returns error if not name found
        string currentAristError = "Error: Unknown Track";
        return currentAristError; 
    }


    /// <summary>
    /// When this method is called, it returns the current artist name
    /// </summary>
    /// <returns></returns>
    public string GetCurrentTrackArtist()
    {
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            //gets the artist name and returns if not null
            string artistName = currentPlaylist.GetArtistName();
            if (artistName != null)
            {
                return artistName;
            }
            else
            {   //if name is null returns error
                string currentArtistError = "Error: Unknown Artist";
                return currentArtistError;
            }
        }
        else
        {   //if playlist or index error returns error
            string currentPlaylistError = "Invalid playlist data or index";
            return currentPlaylistError;
        }
    }

    /// <summary>
    /// This method grabs the current album name. 
    /// it first checks the current playlist and index and then calls the function to get the album name. 
    /// it then returns that new value or returns a default if not found
    /// </summary>
    /// <returns></returns>
    
    public string GetCurrentAlbumName()
    {   
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            string albumTitle = currentPlaylist.GetAlbumName();
            if (albumTitle != null)
            {
                return albumTitle;
            }
        }
        string currentAlbumError = "Error: Unknown Album";
        return currentAlbumError;
    }

    /// <summary>
    /// This method checks if the player has something loaded and returns the length of the media track
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetCurrentTrackLength()
    {
        if (audioFileReader != null)
        {
            return audioFileReader.TotalTime;
        }
        else
        {   //Returns zero if no track is loaded
            return TimeSpan.Zero; 
        }
    }

    /// <summary>
    ///    When called this method gets the current track image. It checks the current playlist and index then grabs the current song.
    ///    it then uses the album manager class to grab the image by the song name and then saves the location of the image and returning that image. 
    ///     it not it returns a default image.
    /// </summary>
    /// <returns></returns>
    public async Task <string> GetCurrentTrackImage()
    {
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            Song currentSong = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            AlbumManager albumManager = new AlbumManager(dbAccess);
            string imageLocation = await albumManager.GetAlbumImageBySongName(currentSong.GetSongName());

            if (!string.IsNullOrEmpty(imageLocation))
            {
                return imageLocation;
            }
            else
            {
                return "default_album_cover.png";
            }
        }
        return "default_album_cover.png";
    }

    /// <summary>
    /// When this method is called it plays the track at the current index in the playlist
    /// </summary>
    public void PlayTrackAtCurrentIndex()
    {   //is the index is not 0 and less than the count of tracks
        if (currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {   //add next track to song object and load that object into the player and play, then triggering the track changed event
            Song track = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            LoadMusic(track.GetSongLocation());
            PlayMusic();
            TrackChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// When called this method performs actions based on the music stopping
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnPlaybackStopped(object sender, StoppedEventArgs e)
    {
        //is stopped in error, log error
        if (e.Exception != null)
        {
            errorLogger.LogError(e.Exception);
        }
        //is the playback stopped event has been subscribed too by another class
        if (PlaybackStopped != null)
        {
            PlaybackStopped(this, e);
        }
        //if there is a file loaded and if there is nothing playing skip to next track
        if (audioFileReader != null && BetawavePlayer.PlaybackState != PlaybackState.Playing)
        {
            SkipNextTrack();
        }
    }
}
