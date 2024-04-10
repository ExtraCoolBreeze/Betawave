using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class Featured_Artists
    {
        public Song Song { get; set; }
        public Artist Artist { get; set; }

        public Featured_Artists(Song song, Artist artist)
        {
            this.Song = song;
            this.Artist = artist;
        }
    }
}

