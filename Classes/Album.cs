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
        private List<Song> AlbumSongs;

        public Album()
        {
            pkalbum_id = 0;
            _album_title = "";
            _image_location = "";
            _tracks = new List<Album_Track>();
            AlbumSongs = new List<Song>();
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

        //------------------------------------------------------- functions to store list of album songs

        public List<Song> GetAlbumSongs()
        { 
            return AlbumSongs;
        }

        public void SetAlbumSongs(List<Song> songData)
        {
            AlbumSongs = songData;
        }

        public void AddTrack(Song song)
        {
            if (song == null)
            {
                throw new ArgumentNullException(nameof(song), "Cannot add a null song to the album.");
            }

            AlbumSongs.Add(song);
        }


        //-------------------------------------------------------end of functions to store list of album songs


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
                if (track.GetTrackNumber() == trackNumber) 
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
