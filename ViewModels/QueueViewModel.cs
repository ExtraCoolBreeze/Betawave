/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This view model was created to manage the interactions, updating and displaying of the UI on the Queue content page  */

using System.ComponentModel;
using System.Windows.Input;
using Betawave.ViewModels;
using System.Runtime.CompilerServices;
public class QueueViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private AudioViewModel audioViewModel;

    public ICommand PlayPauseCommand { get; private set; }
    public ICommand StopCommand { get; private set; }
    public ICommand SkipNextCommand { get; private set; }
    public ICommand SkipPreviousCommand { get; private set; }
    public ICommand ToggleShuffleCommand { get; private set; }
    public ICommand ToggleRepeatCommand { get; private set; }

    public string CurrentTrackName => audioViewModel.CurrentTrackName;
    public string CurrentTrackArtist => audioViewModel.CurrentTrackArtist;
    public string CurrentTrackImage => audioViewModel.CurrentTrackImage;
    public double CurrentTrackPosition => audioViewModel.CurrentTrackPosition;
    public double TrackLength => audioViewModel.TrackLength;

    public float Volume
    {
        get => audioViewModel.Volume;
        set
        {
            if (audioViewModel.Volume != value)
            {
                audioViewModel.Volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }
    }

    public bool Shuffle
    {
        get => audioViewModel.Shuffle;
        set
        {
            if (audioViewModel.Shuffle != value)
            {
                audioViewModel.Shuffle = value;
                OnPropertyChanged(nameof(Shuffle));
            }
        }
    }

    public string Song1 { get; private set; }
    public string Song2 { get; private set; }
    public string Song3 { get; private set; }
    public string Song4 { get; private set; }
    public string Song5 { get; private set; }
    public string Song6 { get; private set; }
    public string Song7 { get; private set; }
    public string Song8 { get; private set; }
    public string Song9 { get; private set; }
    public string Song10 { get; private set; }
    public string Song11 { get; private set; }

    public QueueViewModel(AudioViewModel audioViewModel)
    {

        this.audioViewModel = audioViewModel;

        PlayPauseCommand = new Command(() => audioViewModel.TogglePlayPause());
        StopCommand = new Command(() => audioViewModel.StopAudio());
        SkipNextCommand = new Command(() => audioViewModel.SkipNext());
        SkipPreviousCommand = new Command(() => audioViewModel.SkipPrevious());
        ToggleShuffleCommand = new Command(() => audioViewModel.ToggleShuffle());
        audioViewModel.PropertyChanged += AudioViewModel_PropertyChanged;
        UpdateSongInformation();
    }

    public void AudioViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // Check if the property changed is related to the playlist
        if (e.PropertyName == nameof(AudioViewModel.GetCurrentPlaylist))
        {
            // Update the song information when the playlist changes
            UpdateSongInformation();
        }
    }



    public void UpdateSongInformation()
    {
        BasePlaylist currentPlaylist = audioViewModel.GetCurrentPlaylist();
        List<Song> playlistSongs = currentPlaylist?.GetPlaylistSongs();
        int TrackCount = 0;
        if (playlistSongs != null)
        {
            for (int i = 0; i < playlistSongs.Count && i < 11; i++)
            {
                TrackCount++;
                Song song = playlistSongs[i];
                string songInfo = $"{TrackCount}. {song.GetSongName()} - {song.GetArtist()?.GetName()} - {song.GetAlbum()?.GetAlbumTitle()}";

                string albumName = audioViewModel.CurrentAlbumName;
                if (!string.IsNullOrEmpty(albumName))
                {
                    songInfo += $" - {albumName}";
                }

                // Assign songInfo to the appropriate property based on the index
                switch (i)
                {
                    case 0:
                        Song1 = songInfo;
                        break;
                    case 1:
                        Song2 = songInfo;
                        break;
                    case 2:
                        Song3 = songInfo;
                        break;
                    case 3:
                        Song4 = songInfo;
                        break;
                    case 4:
                        Song5 = songInfo;
                        break;
                    case 5:
                        Song6 = songInfo;
                        break;
                    case 6:
                        Song7 = songInfo;
                        break;
                    case 7:
                        Song8 = songInfo;
                        break;
                    case 8:
                        Song9 = songInfo;
                        break;
                    case 9:
                        Song10 = songInfo;
                        break;
                    case 10:
                        Song11 = songInfo;
                        break;
                }
            }

            // Notify property changes for all properties
            OnPropertyChanged(nameof(Song1));
            OnPropertyChanged(nameof(Song2));
            OnPropertyChanged(nameof(Song3));
            OnPropertyChanged(nameof(Song4));
            OnPropertyChanged(nameof(Song5));
            OnPropertyChanged(nameof(Song6));
            OnPropertyChanged(nameof(Song7));
            OnPropertyChanged(nameof(Song8));
            OnPropertyChanged(nameof(Song9));
            OnPropertyChanged(nameof(Song10));
            OnPropertyChanged(nameof(Song11));
        }
    }



    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
