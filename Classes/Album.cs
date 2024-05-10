/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This Album class us used to store Album details */


namespace Betawave.Classes
{

    //Changed the inheritance from PlaylistControl to BasePlaylist
    public class Album
    {
        private int albumId;
        private string albumTitle;
        private string imageLocation;
        private int artistId;


        public Album()
        {
            albumId = 0;
            albumTitle = "";
            imageLocation = "";
            artistId = 0;
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

        public void SetArtistId(int value)
        {
            artistId = value;
        }

        public int GetArtistId()
        {
            return artistId;
        }

        public void PrintAlbumDetails()
        {
            Console.WriteLine("Album ID: " + GetAlbumId());
            Console.WriteLine("Title: " + GetAlbumTitle());
            Console.WriteLine("Artist ID: " + GetArtistId());
        }
    }
}
