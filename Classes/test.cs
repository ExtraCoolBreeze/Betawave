//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Betawave.Classes
//{
//    internal class test
//    {

//        public class Player
//        {
//            public string CurrentTrack { get; set; } = "No track loaded";
//            public string PlaybackState { get; set; } = "Stopped";
//            public int Volume { get; set; } = 5;
//            public TimeSpan CurrentTrackPosition { get; set; } = TimeSpan.Zero;
//            public bool Repeat { get; set; } = false;
//            public bool Shuffle { get; set; } = false;

//            private Random random = new Random(); // For generating random numbers

//            public List<string> Playlist { get; set; } = new List<string>(); // Your playlist

//            public string FilePath { get; private set; } = ""; // Added property to store file path

//            // Constructor
//            public Player()
//            {
//                // Initialization can be done here if needed
//            }

//            //Still need to implement correct volume controls.

//            public string LoadMusic(string songLocation)
//            {
//                FilePath = songLocation; // Set the song location to the FilePath property
//                return FilePath; // Return the file path
//            }

//            // PlayMusic function
//            public void PlayMusic()
//            {
//                if (Shuffle && Playlist.Count > 0)
//                {
//                    // Shuffle mode: Play tracks from the playlist in random order
//                    while (Playlist.Count > 0)
//                    {
//                        int trackIndex = random.Next(Playlist.Count); // Generate a random index
//                        FilePath = Playlist[trackIndex]; // Set FilePath to the selected track
//                        Console.WriteLine($"Playing track from shuffle: {FilePath}");

//                        Playlist.RemoveAt(trackIndex); // Optionally, remove the played track from the playlist
//                    }
//                }
//                else if (!string.IsNullOrEmpty(FilePath))
//                {
//                    // Non-shuffle mode: Play music from the FilePath
//                    PlaybackState = "Playing";
//                    Console.WriteLine($"Playing music from: {FilePath}");
//                }
//                else
//                {
//                    Console.WriteLine("No music file loaded. Please load a music file first.");
//                }
//            }


//            public void PauseMusic()
//            {
//                if (PlaybackState == "Playing")
//                {
//                    PlaybackState = "Paused";
//                    Console.WriteLine("Music paused.");
//                }
//                else
//                {
//                    Console.WriteLine("Music is not currently playing.");
//                }
//            }

//            // StopMusic function
//            public void StopMusic()
//            {
//                if (PlaybackState == "Playing" || PlaybackState == "Paused")
//                {
//                    PlaybackState = "Stopped";
//                    CurrentTrackPosition = TimeSpan.Zero; // Reset the track position
//                    Console.WriteLine("Music stopped.");
//                }
//                else
//                {
//                    Console.WriteLine("No music is currently playing.");
//                }
//            }

//            public void SkipMusic()
//            {
//                if (PlaybackState == "Playing" || PlaybackState == "Paused")
//                {
//                    // Here you could implement logic to move to the next track if you had a playlist
//                    Console.WriteLine("Skipping to next track...");

//                    // For demonstration, let's just simulate stopping the current track and 'loading' a new one
//                    StopMusic();
//                    LoadMusic("path/to/next/song.mp3");
//                    PlayMusic();
//                }
//                else
//                {
//                    Console.WriteLine("No music is currently playing to skip.");
//                }
//            }


//            //Needs more functionality, this may noit be correct
//            public void SetVolume(int newVolume)
//            {
//                Volume = newVolume; // This will automatically handle range checking and adjustment
//            }

//            // GetVolume function to return the current volume
//            public int GetVolume()
//            {
//                return Volume; // This directly accesses the Volume property's getter
//            }
//            public TimeSpan GetCurrentTrackPosition()
//            {
//                return CurrentTrackPosition; // Directly return the CurrentTrackPosition property
//            }

//            public void SetRepeat()
//            {
//                if (!Repeat)
//                {
//                    if (!IsLastTrackInPlaylist()) // This method needs to be implemented to check if the current track is the last one
//                    {
//                        PlayNextTrack(); // This method needs to be implemented to play the next track
//                        Repeat = true;
//                        Console.WriteLine("Repeat is on.");
//                    }
//                    else
//                    {
//                        StopMusic(); // Assuming StopMusic method is implemented
//                    }
//                }
//                else
//                {
//                    Repeat = false;
//                    Console.WriteLine("Repeat is off.");
//                }
//            }
//            public bool GetRepeat()
//            {
//                return Repeat; // Directly return the Repeat property
//            }

//            // Stub for IsLastTrackInPlaylist method
//            private bool IsLastTrackInPlaylist()
//            {
//                // Implement logic to check if the current track is the last in the playlist
//                // This is a placeholder implementation
//                return false; // For example purposes, always returns false
//            }

//            // Stub for PlayNextTrack method
//            private void PlayNextTrack()
//            {
//                // Implement logic to play the next track in the playlist
//                // This is a placeholder implementation
//                Console.WriteLine("Playing next track...");
//            }

//            public void SetShuffle()
//            {
//                Shuffle = !Shuffle; // Toggle shuffle state
//                Console.WriteLine($"Shuffle is {(Shuffle ? "on" : "off")}.");

//                if (Shuffle)
//                {
//                    PlayMusic(); // Play music in shuffle mode
//                }
//            }

//            public bool GetShuffle()
//            {
//                return Shuffle;
//            }

//            public void PrintMusicPlayerDetails()
//            {
//                Console.WriteLine($"Current Track: {CurrentTrack}");
//                Console.WriteLine($"Playback State: {PlaybackState}");
//                Console.WriteLine($"Volume: {Volume * 100}%");
//                Console.WriteLine($"Current Track Position: {CurrentTrackPosition}");
//                Console.WriteLine($"Repeat: {(Repeat ? "On" : "Off")}");
//                Console.WriteLine($"Shuffle: {(Shuffle ? "On" : "Off")}");
//            }
//        }
//    }
//}
