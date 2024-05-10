/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This Album class us used to store Album details */


namespace Betawave.Classes
{
    //declaring class
    public class Album
    {
        //declaring class
        private int albumId;
        private string albumTitle;
        private string imageLocation;
        private int artistId;

        //class constructor
        public Album()
        {
            //initialising variables
            albumId = 0;
            albumTitle = "";
            imageLocation = "";
            artistId = 0;
        }

        /// <summary>
        /// This function returns the albumid
        /// </summary>
        /// <returns></returns>
        public int GetAlbumId()
        {
            return albumId;
        }

        /// <summary>
        /// When this method is called and passed an int, it sets that int to the album id variable
        /// </summary>
        /// <param name="albumId"></param>
        public void SetAlbumId(int albumId)
        {
            this.albumId = albumId;
        }

        /// <summary>
        /// This function returns the album title
        /// </summary>
        /// <returns></returns>
        public string GetAlbumTitle()
        {
            return albumTitle;
        }

        /// <summary>
        /// When called and passed a string, it sets that int to the album title variable
        /// </summary>
        /// <param name="title"></param>
        public void SetAlbumTitle(string title)
        {
            albumTitle = title;
        }

        /// <summary>
        /// When this method is called and passed a string, it sets that string to the image location variable 
        /// </summary>
        /// <param name="imagelocation"></param>
        public void SetImageLocation(string imagelocation)
        {
            imageLocation = imagelocation;
        }

        /// <summary>
        /// When this function is called it returns the image location
        /// </summary>
        /// <returns></returns>
        public string GetImageLocation()
        {
            return imageLocation;
        }

        /// <summary>
        ///  When this method is called and passed an int, it saves that int to the artist id
        /// </summary>
        /// <param name="value"></param>
        public void SetArtistId(int value)
        {
            artistId = value;
        }

        /// <summary>
        /// When this function is called it returns the artist id
        /// </summary>
        /// <returns></returns>
        public int GetArtistId()
        {
            return artistId;
        }
    }
}
