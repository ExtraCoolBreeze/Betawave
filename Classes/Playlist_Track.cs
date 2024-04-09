using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    // YET TO COMPLETE THIS CLASS!!!

    public class Playlist_Track
    {
        // Fields to store the track's title, track number, artist, and duration
        private string title;
        private int trackNumber;
        private string artist;
        private TimeSpan duration;

        // Constructor without parameters, initializes fields
        public Playlist_Track()
        {
            title = "";
            trackNumber = 0;
            artist = "";
            duration = new TimeSpan(); // Initializes duration to 0
        }

        // Method to set the track number. Assumes trackNumber can be parsed from the string input
        public void SetTrackNumber(string userInput)
        {
            if (int.TryParse(userInput, out int parsedTrackNumber))
            {
                trackNumber = parsedTrackNumber; // Sets trackNumber if userInput is a valid integer
            }
            else
            {
                Console.WriteLine("Invalid input for track number.");
            }
        }

        // Function to return the track number
        public int GetTrackNumber()
        {
            return trackNumber;
        }

        // Method to print the details of the playlist track
        public void PrintPlaylistTrack()
        {
            Console.WriteLine($"Title: {title}, Track Number: {GetTrackNumber()}, Artist: {artist}, Duration: {duration}");
        }
    }

}
