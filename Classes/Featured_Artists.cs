using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{

    public class Featured_Artists
    {
        // Field to store the song artists
        private string songArtists;

        // Constructor without parameters, initializes songArtists to an empty string
        public Featured_Artists()
        {
            songArtists = "";
        }

        // Method to set the song artists
        public void SetSongArtists(string userInput)
        {
            songArtists = userInput; // Sets the songArtists to user input
        }

        // Function to return the song artists
        public string GetSongArtists()
        {
            return songArtists;
        }

        // Method to print the details of the featured artists class
        public void PrintFeaturedArtists()
        {
            Console.WriteLine(GetSongArtists()); // Prints the song artists
        }
    }

}
