using System;
using Betawave.Classes;

namespace Betawave.Classes
{
    public class Album_Track
    {
        private int fkalbum_id;
        private int _trackNumber;
        private int fksong_id;

        public Album_Track()
        {
            fksong_id = 0;
            _trackNumber = 0;
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

        public int GetSongId()
        {
            return fksong_id;
        }

        public void SetSongId(int songid)
        {
            fksong_id = songid;
        }

        public void PrintAlbumTrackDetails()
        {
            Console.WriteLine(GetAlbumId());
            Console.WriteLine($"Track Number: {_trackNumber}");
        }
    }
}
