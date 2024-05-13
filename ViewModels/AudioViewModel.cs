/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This view model was the first created and was designed to allow the user to interact with the player class through the media player UI,
this was originally added to every page however this changed after more view models were created. */

using NAudio.Wave;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Betawave.ViewModels
{
    public class AudioViewModel : INotifyPropertyChanged
    {
        //declaring class objects, commands and variables
        private Player audioPlayer;
        private ICommand playPauseCommand;
        private ICommand stopCommand;
        private ICommand skipNextCommand;
        private ICommand skipPreviousCommand;
        private ICommand toggleShuffleCommand;
        private ICommand toggleRepeatCommand;

        private float volume;
        private bool shuffle;
        private string currentTrackName;
        private string currentTrackArtist;
        private string currentAlbumName;
        private string shuffleOn;
        private string currentTrackImage;
        private double trackLengthInSeconds;

        //declaring events
        public event PropertyChangedEventHandler PropertyChanged;

        //class constructor
        public AudioViewModel()
        {
            //initialising variables
            volume = 1.0f;
            currentTrackName = "Track Name: ";
            currentTrackArtist = "Artist Name: ";
            currentAlbumName = "Album Name: ";
            shuffleOn = "Shuffle Off";
            currentTrackImage = "C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Images\\default.png";

            //initialising commands
            playPauseCommand = new Command(TogglePlayPause);
            stopCommand = new Command(StopAudio);
            skipNextCommand = new Command(SkipNext);
            skipPreviousCommand = new Command(SkipPrevious);
            toggleShuffleCommand = new Command(ToggleShuffle);
            toggleRepeatCommand = new Command(ToggleRepeat);
            audioPlayer = new Player();

            shuffle = audioPlayer.GetShuffle();

            audioPlayer.TrackChanged += UpdateTrackDetails;
        }

        //defining command properties
        public ICommand PlayPauseCommand => playPauseCommand;
        public ICommand StopCommand => stopCommand;
        public ICommand SkipNextCommand => skipNextCommand;
        public ICommand SkipPreviousCommand => skipPreviousCommand;
        public ICommand ToggleShuffleCommand => toggleShuffleCommand;

        //creating track name property
        public string CurrentTrackName
        {
            get => currentTrackName;
            set
            {
                if (currentTrackName != value)
                {
                    currentTrackName = value;
                    OnPropertyChanged();
                }
            }
        }

        //creating track artist property
        public string CurrentTrackArtist
        {
            get => currentTrackArtist;
            set
            {
                if (currentTrackArtist != value)
                {
                    currentTrackArtist = value;
                    OnPropertyChanged();
                }
            }
        }

        //creating volume property
        public float Volume
        {
            get => volume;
            set
            {
                if (Math.Abs(volume - value) > 0.01)
                {
                    volume = value;
                    OnPropertyChanged();
                    audioPlayer.SetVolume(volume);
                }
            }
        }
        //creating shuffle property
        public bool Shuffle
        {
            get => shuffle;
            set
            {
                if (shuffle != value)
                {
                    shuffle = value;
                    OnPropertyChanged();
                    audioPlayer.SetShuffle(shuffle);
                }
            }
        }

        //creating track image property
        public string CurrentTrackImage
        {
            get => currentTrackImage;
            set
            {
                if (currentTrackImage != value)
                {
                    currentTrackImage = value;
                    OnPropertyChanged();
                }
            }
        }

        //creating track length property
        public string TrackLength
        {
            get
            {   
                TimeSpan length = TimeSpan.FromSeconds(trackLengthInSeconds);
                return string.Format("{0}:{1:00}", (int)length.TotalMinutes, length.Seconds);
            }
        }


        //creating album name property
        public string CurrentAlbumName
        {
            get => currentAlbumName;
            set
            {
                if (currentAlbumName != value)
                {
                    currentAlbumName = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// When called this method toggles the playing and pausing of music methods in the audio player
        /// </summary>
        public void TogglePlayPause()
        {
            if (audioPlayer.IsPlaying())
            {
                audioPlayer.PauseMusic();
            }
            else
            {
                audioPlayer.PlayMusic();
            }
        }

        //When called and passed a playlist this method calls further methods in from the audio player and loads in the playlist and starts playing it 
        public void SetPlaylistAndPlay(BasePlaylist playlist)
        {
            audioPlayer.SetPlaylist(playlist);
            audioPlayer.PlayMusic();
        }

        //This method calls another method within the audio player to skip to the next track
        public void SkipNext()
        {
            audioPlayer.PlayNextTrack();
        }

        //This method calls another method within the audio player to skip to the previous track
        public void SkipPrevious()
        {
            audioPlayer.PlayPreviousTrack();
        }

        //This method calls another method within the audio player to top the music
        public void StopAudio()
        {
            audioPlayer.StopMusic();
        }

        //This method calls another method within the audio player to toggle shuffle
        public void ToggleShuffle()
        {
            audioPlayer.ToggleShuffle();
        }

        //This method calls another method within the audio player to toggle repeat
        public void ToggleRepeat()
        {
            audioPlayer.ToggleRepeat();
        }

        //This method calls another method within the audio player to return the current loaded in playlist
        public BasePlaylist GetCurrentPlaylist()
        {
            return audioPlayer.GetCurrentPlaylist();
        }

        /// <summary>
        /// When called this method updates properties based on changes in the audio player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UpdateTrackDetails(object sender, EventArgs e)
        {
            CurrentTrackName = audioPlayer.GetCurrentTrackName();
            CurrentTrackArtist = audioPlayer.GetCurrentTrackArtist();
            CurrentAlbumName = audioPlayer.GetCurrentAlbumName();
            CurrentTrackImage = await audioPlayer.GetCurrentTrackImage();
            trackLengthInSeconds = audioPlayer.GetCurrentTrackLength().TotalSeconds;

            OnPropertyChanged(nameof(TrackLength));
            OnPropertyChanged(nameof(CurrentTrackName));
            OnPropertyChanged(nameof(CurrentTrackArtist));
            OnPropertyChanged(nameof(CurrentAlbumName));
            OnPropertyChanged(nameof(CurrentTrackImage));
        }

        /// <summary>
        /// This method triggers of property changes
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
