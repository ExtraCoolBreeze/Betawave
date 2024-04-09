using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    using System;

    public class Album_Track
    {
        // Field to store the track number
        private int trackNumber;

        // Constructor without parameters, initializes trackNumber to 0
        public Album_Track()
        {
            trackNumber = 0;
        }

        // Method to set the track number
        public void SetTrackNumber(int userInput)
        {
            trackNumber = userInput;
        }

        // Function to get the track number
        public int GetTrackNumber()
        {
            return trackNumber;
        }

        // Method to print the details of the album track
        public void PrintAlbumTrackDetails()
        {
            Console.WriteLine(GetTrackNumber());
        }
    }

}
