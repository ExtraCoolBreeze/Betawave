/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This Album class us used to store Album details */


namespace Betawave.Classes
{

    //Changed the inheritance from PlaylistControl to BasePlaylist
    public class Album : BasePlaylist
    {
        private int albumId;
        private string albumTitle;
        private string imageLocation;
        private int artistId;
        public Artist Artist;
        private ArtistManager artistManager
         { get; set; }

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

        public void SetArtistId(int value)
        {
            artistId = value;
        }

        public int GetArtistId()
        {
            return artistId;
        }

        public void SetArtist(Artist artist)
        {
            this.Artist = artist;
            this.artistId = artist.GetArtistId();
        }

        public Artist GetArtist()
        {
            if (this.Artist == null && this.artistId > 0)
            {
                Artist fetchedArtist = artistManager.GetArtistById(this.artistId);
                if (fetchedArtist != null)
                {
                    this.Artist = fetchedArtist;
                }
                else
                {
                     string ArtistError = "Cannot load artist";
                }
            }
            return this.Artist;
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
