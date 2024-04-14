using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls; // Ensure you include this if using Commands in MAUI

namespace Betawave.ViewModels
{
    public class TestView : INotifyPropertyChanged
    {
        private Player audioPlayerService;
        private ICommand playCommand;
        private ICommand pauseCommand;
        private ICommand stopCommand;
        private ICommand skipCommand;
        private ICommand toggleShuffleCommand;
        private float volume = 0.5f; // Set default volume
        private bool shuffle;

        public TestView()
        {
            audioPlayerService = new Player(); // Ensure player is initialized

            playCommand = new Command(PlayAudio);
            pauseCommand = new Command(PauseAudio);
            stopCommand = new Command(StopAudio);
            skipCommand = new Command(SkipAudio);
            toggleShuffleCommand = new Command(ToggleShuffle);

            shuffle = audioPlayerService.GetShuffle(); // Initialize shuffle state from the player
        }

        public ICommand PlayCommand => playCommand;
        public ICommand PauseCommand => pauseCommand;
        public ICommand StopCommand => stopCommand;
        public ICommand SkipCommand => skipCommand;
        public ICommand ToggleShuffleCommand => toggleShuffleCommand;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PlayAudio()
        {
            string filePath = @"C:\Users\Craig\Desktop\Betawave8.0\Betawave\Resources\Music\FM\02 This Town (Featuring Tippa Irie &.mp3";
            if (File.Exists(filePath))
            {
                audioPlayerService.LoadAndPlayMusic(filePath);
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
            }
        }


        private void PauseAudio()
        {
            audioPlayerService.PauseMusic();
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
    }
}
