using System;
using Betawave.Classes;

namespace Betawave.Classes
{
    public class Album_Track
    {
        private int fkalbum_id;
        private int _trackNumber;
        private Song fksong_id;

        public Album_Track(Song songid, int trackNumber)
        {
            fksong_id = songid;
            _trackNumber = trackNumber;
        }

        public void SetAlbumId(int albumid)
        {
            fkalbum_id = albumid;
        }

        public int GetAlbumId()
        {   
            return fkalbum_id;
        }

        public int GetTrackNumber()
        {
            return _trackNumber;
        }

        public void SetTrackNumber(int trackNumber)
        {
            _trackNumber = trackNumber;
        }

        public Song GetSong()
        {
            return fksong_id;
        }

        public void SetSong(Song songid)
        {
            fksong_id = songid;
        }

        public void PrintAlbumTrackDetails()
        {
            Console.WriteLine(GetAlbumId());
            Console.WriteLine($"Track Number: {_trackNumber}");
            Console.WriteLine($"Song: {fksong_id.GetName()}");
        }
    }
}
