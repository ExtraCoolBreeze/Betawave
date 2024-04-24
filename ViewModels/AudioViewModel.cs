using NAudio.Wave;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Betawave.ViewModels
{
    public class AudioViewModel : INotifyPropertyChanged
    {
        private Player audioPlayerService;
        private ICommand playPauseCommand;
        private ICommand stopCommand;
        private ICommand skipNextCommand;
        private ICommand skipPreviousCommand;
        private ICommand toggleShuffleCommand;
        private float volume = 1.0f;
        private bool shuffle;
        private string currentTrackName = "Track Name: ";
        private string currentTrackArtist = "Artist Name: ";
        private double currentTrackPosition = 0.00;
        private double trackLength = 0.00;
        private string currentTrackImage = "C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Images\\default.png"; // Default image

        public event PropertyChangedEventHandler PropertyChanged;

        public AudioViewModel(Player player)
        {
            playPauseCommand = new Command(TogglePlayPause);
            stopCommand = new Command(StopAudio);
            skipNextCommand = new Command(SkipNext);
            skipPreviousCommand = new Command(SkipPrevious);
            toggleShuffleCommand = new Command(ToggleShuffle);
            this.audioPlayerService = new Player();

            shuffle = audioPlayerService.GetShuffle();
            audioPlayerService.PlaybackStopped += HandlePlaybackStopped;
            audioPlayerService.TrackChanged += UpdateTrackDetails;
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

        public double CurrentTrackPosition
        {
            get => currentTrackPosition;
            set
            {
                if (currentTrackPosition != value)
                {
                    currentTrackPosition = value;
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
                    audioPlayerService.SetVolume(volume);
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
                    audioPlayerService.SetShuffle(shuffle);
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
            if (audioPlayerService.IsPlaying())
            {
                audioPlayerService.PauseMusic();
            }
            else
            {
                audioPlayerService.PlayMusic();
            }
        }

        public double TrackLength
        {
            get
            {
                if (audioPlayerService != null && audioPlayerService.GetCurrentTrackLength() != TimeSpan.Zero)
                {
                    return audioPlayerService.GetCurrentTrackLength().TotalSeconds;
                }
                else
                {
                    return 0; // Or appropriate default value
                }
            }
            set
            {
                if (trackLength != value)
                {
                    trackLength = value;
                    OnPropertyChanged();
                }
            }
        }


        public void SetPlaylistAndPlay(BasePlaylist playlist)
        {
            audioPlayerService.SetPlaylist(playlist);
            audioPlayerService.PlayMusic();
        }

        public void SkipNext()
        {
            audioPlayerService.PlayNextTrack();
        }

        public void SkipPrevious()
        {
            audioPlayerService.PlayPreviousTrack();
        }

        public void StopAudio()
        {
            audioPlayerService.StopMusic();
        }

        public void ToggleShuffle()
        {
            audioPlayerService.ToggleShuffle();
        }

        private void UpdateTrackDetails(object sender, EventArgs e)
        {
            CurrentTrackImage = audioPlayerService.GetCurrentTrackImage();
            CurrentTrackName = audioPlayerService.GetCurrentTrackName();
            CurrentTrackArtist = audioPlayerService.GetCurrentTrackArtist();
            CurrentTrackPosition = 0; // Reset or update according to actual progress
            TrackLength = audioPlayerService.GetCurrentTrackLength().TotalSeconds;  // Update length when track changes
            OnPropertyChanged(nameof(TrackLength));
        }


        public void HandlePlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                Console.WriteLine($"Playback Stopped due to an error: {e.Exception.Message}");
            }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
