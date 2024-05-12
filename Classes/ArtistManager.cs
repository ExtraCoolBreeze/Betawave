/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to manage a list artist objects
*/

using MySql.Data.MySqlClient;
using System.Data;

namespace Betawave.Classes
{
    //declaring class
    public class ArtistManager
    {
        //declaring class objects
        private List<Artist> artists = new List<Artist>();
        private DatabaseAccess dbAccess;

        //class constructor
        public ArtistManager(DatabaseAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        /// <summary>
        /// This method loads in all artists from the database. 
        /// It connects, the searches for the artist id and name and then creates and sets an artist object and finally adds that artist object to a list of artist objects.
        /// </summary>
        /// <returns></returns>
        public async Task LoadArtists()
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT artist_id, name FROM artist", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var artist = new Artist();
                        artist.SetArtistId(reader.GetInt32("artist_id"));
                        artist.SetName(reader.GetString("name"));
                        artists.Add(artist);
                    }
                }
            }
        }

        /// <summary>
        /// This method when passed an artist object, connects and adds that artist name to the database
        /// </summary>
        /// <param name="artist"></param>
        public void AddArtist(Artist artist)
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("INSERT INTO artist (name) VALUES (@Name)", connection);
                command.Parameters.AddWithValue("@Name", artist.GetName());
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// When passed an int, it searches the artist list for the artist id and removes that artist from the list. It then searches for the artist in the database based on the artist id and deletes it.
        /// </summary>
        /// <param name="artistId"></param>
        public void DeleteArtist(int artistId)
        {
            for (int i = artists.Count - 1; i >= 0; i--)
            {
                if (artists[i].GetArtistId() == artistId)
                {
                    artists.RemoveAt(i);
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("DELETE FROM artist WHERE artist_id = @ArtistId", connection);
                command.Parameters.AddWithValue("@ArtistId", artistId);
                command.ExecuteNonQuery();
            }
        }

        //May need to write a function that searches the database for artists, counts them and then if there is already 3 artists in the database then return and true or false

        //Might be better to use this in the view model but unsure yet

        /// <summary>
        /// This function returns the list of artist objects
        /// </summary>
        /// <returns></returns>
        public List<Artist> GetAllArtists()
        {
            return artists;
        }


        //Might be a good idea to change this so if the id isn't found then search the database for it and return that.
        /// <summary>
        /// When passed an int, this function searches the list of artists for the artist object with the matching artistId and returns it, if not returns null
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        public Artist GetArtistById(int artistId)
        {
            foreach (var artist in artists)
            {
                if (artist.GetArtistId() == artistId)
                {
                    return artist;
                }
            }

            return null; // Return null if no match is found
        }


        /// <summary>
        /// When passed an string, this function searches the database for an artist and returns if not returns null.
        /// It connects, searches for the artist name and artist id, saves that information to an artist object and returns the artist object, if not returns null.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Artist> GetArtistByName(string name)
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT artist_id, name FROM artist WHERE name = @Name", connection);
                command.Parameters.AddWithValue("@Name", name);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var artist = new Artist();
                        artist.SetArtistId(reader.GetInt32("artist_id"));
                        artist.SetName(reader.GetString("name"));
                        return artist;
                    }
                }
            }
            return null;
        }
    }
}
