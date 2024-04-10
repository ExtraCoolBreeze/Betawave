using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
namespace Betawave
{

    public class Player
    {
        public MediaElement MediaElement { get; set; } // .NET MAUI MediaElement
        public BasePlaylist CurrentPlaylist { get; set; }
        private Random random = new Random();
        public bool Shuffle { get; set; } = false;
        private int currentTrackIndex = -1;

        public Player(MediaElement mediaElement)
        {
            MediaElement = mediaElement;
            MediaElement.MediaEnded += OnMediaEnded; // Handle media ended event
        }

        private void OnMediaEnded(object sender, EventArgs e)
        {
            if (Shuffle)
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
            MediaElement.Source = new Uri(songLocation);
        }

        public void PlayMusic()
        {
            if (MediaElement.Source != null)
            {
                MediaElement.Play();
            }
        }

        public void PauseMusic()
        {
            MediaElement.Pause();
        }

        public void StopMusic()
        {
            MediaElement.Stop();
        }

        public void SetVolume(double volume)
        {
            MediaElement.Volume = volume; // Volume is a value between 0.0 and 1.0
        }

        public double GetVolume()
        {
            return MediaElement.Volume;
        }

        public void SetShuffle(bool shuffle)
        {
            Shuffle = shuffle;
        }

        public void SkipMusic()
        {
            if (Shuffle)
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
            if (Playlist.Count > 0)
            {
                currentTrackIndex = (currentTrackIndex + 1) % Playlist.Count;
                LoadMusic(Playlist[currentTrackIndex]);
                PlayMusic();
            }
        }

        public void PlayRandomTrack()
        {
            if (Playlist.Count > 0)
            {
                int trackIndex = random.Next(Playlist.Count);
                LoadMusic(Playlist[trackIndex]);
                PlayMusic();
            }
        }

        public void AddToPlaylist(string track)
        {
            Playlist.Add(track);
        }

        public void RemoveFromPlaylist(string track)
        {
            Playlist.Remove(track);
        }
    }
}