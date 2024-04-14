using System;
using Betawave.Classes;

namespace Betawave.Classes
{
    public class Album_Track
    {
        private int _album_id;
        private int _trackNumber;
        private Song _song_id;

        public Album_Track(Song songid, int trackNumber)
        {
            _song_id = songid;
            _trackNumber = trackNumber;
        }

        public void SetAlbumId(int albumid)
        {
            _album_id = albumid;
        }

        public int GetAlbumId()
        {   
            return _album_id;
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
            return _song_id;
        }

        public void SetSong(Song songid)
        {
            _song_id = songid;
        }

        public void PrintAlbumTrackDetails()
        {
            Console.WriteLine(GetAlbumId());
            Console.WriteLine($"Track Number: {_trackNumber}");
            Console.WriteLine($"Song: {_song_id.GetName()}");
        }
    }
}
