/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This player class was created to store the information and functions based around playing media */

using Betawave.Classes;
using NAudio.Wave;

//declaring class and variables
public class Player
{
    
    private IWavePlayer BetawavePlayer;
    private AudioFileReader audioFileReader;
    private BasePlaylist currentPlaylist;
    private DatabaseAccess dbAccess;
    private Random random = new Random();
    private bool shuffle = false;
    private bool repeat = false;
    private int currentTrackIndex = -1;
    private ErrorLogger errorLogger;


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


    public void LoadAndPlayMusic(string filePath)
    {
        LoadMusic(filePath);
        PlayMusic();
    }

    public void LoadMusic(string filePath)
    {
        StopMusic();
        if (audioFileReader != null)
        {
            audioFileReader.Dispose();
            audioFileReader = null;
        }

        try
        {
            audioFileReader = new AudioFileReader(filePath);
            BetawavePlayer.Init(audioFileReader);
            TrackChanged.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            errorLogger.LogError(ex);
        }
    }

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

    public bool IsPlaying()
    {
        return BetawavePlayer.PlaybackState == PlaybackState.Playing;
    }

    public void PauseMusic()
    {
        BetawavePlayer.Pause();
    }

    public void StopMusic()
    {
        BetawavePlayer.Stop();
        audioFileReader?.Dispose();
        audioFileReader = null;
    }

    public void SetVolume(float volume)
    {
        if (audioFileReader != null)
        {
            audioFileReader.Volume = volume;
        }
    }

    public float GetVolume()
    {
        return audioFileReader != null ? audioFileReader.Volume : 0f;
    }

    public void SkipNextTrack()
    {
        if (shuffle)
        {
            PlayRandomTrack();
        }
        else
        {
            PlayNextTrack();
        }
    }

    public void SetPlaylist(BasePlaylist playlist)
    {
        currentPlaylist = playlist;
        currentTrackIndex = 0;

        PlayCurrentTrack();
    }

    public BasePlaylist GetCurrentPlaylist()
    {
        return currentPlaylist;
    }


    public void PlayCurrentTrack()
    {
        if (currentPlaylist != null && currentPlaylist.GetPlaylistSongs().Count > 0)
        {
            Song track = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            LoadMusic(track.GetSongLocation());
            PlayMusic();
        }
        else
        {
            errorLogger.Log("Playlist is empty or not set.");
        }
    }

    public void ToggleShuffle()
    {
        shuffle = !shuffle;
    }

    public void ToggleRepeat()
    {
        repeat = !repeat;
    }

    public void PlayNextTrack()
    {
        if (currentPlaylist == null || currentPlaylist.GetPlaylistSongs().Count == 0)
        {
            errorLogger.Log("Playlist is empty or not set.");
            return;
        }

        if (shuffle)
        {
            PlayRandomTrack();
        }
        else
        {
            currentTrackIndex = (currentTrackIndex + 1) % currentPlaylist.GetPlaylistSongs().Count;
            if (currentTrackIndex == 0 && !repeat)
            {
                StopMusic();
            }
            else
            {
                PlayTrackAtCurrentIndex();
            }
        }
    }

    public void PlayPreviousTrack()
    {
        if (currentPlaylist != null && currentPlaylist.GetPlaylistSongs().Count > 0)
        {
            currentTrackIndex = (currentTrackIndex - 1 + currentPlaylist.GetPlaylistSongs().Count) % currentPlaylist.GetPlaylistSongs().Count;
            PlayTrackAtCurrentIndex();
        }
    }

    public void SetShuffle(bool enable)
    {
        shuffle = enable;
    }

    public bool GetShuffle()
    {
        return shuffle;
    }

    public void PlayRandomTrack()
    {
        if (currentPlaylist != null && currentPlaylist.GetPlaylistSongs().Count > 0)
        {
            int newIndex;
            do
            {
                newIndex = random.Next(currentPlaylist.GetPlaylistSongs().Count);
            } while (currentPlaylist.GetPlaylistSongs().Count > 1 && newIndex == currentTrackIndex); // Avoid repeating the same track if possible

            currentTrackIndex = newIndex;
            PlayTrackAtCurrentIndex();
        }
    }

    public string GetCurrentTrackName()
    {
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            return currentPlaylist.GetPlaylistSongs()[currentTrackIndex].GetSongName();
        }
        else
        {
            return "Error: Unknown Track";
        }
    }

    public string GetCurrentTrackArtist()
    {
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {

            string artistName = currentPlaylist.GetArtistName();
            if (artistName != null)
            {
                return artistName;
            }
            else
            {
                string currentArtistError = "Error: Unknown Artist";
                return currentArtistError;
            }
        }
        else
        {
            string currentPlaylistError = "Invalid playlist data or index";
            return currentPlaylistError;
        }
    }

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


    public TimeSpan GetCurrentTrackLength()
    {
        if (audioFileReader != null)
        {
            return audioFileReader.TotalTime;
        }
        else
        {
            return TimeSpan.Zero; // Return zero if no track is loaded
        }
    }


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

    public void PlayTrackAtCurrentIndex()
    {
        if (currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            Song track = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            LoadMusic(track.GetSongLocation());
            PlayMusic();
            TrackChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void OnPlaybackStopped(object sender, StoppedEventArgs e)
    {
        if (e.Exception != null)
        {
            errorLogger.LogError(e.Exception);
        }

        if (PlaybackStopped != null)
        {
            PlaybackStopped(this, e);
        }

        if (audioFileReader != null && BetawavePlayer.PlaybackState != PlaybackState.Playing)
        {
            SkipNextTrack();
        }
    }
}
