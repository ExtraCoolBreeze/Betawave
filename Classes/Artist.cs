using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//I think this is functionally correct although needs double checking. Print functrion may need changing 

namespace Betawave.Classes
{
    public class Artist
    {
        private int pkartistId;
        private string _name;
        public List<Featured_Artists> FeaturedSongs { get; set; } = new List<Featured_Artists>();

        // Constructor that takes 'name' as a parameter
        public Artist()
        {
            pkartistId = 0; // This ID should ideally be set uniquely, possibly managed by the database or a higher-level application logic
            _name = "";
        }

        // Getter method for artistId
        public int GetArtistId()
        {
            return pkartistId;
        }

        // Setter method for artistId
        public void SetArtistId(int id)
        {
            pkartistId = id;
        }

        // Getter method for name
        public string GetName()
        {
            return _name;
        }

        // Setter method for name
        public void SetName(string name)
        {
            _name = name;
        }

        public void PrintArtistDetails()
        {
            Console.WriteLine($"Artist ID: {GetArtistId()}");
            Console.WriteLine($"Artist Name: {GetName()}");
            // If you want to include featured songs:
            Console.WriteLine("Featured Songs:");
            foreach (var featured in FeaturedSongs)
            {
                Console.WriteLine($"- Song ID: {featured.GetSongId()} on Track: {featured.GetTrackNumber()}");
            }
        }

    }
}

