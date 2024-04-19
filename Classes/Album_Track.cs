using System;
using Betawave.Classes;

namespace Betawave.Classes
{
    public class Album_Track
    {
        private int fkalbumId;
        private int TrackNumber;
        private int fksongid;

        public Album_Track()
        {
            fksongid = 0;
            TrackNumber = 0;
        }

        public void SetAlbumId(int albumid)
        {
            fkalbumId = albumid;
        }

        public int GetAlbumId()
        {   
            return fkalbumId;
        }

        public int GetTrackNumber()
        {
            return TrackNumber;
        }

        public void SetTrackNumber(int trackNumber)
        {
            TrackNumber = trackNumber;
        }

        public int GetSongId()
        {
            return fksongid;
        }

        public void SetSongId(int songid)
        {
            fksongid = songid;
        }

        public void PrintAlbumTrackDetails()
        {
            Console.WriteLine(GetAlbumId());
            Console.WriteLine($"Track Number: {TrackNumber}");
        }
    }
}
