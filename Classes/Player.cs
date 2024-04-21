using NAudio.Wave;
using System;
using Betawave.Classes; // Ensure this is correctly referencing your classes

public class Player
{
    private IWavePlayer waveOutDevice;
    private AudioFileReader audioFileReader;
    private BasePlaylist currentPlaylist;
    private Random random = new Random();
    private bool shuffle = false;
    private int currentTrackIndex = -1;
    public bool IsPlaying;

    public event EventHandler<StoppedEventArgs> PlaybackStopped;
    public event EventHandler TrackChanged;

    public Player()
    {
        waveOutDevice = new WaveOutEvent();
        waveOutDevice.PlaybackStopped += OnPlaybackStoppedInternal;
    }

    private void OnPlaybackStoppedInternal(object sender, StoppedEventArgs e)
    {
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
        if (audioFileReader != null)
        {
            audioFileReader.Dispose(); // Dispose previous reader
        }

        try
        {
            audioFileReader = new AudioFileReader(filePath);
            waveOutDevice.Init(audioFileReader);
            if (TrackChanged != null)
            {
                TrackChanged(this, EventArgs.Empty);
            }
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
            waveOutDevice.Play();
        }
        else
        {
            Console.WriteLine("Audio file is not loaded.");
        }
    }

    public void PauseMusic()
    {
        waveOutDevice.Pause();
    }

    public void StopMusic()
    {
        waveOutDevice.Stop();
    }

    public void SetVolume(float volume)
    {
        if (audioFileReader != null)
        {
            audioFileReader.Volume = volume; // Volume between 0.0 and 1.0
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

    public void ToggleShuffle()
    {
        shuffle = !shuffle;
        Console.WriteLine($"Shuffle mode: {(shuffle ? "Enabled" : "Disabled")}");
    }

    public void PlayNextTrack()
    {
        if (currentPlaylist != null && currentPlaylist.GetPlaylistSongs().Count > 0)
        {
            currentTrackIndex = (currentTrackIndex + 1) % currentPlaylist.GetPlaylistSongs().Count;
            var track = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            LoadMusic(track.GetSongLocation());
            PlayMusic();
        }
    }

    public void PlayPreviousTrack()
    {
        if (currentPlaylist != null && currentPlaylist.GetPlaylistSongs().Count > 0)
        {
            currentTrackIndex = (currentTrackIndex - 1 + currentPlaylist.GetPlaylistSongs().Count) % currentPlaylist.GetPlaylistSongs().Count;
            var track = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            LoadMusic(track.GetSongLocation());
            PlayMusic();
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
            int index = random.Next(currentPlaylist.GetPlaylistSongs().Count);
            var track = currentPlaylist.GetPlaylistSongs()[index];
            LoadMusic(track.GetSongLocation());
            PlayMusic();
        }
    }

    public string GetCurrentTrackName()
    {
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            return currentPlaylist.GetPlaylistSongs()[currentTrackIndex].GetName();
        }
        else
        {
            return "Unknown Track";
        }
    }

    public string GetCurrentTrackArtist()
    {
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            return currentPlaylist.GetPlaylistSongs()[currentTrackIndex].GetArtist().GetName();
        }
        else
        {
            return "Unknown Artist";
        }
    }

    public string GetCurrentTrackImage()
    {
        if (currentPlaylist != null && currentTrackIndex >= 0 && currentTrackIndex < currentPlaylist.GetPlaylistSongs().Count)
        {
            Song currentSong = currentPlaylist.GetPlaylistSongs()[currentTrackIndex];
            return currentSong.GetSongLocation();  // Ensure songs have an image location set
        }
        else
        {
            return "default_album_cover.png";  // Return a default image if no track is loaded or if no image URL is set
        }
    }


}
