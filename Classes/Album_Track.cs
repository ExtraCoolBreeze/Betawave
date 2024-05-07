/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was designed to store information in the Album_Track junction table, it was created based on the class daigram and database but was left unused. Left in as proof of work
*/

/*using System;
using Betawave.Classes;

namespace Betawave.Classes
{
    public class Album_Track
    {
        private int fkalbumId;
        private int trackNumber;
        private int fksongid;

        public Album_Track()
        {
            fksongid = 0;
            trackNumber = 0;
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
            return trackNumber;
        }

        public void SetTrackNumber(int value)
        {
            this.trackNumber = value;
        }

        public int GetSongId()
        {
            return fksongid;
        }

        public void SetSongId(int songid)
        {
            fksongid = songid;
        }

        public int AlbumId
        {
            get => fkalbumId;
            set => fkalbumId = value;
        }

        public int TrackNumber
        {
            get => trackNumber;
            set => trackNumber = value;
        }

        public int SongId
        {
            get => fksongid;
            set => fksongid = value;
        }

        public void PrintAlbumTrackDetails()
        {
            Console.WriteLine(GetAlbumId());
            Console.WriteLine($"Track Number: {trackNumber}");
        }
    }
}
*/