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
        public int TrackNumber { get; set; }
        public Song Song { get; set; }

        public Album_Track(Song song, int trackNumber)
        {
            Song = song;
            TrackNumber = trackNumber;
        }

        public void PrintAlbumTrackDetails()
        {
            Console.WriteLine($"Track Number: {TrackNumber}");
            Console.WriteLine($"Song: {Song.GetName()}");
        }
    }
}
