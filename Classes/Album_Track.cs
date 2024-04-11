using System;
using Betawave.Classes;

namespace Betawave.Classes
{
    public class Album_Track
    {
        private int _trackNumber;
        private Song _song;

        public Album_Track(Song song, int trackNumber)
        {
            _song = song;
            _trackNumber = trackNumber;
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
            return _song;
        }

        public void SetSong(Song song)
        {
            _song = song;
        }

        public void PrintAlbumTrackDetails()
        {
            Console.WriteLine($"Track Number: {_trackNumber}");
            Console.WriteLine($"Song: {_song.GetName()}");
        }
    }
}
