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
        private Song fksongId;
        private Artist fkartistId;

        // Constructor
        public Featured_Artists()
        {
            fksongId = new Song();
            fkartistId = new Artist();
        }

        // Getter method for Song
        public Song GetSongId()
        {
            return fksongId;
        }

        // Setter method for Song
        public void SetSongId(Song value)
        {
            fksongId = value;
        }

        // Getter method for Artist
        public Artist GetArtistId()
        {
            return fkartistId;
        }

        // Setter method for Artist
        public void SetArtistId(Artist value)
        {
            fkartistId = value;
        }
    }
}
