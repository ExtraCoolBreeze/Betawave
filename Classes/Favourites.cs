using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Betawave.Classes
{
    public class Favourites : BasePlaylist
    {
        // Field to store a favourite attribute, such as a favourite song or artist
        private string _favourite;

        public Favourites() : base() // Calls the base constructor
        {
            _favourite = "";
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
            _favourite = userInput;
        }

        // Returns the favourite attribute
        public string GetFavourite()
        {
            return _favourite;
        }

        // Adds a song to the playlist; overrides the abstract method from BasePlaylist
        public override void AddToPlaylist(Playlist_Track track)
        {
            // Your subclass-specific logic here
            base._playlistTracks.Add(track);
        }

        public override void RemoveFromPlaylist(Playlist_Track track)
        {
            // Your subclass-specific logic here
            base._playlistTracks.Remove(track);
        }

        // Prints details of each track in the playlist, along with the favourite attribute
        public override void PrintPlaylistDetails()
        {
            Console.WriteLine($"Favourites Playlist Title: {GetTitle()}, Favourite: {_favourite}");
            foreach (var track in base._playlistTracks)
            {
                track.PrintPlaylistTrack();
            }
        }

        // Returns a list of locations of all tracks in the playlist
        public override List<string> GetTrackLocations()
        {
            List<string> trackLocations = new List<string>();
            foreach (var track in base._playlistTracks)
            {
                trackLocations.Add(track.GetTrackUri());
            }
            return trackLocations;
        }
    }
}
