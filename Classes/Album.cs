using System;
using System.Collections.Generic;

namespace Betawave.Classes
{
    public class Album
    {
        private int pkalbum_id;
        private string _album_title;
        private string _image_location;
        private List<Album_Track> _tracks;

        public Album()
        {
            pkalbum_id = 0;
            _album_title = "";
            _image_location = "";
            _tracks = new List<Album_Track>();
        }

        public int GetAlbumId()
        {
            return pkalbum_id;
        }

        public void SetAlbumId(int albumId)
        {
            pkalbum_id = albumId;
        }

        public string GetAlbumTitle()
        {
            return _album_title;
        }

        public void SetAlbumTitle(string title)
        {
            _album_title = title;
        }

        public void SetImageLocation(string imagelocation)
        {
            _image_location = imagelocation;
        }

        public string GetImageLocation()
        {
            return _image_location;
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
            Console.WriteLine(GetAlbumId());
            Console.WriteLine(GetAlbumTitle());
            Console.WriteLine("Tracks:");
            foreach (var track in _tracks)
            {
                track.PrintAlbumTrackDetails();
            }
        }
    }
}
