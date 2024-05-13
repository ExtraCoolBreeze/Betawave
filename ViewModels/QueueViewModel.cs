/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This view model was created to manage the interactions, updating and displaying of the UI on the Queue content page  */

using System.ComponentModel;
using System.Windows.Input;
using Betawave.ViewModels;
using System.Runtime.CompilerServices;
using Betawave.Classes;
//declaring class and inheriting
public class QueueViewModel : INotifyPropertyChanged
{   //declaring event
    public event PropertyChangedEventHandler PropertyChanged;
    //declaring class objects
    public DatabaseAccess dbAccess;
    public PlaylistManager playlistManager;
    public BasePlaylist playlist;
    private AudioViewModel audioViewModel;
    //creating player comments
    public ICommand PlayPauseCommand { get; private set; }
    public ICommand StopCommand { get; private set; }
    public ICommand SkipNextCommand { get; private set; }
    public ICommand SkipPreviousCommand { get; private set; }
    public ICommand ToggleShuffleCommand { get; private set; }
    public ICommand ToggleRepeatCommand { get; private set; }

    //accessing class properties
    public string CurrentTrackName => audioViewModel.CurrentTrackName;
    public string CurrentTrackArtist => audioViewModel.CurrentTrackArtist;
    public string CurrentTrackImage => audioViewModel.CurrentTrackImage;
    public string TrackLength => audioViewModel.TrackLength;

    //creating volumne parameter
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

    //creating shuffle parameter
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

    // creating song properties
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

    //class constructor, passing in view model
    public QueueViewModel(AudioViewModel audioViewModel)
    {
        //initialising objects
        this.audioViewModel = audioViewModel;
        dbAccess = new DatabaseAccess();
        playlistManager = new PlaylistManager( dbAccess);
        playlist = new BasePlaylist();

        //initialising commands
        PlayPauseCommand = new Command(() => audioViewModel.TogglePlayPause());
        StopCommand = new Command(() => audioViewModel.StopAudio());
        SkipNextCommand = new Command(() => audioViewModel.SkipNext());
        SkipPreviousCommand = new Command(() => audioViewModel.SkipPrevious());
        ToggleShuffleCommand = new Command(() => audioViewModel.ToggleShuffle());
        this.audioViewModel.PropertyChanged += AudioViewModel_PropertyChanged;
        UpdateSongInformation();
    }

    /// <summary>
    /// this method checks for property changes in the audioviewmodel class
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public async void AudioViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(e.PropertyName);
        // Check if the property changed is related to the playlist
        if (e.PropertyName == nameof(AudioViewModel.GetCurrentPlaylist))
        {
            // Update the song information when playlist changes
            await UpdateSongInformation();
        }
    }


    /// <summary>
    /// When called this function updates the queue page based on the playlist loaded into the player
    /// </summary>
    /// <returns></returns>
    public async Task UpdateSongInformation()
    {
        
        BasePlaylist currentPlaylist = audioViewModel.GetCurrentPlaylist();

        //have fixes already but remove it for test and add it back in to show correction
        if (currentPlaylist == null)
        {
            return; // Exit if no valid playlist is available
        }
        //get playlist of songs and count tracks
        List<Song> playlistSongs = currentPlaylist.GetPlaylistSongs();
        int TrackCount = 0;
        //if songlist is not empty
        if (playlistSongs != null)
        {   //for each element tick up track count and add song details to object
            for (int i = 0; i < playlistSongs.Count && i < 11; i++)
            {
                TrackCount++;
                Song song = playlistSongs[i];
                string songName = song.GetSongName(); //get song name
                playlist = await playlistManager.GetDetailsForPlaylist(songName); //get details related to song name in database

                string artistName = playlist.GetArtistName(); //get artist name
                string albumName = playlist.GetAlbumName(); //get album name
                string songInfo = $"{TrackCount}. {song.GetSongName()} - {artistName} - {albumName}"; //update ui based on track count and details



                //uses switch statement to update song information
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

            //this is to trigger event paged on the song name changes
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


    /// <summary>
    /// This method updates pages based on event triggers
    /// </summary>
    /// <param name="propertyName"></param>
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
