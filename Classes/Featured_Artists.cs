using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This class is functionally complete and acts as a join between artist and song

namespace Betawave.Classes
{
    public class Featured_Artists
    {
        private Song _song;
        private Artist _artist;

        // Constructor
        public Featured_Artists()
        {

        }

        // Getter method for Song
        public Song GetSong()
        {
            return _song;
        }

        // Setter method for Song
        public void SetSong(Song value)
        {
            _song = value;
        }

        // Getter method for Artist
        public Artist GetArtist()
        {
            return _artist;
        }

        // Setter method for Artist
        public void SetArtist(Artist value)
        {
            _artist = value;
        }
    }
}
