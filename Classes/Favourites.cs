using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Betawave.Classes
{
    using System.Collections.Generic;

    public class Favourites : BasePlaylist
    {
        private List<Song> favouriteSongs = new List<Song>();

        public Favourites() : base()
        {
            // Load favourites from the database
        }

        public void AddToFavourites(Song song)
        {
            if (!favouriteSongs.Contains(song))
            {
                favouriteSongs.Add(song);
                // Add to database
            }
        }

        public bool RemoveFromFavourites(Song song)
        {
            bool removed = favouriteSongs.Remove(song);
            if (removed)
            {
                // Remove from database
            }
            return removed;
        }

        public void PrintFavourites()
        {
            Console.WriteLine("Favourite Songs:");
            foreach (Song song in favouriteSongs)
            {
                Console.WriteLine($"Song: {song.GetName()}");
            }
        }
    }

}
