using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class Artist
    {
        // Fields to store the artist's ID and name
        private int artistId;
        private string name;

        // Constructor without parameters, initializes artistId to 0 and name to an empty string
        public Artist()
        {
            artistId = 0;
            name = "";
        }

        // Method to increment and set the artist ID
        public void SetArtistId()
        {
            artistId += 1; // Increments the artistId by 1
        }

        // Function to return the artist ID
        public int GetArtistId()
        {
            return artistId;
        }

        // Method to set the artist's name
        public void SetName(string userInput)
        {
            name = userInput; // Sets the name to user input
        }

        // Function to return the artist's name
        public string GetName()
        {
            return name;
        }

        // Method to print the artist class details
        public void PrintArtistDetails()
        {
            Console.WriteLine(GetArtistId()); // Prints the artist ID
            Console.WriteLine(GetName()); // Prints the artist name
        }
    }
}
