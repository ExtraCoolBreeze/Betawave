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
    public event EventHandler TrackChanged; // Event to notify when the track changes
    public event EventHandler PlaybackPositionChanged;


    public Player()
    {
        _waveOutDevice = new WaveOutEvent();
        _waveOutDevice.PlaybackStopped += OnPlaybackStoppedInternal;
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
            TrackChanged?.Invoke(this, EventArgs.Empty); // Notify that track has changed
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
            // Start a timer or similar to update the playback position
            StartPositionUpdateTimer();
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

    public void PlayPreviousTrack()
    {
        if (_currentPlaylist != null && _currentPlaylist.GetTracks().Count > 0)
        {
            _currentTrackIndex = (_currentTrackIndex - 1 + _currentPlaylist.GetTracks().Count) % _currentPlaylist.GetTracks().Count;
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
    private void OnPlaybackStoppedInternal(object sender, StoppedEventArgs e)
    {
        OnPlaybackStopped?.Invoke(this, e);
    }

    public double CurrentTrackPosition
    {
        get => _audioFileReader?.CurrentTime.TotalSeconds ?? 0;
        set
        {
            if (_audioFileReader != null && _audioFileReader.TotalTime.TotalSeconds > value)
            {
                _audioFileReader.CurrentTime = TimeSpan.FromSeconds(value);
                PlaybackPositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string GetCurrentTrackName()
    {
        if (_currentPlaylist != null && _currentTrackIndex >= 0 && _currentTrackIndex < _currentPlaylist.GetTracks().Count)
        {
            return _currentPlaylist.GetTracks()[_currentTrackIndex].GetTitle();
        }
        return "Unknown Track";
    }

    public string GetCurrentTrackArtist()
    {
        if (_currentPlaylist != null && _currentTrackIndex >= 0 && _currentTrackIndex < _currentPlaylist.GetTracks().Count)
        {
            return _currentPlaylist.GetTracks()[_currentTrackIndex].GetArtist();
        }
        return "Unknown Artist";
    }

    //Need to complete this function.

    public string GetCurrentTrackImageUrl()
    {
        if (_currentPlaylist != null && _currentTrackIndex >= 0 && _currentTrackIndex < _currentPlaylist.GetTracks().Count)
        {
            return ""; //_currentPlaylist.GetTracks()[_currentTrackIndex].GetImageUri();
        }
        return "default_album_cover.png";  // Return a default image if no track is loaded or if no image URL is set
    }


    public double TrackLength
    {
        get => _audioFileReader?.TotalTime.TotalSeconds ?? 0;
    }

    private void StartPositionUpdateTimer()
    {
        // Implement a timer or use event-driven updates that frequently checks the playback position
        // and raises the PlaybackPositionChanged event if there is a significant change.
    }

}
