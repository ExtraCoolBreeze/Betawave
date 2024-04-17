
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
        private ICommand skipCommand;
        private ICommand toggleShuffleCommand;
        private float volume = 1.0f;
        private bool shuffle;
        private TimeSpan currentCount;
        private string maxDuration;

        public AudioViewModel()
        {
            playPauseCommand = new Command(TogglePlayPause);
            stopCommand = new Command(StopAudio);
            skipCommand = new Command(SkipAudio);
            toggleShuffleCommand = new Command(ToggleShuffle);

            shuffle = audioPlayerService.GetShuffle(); // Initialize shuffle state from the player
            audioPlayerService.OnPlaybackStopped -= HandlePlaybackStopped;

        }

        public ICommand PlayPauseCommand
        {
            get { return playPauseCommand; }
        }

        public ICommand StopCommand
        {
            get { return stopCommand; }
            set { stopCommand = value; }
        }

        public ICommand SkipCommand
        {
            get { return skipCommand; }
            set { skipCommand = value; }
        }

        public ICommand ToggleShuffleCommand
        {
            get { return toggleShuffleCommand; }
            set { toggleShuffleCommand = value; }
        }

        public float Volume
        {
            get { return volume; }
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
            get { return shuffle; }
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

        private void TogglePlayPause()
        {
            // Check the current playing state and toggle accordingly
            if (audioPlayerService.IsPlaying)
            {
                audioPlayerService.PauseMusic();
               // PlayPauseIcon = "play.png";
            }
            else
            {
                audioPlayerService.PlayMusic();
                //PlayPauseIcon = "pause.png";
            }
        }

        private void HandlePlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
                Console.WriteLine($"Playback Stopped due to an error: {e.Exception.Message}");
        }

        private void StopAudio()
        {
            audioPlayerService.StopMusic();
        }

        private void SkipAudio()
        {
            audioPlayerService.SkipMusic();
        }

        private void ToggleShuffle()
        {
            audioPlayerService.ToggleShuffle();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}