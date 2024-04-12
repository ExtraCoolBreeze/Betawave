using CommunityToolkit.Maui.Views;
using Betawave.Classes;
using CommunityToolkit.Maui.Core.Primitives;
using System.Reflection;
using Microsoft.Maui.Controls;
using System;
using System.IO;

public class Player
{
    private MediaElement _mediaElement;
    private BasePlaylist _currentPlaylist;
    private Random _random = new Random();
    private bool _shuffle = false;
    private int _currentTrackIndex = -1;
    private string _resourceName;

    public Player(MediaElement mediaElement)
    {
        SetMediaElement(mediaElement);
    }

    public MediaElementState GetCurrentState()
    {
        return _mediaElement.CurrentState;
    }

    public void SetMediaElement(MediaElement element)
    {
        _mediaElement = element;
        _mediaElement.MediaEnded += OnMediaEnded;
    }

    public bool IsPlaying()
    {
        return _mediaElement.CurrentState == MediaElementState.Playing;
    }

    public void LoadAndPlayMusic(string relativePath)
    {
        LoadMusic(relativePath);
        PlayMusic();
    }

    private void OnMediaEnded(object sender, EventArgs e)
    {
        if (_shuffle)
            PlayRandomTrack();
        else
            PlayNextTrack();
    }

    public void LoadMusic(string relativePath)
    {
        try
        {
            Uri fileUri = new Uri($"file:///{relativePath}", UriKind.Absolute);
            _mediaElement.Source = fileUri;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading music: {ex.Message}");
        }
    }

    public void PlayMusic()
    {
        if (_mediaElement.Source != null)
        {
            _mediaElement.Play();
        }
        else 
        { 
            Console.WriteLine("_mediaElement.Source is null");
        }
    }

    public void PauseMusic()
    {
        _mediaElement.Pause();
    }

    public void StopMusic()
    {
        _mediaElement.Stop();
    }

    public void SetVolume(double volume)
    {
        _mediaElement.Volume = volume; // Volume is a value between 0.0 and 1.0
    }

    public double GetVolume()
    {
        return _mediaElement.Volume;
    }

    public void SkipMusic()
    {
        if (_shuffle)
        {
            PlayRandomTrack();
        }
        else
        {
            PlayNextTrack();
        }
    }

    public void PlayNextTrack()
    {
        if (_currentPlaylist != null && _currentPlaylist.GetTracks().Count > 0)
        {
            _currentTrackIndex = (_currentTrackIndex + 1) % _currentPlaylist.GetTracks().Count;
            var trackUri = _currentPlaylist.GetTracks()[_currentTrackIndex].GetTrackUri(); // Assuming GetTrackUri() exists
            LoadMusic(trackUri);
            PlayMusic();
        }
    }
    public void SetShuffle(bool shuffle)
    {
        _shuffle = shuffle;
        Console.WriteLine($"Shuffle mode set to: {(_shuffle ? "Enabled" : "Disabled")}");
    }


    public bool GetShuffle()
    {
        return _shuffle;
    }


    public void ToggleShuffle()
    {
        _shuffle = !_shuffle;
        Console.WriteLine($"Shuffle mode: {(_shuffle ? "Enabled" : "Disabled")}");
    }


    public void PlayRandomTrack()
    {
        if (_currentPlaylist != null && _currentPlaylist.GetTracks().Count > 0)
        {
            int trackIndex = _random.Next(_currentPlaylist.GetTracks().Count);
            var trackUri = _currentPlaylist.GetTracks()[trackIndex].GetTrackUri(); // Assuming GetTrackUri() exists
            LoadMusic(trackUri);
            PlayMusic();
        }
    }

    public void AddToPlaylist(Playlist_Track track)
    {
        _currentPlaylist.AddToPlaylist(track);
    }

    public void RemoveFromPlaylist(Playlist_Track track)
    {
        _currentPlaylist.RemoveFromPlaylist(track);
    }
}
