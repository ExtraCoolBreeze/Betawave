/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description:  This class was created to store the information of the Artist class*/

//I think this is functionally correct although needs double checking. Print function may need changing 

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

        // Getter method for artistId
        public int GetArtistId()
        {
            return artistId;
        }

        // Setter method for artistId
        public void SetArtistId(int id)
        {
            artistId = id;
        }

        // Getter method for name
        public string GetName()
        {
            return ArtistName;
        }

        // Setter method for name
        public void SetName(string name)
        {
            ArtistName = name;
        }

        public void PrintArtistDetails()
        {
            Console.WriteLine($"Artist ID: {GetArtistId()}, Artist Name: {GetName()}");
        }

    }
}

