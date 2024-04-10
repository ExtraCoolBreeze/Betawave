using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    using System;

    public class Album
    {
        public int AlbumId { get; private set; }
        public string Name { get; set; }
        public List<Album_Track> Tracks { get; private set; }

        public Album()
        {
            Tracks = new List<Album_Track>();
        }

        public void AddTrack(Song song, int trackNumber)
        {
            var track = new Album_Track(song, trackNumber);
            Tracks.Add(track);
        }

        public Album_Track FindTrackByNumber(int trackNumber)
        {
            return Tracks.FirstOrDefault(t => t.TrackNumber == trackNumber);
        }

        public void PrintAlbumDetails()
        {
            Console.WriteLine($"Album ID: {AlbumId}");
            Console.WriteLine($"Album Name: {Name}");
            Console.WriteLine("Tracks:");
            foreach (var track in Tracks)
            {
                track.PrintAlbumTrackDetails();
            }
        }
    }
}