using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class Artist
    {
        private int artistId;
        private string name;
        public List<Featured_Artists> FeaturedSongs { get; set; } = new List<Featured_Artists>();

        public Artist(string name)
        {
            this.artistId = 0; // This ID should ideally be set uniquely, possibly managed by the database or a higher-level application logic
            this.name = name;
        }

        public string Name => name;

        public void PrintArtistDetails()
        {
            Console.WriteLine($"Artist Name: {Name}");
            foreach (var featured in FeaturedSongs)
            {
                Console.WriteLine($"Featured in Song: {featured.Song.Name}");
            }
        }
    }
}

