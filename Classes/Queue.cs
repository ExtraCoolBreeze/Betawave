using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    using System;

    public class Queue : BasePlaylist
    {
        // Assuming there's a field to indicate if the queue has been checked
        private bool checkQueued;

        public Queue() : base() // Calls the base constructor
        {
            checkQueued = false;
        }

        // Simplified version of CreatePlaylist to set the playlist title
        public string CreatePlaylist(string userInput)
        {
            // Placeholder for user input and database query logic
            // Set playlist title to user input
            SetTitle(userInput);
            // This is where you'd typically check if the playlist name exists in the database
            // and handle user input accordingly

            // For simplicity, just return the user input as the playlist name
            return userInput;
        }

        // Adds a song to the playlist; overrides the abstract method from BasePlaylist
        public override void AddToPlaylist(Playlist_Track track)
        {
            playlistTracks.Add(track); // Adds the track to the end of the playlist
        }

        // Implementation not provided in your pseudocode; removing the track is not typical for queues, but here's a simple implementation
        public override void RemoveFromPlaylist(Playlist_Track track)
        {
            playlistTracks.Remove(track); // Removes the track if it exists
        }

        // Prints details of each track in the playlist
        public override void PrintPlaylistDetails()
        {
            Console.WriteLine($"Queue ID: {GetPlaylistId()}, Title: {GetTitle()}");
            foreach (var track in playlistTracks)
            {
                track.PrintPlaylistTrack();
            }
        }

        // Sets the checkQueued flag based on user input
        public void SetCheckQueued(string userInput)
        {
            // Assuming userInput is expected to be "true" or "false"
            if (bool.TryParse(userInput, out bool result))
            {
                checkQueued = result;
            }
            else
            {
                Console.WriteLine("Invalid input for checkQueued.");
            }
        }

        public override void GetTrackLocations()
        {
            throw new NotImplementedException();
        }
    }

}
