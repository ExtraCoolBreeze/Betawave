/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description:  This class was created to store the information of the Artist class*/


namespace Betawave.Classes
{
    public class Artist
    {
        private int artistId;
        private string ArtistName;

        public Artist()
        {
            artistId = 0;
            ArtistName = "";
        }

        /// <summary>
        /// Returns artistId
        /// </summary>
        /// <returns></returns>
        public int GetArtistId()
        {
            return artistId;
        }

        /// <summary>
        /// When passed an int, it sets that to the artistId
        /// </summary>
        /// <param name="id"></param>
        public void SetArtistId(int id)
        {
            artistId = id;
        }

        /// <summary>
        /// Returns the artistName
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return ArtistName;
        }

        /// <summary>
        /// When passed a string name, it sets that to the ArtistName
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            ArtistName = name;
        }

    }
}

