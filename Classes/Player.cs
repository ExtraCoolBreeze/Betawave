using NAudio.Wave;
using System;
using System.IO;

public class Player
{
    private IWavePlayer _waveOutDevice;
    private AudioFileReader _audioFileReader;
    private BasePlaylist _currentPlaylist;
    private Random _random = new Random();
    private bool _shuffle = false;
    private int _currentTrackIndex = -1;
    public bool IsPlaying;
    public event EventHandler<StoppedEventArgs> OnPlaybackStopped;


    public Player()
    {
        _waveOutDevice = new WaveOutEvent();
    }

    public void SetIsPlaying()
    {
        IsPlaying = _waveOutDevice.PlaybackState == PlaybackState.Playing;
    }

    public bool GetIsPlaying()
    {
        return IsPlaying;
    }


    public void LoadAndPlayMusic(string filePath)
    {
        LoadMusic(filePath);
        PlayMusic();
    }


    public void LoadMusic(string filePath)
    {
        StopMusic(); // Stop current track before loading another

        if (_audioFileReader != null)
        {
            _audioFileReader.Dispose(); // Dispose previous reader if exists
        }

        try
        {
            _audioFileReader = new AudioFileReader(filePath);
            _waveOutDevice.Init(_audioFileReader);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading music: {ex.Message}");
        }
    }

    public void PlayMusic()
    {
        if (_audioFileReader != null)
        {
            _waveOutDevice.Play();
        }
        else
        {
            Console.WriteLine("Audio file is not loaded.");
        }
    }

    public void PauseMusic()
    {
        _waveOutDevice.Pause();
    }

    public void StopMusic()
    {
        _waveOutDevice.Stop();
    }

    public void SetVolume(float volume)
    {
        if (_audioFileReader != null)
        {
            _audioFileReader.Volume = volume; // Volume is a value between 0.0 (silent) and 1.0 (full volume)
        }

    }

    public float GetVolume()
    {
        return _audioFileReader?.Volume ?? 0f;
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
            var trackUri = _currentPlaylist.GetTracks()[_currentTrackIndex].GetTrackUri();
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
