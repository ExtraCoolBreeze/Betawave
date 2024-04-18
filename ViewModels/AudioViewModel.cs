
using NAudio.Wave;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Betawave.ViewModels
{
    public class AudioViewModel : INotifyPropertyChanged
    {
        private Player audioPlayerService = new Player();
        private ICommand playPauseCommand;
        private ICommand stopCommand;
        private ICommand skipNextCommand;
        private ICommand skipPreviousCommand;
        private ICommand toggleShuffleCommand;
        private float volume = 1.0f;
        private bool shuffle;
        private string currentTrackName;
        private string currentTrackArtist;
        private double currentTrackPosition;
        private double trackLength;

        public event PropertyChangedEventHandler PropertyChanged;


        public AudioViewModel()
        {
            playPauseCommand = new Command(TogglePlayPause);
            stopCommand = new Command(StopAudio);
            skipNextCommand = new Command(() => audioPlayerService.PlayNextTrack()); // Play next track
            skipPreviousCommand = new Command(() => audioPlayerService.PlayPreviousTrack()); // Play previous track
            toggleShuffleCommand = new Command(ToggleShuffle);

            shuffle = audioPlayerService.GetShuffle(); // Initialize shuffle state from the player
            audioPlayerService.OnPlaybackStopped += HandlePlaybackStopped; // Correctly subscribe to the event

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

        public double TrackLength
        {
            get => trackLength;
            set
            {
                if (trackLength != value)
                {
                    trackLength = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Volume
        {
            get => volume;
            set
            {
                if (Math.Abs(volume - value) > 0.01) // Adding a tolerance to prevent unnecessary updates
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

        public void TogglePlayPause()
        {
            if (audioPlayerService.IsPlaying)
            {
                audioPlayerService.PauseMusic();
            }
            else
            {
                audioPlayerService.PlayMusic();
            }
        }

        public void HandlePlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                Console.WriteLine($"Playback Stopped due to an error: {e.Exception.Message}");
            }
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

        public void SkipAudio()
        {
            audioPlayerService.SkipMusic();
        }

        public void ToggleShuffle()
        {
            audioPlayerService.ToggleShuffle();
            
        }

        private void UpdateTrackDetails(object sender, EventArgs e)
        {
            // Update the track name and artist from your player
            CurrentTrackName = audioPlayerService.GetCurrentTrackName();
            CurrentTrackArtist = audioPlayerService.GetCurrentTrackArtist(); 
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}