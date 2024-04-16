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
        private Song fksong;
        private Artist fkartist;

        // Constructor
        public Featured_Artists()
        {

        }

        // Getter method for Song
        public Song GetSong()
        {
            return fksong;
        }

        // Setter method for Song
        public void SetSong(Song value)
        {
            fksong = value;
        }

        // Getter method for Artist
        public Artist GetArtist()
        {
            return fkartist;
        }

        // Setter method for Artist
        public void SetArtist(Artist value)
        {
            fkartist = value;
        }
    }
}
