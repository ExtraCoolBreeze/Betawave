using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{

    public class Song
    {
        private static int _nextSongId = 1;
        public int SongId { get; private set; }
        public string Name { get; set; }
        public List<Featured_Artists> FeaturedArtists { get; set; } = new List<Featured_Artists>();

        public string duration;

        public string songLocation;

        public Song(string name)
        {
            this.SongId = _nextSongId++;
            this.Name = name;
        }

        // Getter for SongId, no setter since SongId should be read-only after initialization
        public int GetSongId()
        {
            return SongId;
        }

        // Getter and Setter for Name

        // Getter and Setter for Duration
        public string GetDuration()
        {
            return duration;
        }

        public void SetDuration(string durationValue)
        {
            duration = durationValue;
        }

        // Getter and Setter for SongLocation
        public string GetSongLocation()
        {
            return songLocation;
        }

        public void SetSongLocation(string locationValue)
        {
            songLocation = locationValue;
        }

        public void AddFeaturedArtist(Artist artist)
        {
            var featuredArtist = new Featured_Artists(this, artist);
            FeaturedArtists.Add(featuredArtist);
            artist.FeaturedSongs.Add(featuredArtist);
        }

        public void PrintSongDetails()
        {
            Console.WriteLine($"Song: {Name}");
            foreach (var featured in FeaturedArtists)
            {
                Console.WriteLine($"Featured Artist: {featured.Artist.Name}");
            }
        }
    }


}
