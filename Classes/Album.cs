using System;
using System.Collections.Generic;

using System;

namespace Betawave.Classes
{
    public class Album : PlaylistControl
    {
        private int albumId;
        private string albumTitle;
        private string imageLocation;
        private int artistId;
        private Artist artist;
        private ArtistManager artistManager;  // Reference to the ArtistManager

        public Album(ArtistManager artistManager)
        {
            albumId = 0;
            albumTitle = "";
            imageLocation = "";
            artistId = 0;
            this.artistManager = artistManager;
        }

        public int GetAlbumId()
        {
            return albumId;
        }

        public void SetAlbumId(int albumId)
        {
            this.albumId = albumId;
        }

        public string GetAlbumTitle()
        {
            return albumTitle;
        }

        public void SetAlbumTitle(string title)
        {
            albumTitle = title;
        }

        public void SetImageLocation(string imagelocation)
        {
            imageLocation = imagelocation;
        }

        public string GetImageLocation()
        {
            return imageLocation;
        }

        public void SetArtistId(int id)
        {
            artistId = id;
            artist = null;  // Invalidate the cached artist when ID changes
        }

        public int GetArtistId()
        {
            return artistId;
        }

        public void SetArtist(Artist artist)
        {
            this.artist = artist;
        }


        public Artist GetArtist()
        {
            if (artist == null && artistManager != null)
            {
                artist = artistManager.GetArtistById(artistId);
            }
            return artist;
        }

        public void PrintAlbumDetails()
        {
            Console.WriteLine("Album ID: " + GetAlbumId());
            Console.WriteLine("Title: " + GetAlbumTitle());
            Console.WriteLine("Artist ID: " + GetArtistId());
            Console.WriteLine("Artist Name: " + GetArtist()?.GetName());
        }
    }
}
