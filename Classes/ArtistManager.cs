/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to manage the artist objects
*/

using MySql.Data.MySqlClient;
using System.Data;

namespace Betawave.Classes
{
    public class ArtistManager
    {
        private List<Artist> artists = new List<Artist>();
        private DatabaseAccess dbAccess;

        public ArtistManager(DatabaseAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        public async Task LoadArtistsAsync()
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


        public void AddArtist(Artist artist)
        {
            artists.Add(artist);
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("INSERT INTO artist (name) VALUES (@Name)", connection);
                command.Parameters.AddWithValue("@Name", artist.GetName());
                command.ExecuteNonQuery();
            }
        }

        public void UpdateArtist(Artist artist)
        {
            for (int i = 0; i < artists.Count; i++)
            {
                if (artists[i].GetArtistId() == artist.GetArtistId())
                {
                    artists[i].SetName(artist.GetName());
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("UPDATE artist SET name = @Name WHERE artist_id = @ArtistId", connection);
                command.Parameters.AddWithValue("@ArtistId", artist.GetArtistId());
                command.Parameters.AddWithValue("@Name", artist.GetName());
                command.ExecuteNonQuery();
            }
        }

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

        public List<Artist> GetAllArtists()
        {
            return artists;
        }

        public Artist GetArtistById(int artistId)
        {
            for (int i = 0; i < artists.Count; i++)
            {
                if (artists[i].GetArtistId() == artistId)
                {
                    return artists[i];
                }
            }
            return null;  // Return null if no artist is found
        }

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
