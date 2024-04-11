using System;
using System.Collections.Generic;

namespace Betawave.Classes
{
    public class Album
    {
        private int _albumId;
        private string _name;
        private List<Album_Track> _tracks;

        public Album()
        {
            _tracks = new List<Album_Track>();
        }

        public int GetAlbumId()
        {
            return _albumId;
        }

        public void SetAlbumId(int albumId)
        {
            _albumId = albumId;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public List<Album_Track> GetTracks()
        {
            return _tracks;
        }

        public void AddTrack(Album_Track track)
        {
            _tracks.Add(track);
        }

        public Album_Track FindTrackByNumber(int trackNumber)
        {
            foreach (var track in _tracks)
            {
                if (track.GetTrackNumber() == trackNumber) // Changed to use GetTrackNumber() method
                {
                    return track;
                }
            }
            return null;
        }

        public void PrintAlbumDetails()
        {
            Console.WriteLine($"Album ID: {_albumId}");
            Console.WriteLine($"Album Name: {_name}");
            Console.WriteLine("Tracks:");
            foreach (var track in _tracks)
            {
                track.PrintAlbumTrackDetails();
            }
        }
    }
}
