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


        public event PropertyChangedEventHandler PropertyChanged;

        public AudioViewModel()
        {
            volume = 1.0f;
            currentTrackName = "Track Name: ";
            currentTrackArtist = "Artist Name: ";
            currentAlbumName = "Album Name: ";
            shuffleOn = "Shuffle Off";
            currentTrackImage = "C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Images\\default.png";

            playPauseCommand = new Command(TogglePlayPause);
            stopCommand = new Command(StopAudio);
            skipNextCommand = new Command(SkipNext);
            skipPreviousCommand = new Command(SkipPrevious);
            toggleShuffleCommand = new Command(ToggleShuffle);
            toggleRepeatCommand = new Command(ToggleRepeat);
            audioPlayer = new Player();

            shuffle = audioPlayer.GetShuffle();
            audioPlayer.PlaybackStopped += HandlePlaybackStoppedAndStopTimer;

            audioPlayer.TrackChanged += UpdateTrackDetails;
        }

        public ICommand PlayPauseCommand => playPauseCommand;
        public ICommand StopCommand => stopCommand;
        public ICommand SkipNextCommand => skipNextCommand;
        public ICommand SkipPreviousCommand => skipPreviousCommand;
        public ICommand ToggleShuffleCommand => toggleShuffleCommand;

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

        public string TrackLength
        {
            get
            {
                TimeSpan length = TimeSpan.FromSeconds(trackLengthInSeconds);
                return string.Format("{0}:{1:00}", (int)length.TotalMinutes, length.Seconds);
            }
        }



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



        public void SetPlaylistAndPlay(BasePlaylist playlist)
        {
            audioPlayer.SetPlaylist(playlist);
            audioPlayer.PlayMusic();
        }

        public void SkipNext()
        {
            audioPlayer.PlayNextTrack();
        }

        public void SkipPrevious()
        {
            audioPlayer.PlayPreviousTrack();
        }

        public void StopAudio()
        {
            audioPlayer.StopMusic();
        }

        public void ToggleShuffle()
        {
            audioPlayer.ToggleShuffle();
        }

        public void ToggleRepeat()
        {
            audioPlayer.ToggleRepeat();
        }

        public BasePlaylist GetCurrentPlaylist()
        {
            return audioPlayer.GetCurrentPlaylist();
        }

        private async void UpdateTrackDetails(object sender, EventArgs e)
        {
            Console.WriteLine("Updating track details.");
            CurrentTrackName = audioPlayer.GetCurrentTrackName();
            CurrentTrackArtist = audioPlayer.GetCurrentTrackArtist();
            CurrentAlbumName = audioPlayer.GetCurrentAlbumName();
            CurrentTrackImage = await audioPlayer.GetCurrentTrackImage();
            trackLengthInSeconds = audioPlayer.GetCurrentTrackLength().TotalSeconds;
            Console.WriteLine($"Updated Artist: {CurrentTrackArtist}");

            OnPropertyChanged(nameof(TrackLength));
            OnPropertyChanged(nameof(CurrentTrackName));
            OnPropertyChanged(nameof(CurrentTrackArtist));
            OnPropertyChanged(nameof(CurrentAlbumName));
            OnPropertyChanged(nameof(CurrentTrackImage));
        }


        public void HandlePlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                Console.WriteLine($"Playback Stopped due to an error: {e.Exception.Message}");
            }
        }

        private void HandlePlaybackStoppedAndStopTimer(object sender, StoppedEventArgs e)
        {
            // Invoke both event handlers
            HandlePlaybackStopped(sender, e);
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
