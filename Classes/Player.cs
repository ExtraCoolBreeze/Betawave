﻿/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This player class was created to store the information and functions based around playing media */

using NAudio.Wave;
using Betawave.Classes;

public class Player
{
    private IWavePlayer BetawavePlayer;
    private AudioFileReader audioFileReader;
    private BasePlaylist currentPlaylist;
    private Random random = new Random();
    private bool shuffle = false;
    private bool repeat = false;
    private int currentTrackIndex = -1;


    public event EventHandler<StoppedEventArgs> PlaybackStopped;
    public event EventHandler TrackChanged;

    public Player()
    {
        BetawavePlayer = new WaveOutEvent();
        BetawavePlayer.PlaybackStopped += OnPlaybackStopped;
    }

    public void OnPlaybackStopped(object sender, StoppedEventArgs e)
    {
        if (e.Exception != null)
        {
            Console.WriteLine($"Playback stopped due to an error: {e.Exception.Message}");
        }
        else
        {
            Console.WriteLine("Playback stopped without any error.");
        }

        if (PlaybackStopped != null)
        {
            PlaybackStopped(this, e);
        }
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
            TrackChanged?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading music: {ex.Message}");
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
            Console.WriteLine("Audio file is not loaded.");
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
            var track = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            LoadMusic(track.GetSongLocation());
            PlayMusic();
        }
        else
        {
            Console.WriteLine("Playlist is empty or not set.");
        }
    }


    public void ToggleShuffle()
    {
        shuffle = !shuffle;

        if (shuffle == true)
        {

        }
        Console.WriteLine($"Shuffle mode: {(shuffle ? "Enabled" : "Disabled")}");
    }

    public void ToggleRepeat()
    {
        repeat = !repeat;

        if (repeat == true)
        {
            //
        }
        Console.WriteLine($"Shuffle mode: {(shuffle ? "Enabled" : "Disabled")}");

    }

    public void PlayNextTrack()
    {
        if (currentPlaylist == null || currentPlaylist.GetPlaylistSongs().Count == 0)
        {
            Console.WriteLine("Playlist is empty or not set.");
            return;
        }

        if (shuffle)
        {
            PlayRandomTrack();
        }
        else
        {
            // Increment the current track index
            currentTrackIndex = (currentTrackIndex + 1) % currentPlaylist.GetPlaylistSongs().Count;

            // If the index wraps around to 0 and you do not want to repeat the playlist automatically
            if (currentTrackIndex == 0 && !repeat) // Assuming 'repeat' is a boolean indicating if the playlist should loop
            {
                Console.WriteLine("Reached end of playlist.");
                StopMusic(); // Stop playing if repeat is not enabled
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
        Console.WriteLine($"Shuffle mode: {(shuffle ? "Enabled" : "Disabled")}");
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


    public string GetCurrentTrackImage()
    {
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            Song currentSong = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];

/*           // Album album = currentSong;//.GetAlbum();
            if (album != null)
            {
                return album.GetImageLocation();
            }*/
        }
        return "default_album_cover.png";
    }



    public void PlayTrackAtCurrentIndex()
    {
        if (currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            var track = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            LoadMusic(track.GetSongLocation());
            PlayMusic();
        }
    }

    public TimeSpan GetCurrentTrackPosition()
    {
        if (audioFileReader != null)
        {
            return audioFileReader.CurrentTime;
        }
        else
        {
            return TimeSpan.Zero; // Or appropriate default value
        }
    }


}
