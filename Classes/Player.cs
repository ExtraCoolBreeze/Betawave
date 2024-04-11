using CommunityToolkit.Maui.Views;
using Betawave.Classes;
public class Player
{
    private MediaElement _mediaElement;
    private BasePlaylist _currentPlaylist;
    private Random _random = new Random();
    private bool _shuffle = false;
    private int _currentTrackIndex = -1;

    public Player(MediaElement mediaElement)
    {
        SetMediaElement(mediaElement);
    }

    public void SetMediaElement(MediaElement element)
    {
        if (_mediaElement != null)
        {
            _mediaElement.MediaEnded -= OnMediaEnded;
        }

        _mediaElement = element;
        if (_mediaElement != null)
        {
            _mediaElement.MediaEnded += OnMediaEnded;
        }
    }


    public MediaElement GetMediaElement()
    {
        return _mediaElement;
    }

    public void SetCurrentPlaylist(BasePlaylist playlist)
    {
        _currentPlaylist = playlist;
    }

    public BasePlaylist GetCurrentPlaylist()
    {
        return _currentPlaylist;
    }

    public void SetShuffle(bool value)
    {
        _shuffle = value;
    }

    public bool GetShuffle()
    {
        return _shuffle;
    }

    private void OnMediaEnded(object sender, EventArgs e)
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

    public void LoadMusic(string songLocation)
    {
        _mediaElement.Source = new Uri(songLocation);
    }

    public void PlayMusic()
    {
        if (_mediaElement.Source != null)
        {
            _mediaElement.Play();
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
        var tracks = _currentPlaylist.GetTracks();
        if (tracks.Count > 0)
        {
            _currentTrackIndex = (_currentTrackIndex + 1) % tracks.Count;
            var trackUri = tracks[_currentTrackIndex].GetTrackUri(); // Assuming GetTrackUri() exists in Playlist_Track
            LoadMusic(trackUri);
            PlayMusic();
        }
    }

    public void PlayRandomTrack()
    {
        var tracks = _currentPlaylist.GetTracks();
        if (tracks.Count > 0)
        {
            int trackIndex = _random.Next(tracks.Count);
            var trackUri = tracks[trackIndex].GetTrackUri(); // Assuming GetTrackUri() exists in Playlist_Track
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
