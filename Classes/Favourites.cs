using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class Favourites : BasePlaylist
    {
        // Field to store a favourite attribute, such as a favourite song or artist
        private string favourite;

        public Favourites() : base() // Calls the base constructor
        {
            favourite = "";
        }

        // Simplified version of CreatePlaylist to set the playlist title
        public string CreatePlaylist(string userInput)
        {
            // This is where you'd typically check if the playlist name exists in the database
            // and handle user input accordingly. For simplicity, just set the title.
            SetTitle(userInput);

            // Return the user input as the playlist name for simplicity
            return userInput;
        }

        // Sets the favourite attribute
        public void SetFavourite(string userInput)
        {
            favourite = userInput;
        }

        // Returns the favourite attribute
        public string GetFavourite()
        {
            return favourite;
        }

        // Adds a song to the playlist; overrides the abstract method from BasePlaylist
        public override void AddToPlaylist(Playlist_Track track)
        {
            playlistTracks.Add(track);
        }

        // Implementation not provided in your pseudocode; removing the track is not typical for a favourites list, but here's a simple implementation
        public override void RemoveFromPlaylist(Playlist_Track track)
        {
            playlistTracks.Remove(track);
        }

        // Prints details of each track in the playlist, along with the favourite attribute
        public override void PrintPlaylistDetails()
        {
            Console.WriteLine($"Favourites Playlist ID: {GetPlaylistId()}, Title: {GetTitle()}, Favourite: {GetFavourite()}");
            foreach (var track in playlistTracks)
            {
                track.PrintPlaylistTrack();
            }
        }
    }
}
