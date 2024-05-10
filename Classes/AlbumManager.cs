/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to manage album objects */

using MySql.Data.MySqlClient;
using System.Data;

namespace Betawave.Classes
{
    public class AlbumManager
    {
        private List<Album> albums = new List<Album>();
        private DatabaseAccess dbAccess;

        public AlbumManager(DatabaseAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        public async Task LoadAlbums()
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT album_id, title, image_location, artist_id FROM album ORDER BY album_id ASC LIMIT 3", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    albums.Clear();
                    while (await reader.ReadAsync())
                    {
                        var album = new Album();
                        album.SetAlbumId(reader.GetInt32("album_id"));
                        album.SetAlbumTitle(reader.GetString("title"));
                        album.SetImageLocation(reader.GetString("image_location"));
                        album.SetArtistId(reader.GetInt32("artist_id"));

                        albums.Add(album);
                    }
                }
            }
        }

        
        public async Task<bool> AddAlbum(Album album)
        {
            if (await CountAlbums() >= 3)
            {
                return false;
            }

            albums.Add(album);
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("INSERT INTO album (title, image_location) VALUES (@Title, @ImageLocation)", connection);
                command.Parameters.AddWithValue("@Title", album.GetAlbumTitle());
                command.Parameters.AddWithValue("@ImageLocation", album.GetImageLocation());
                await command.ExecuteNonQueryAsync();
            }
            return true;
        }

        public void UpdateAlbum(Album album)
        {
            for (int i = 0; i < albums.Count; i++)
            {
                if (albums[i].GetAlbumId() == album.GetAlbumId())
                {
                    albums[i].SetAlbumTitle(album.GetAlbumTitle());
                    albums[i].SetImageLocation(album.GetImageLocation());
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("UPDATE album SET title = @Title, image_location = @ImageLocation WHERE album_id = @AlbumId", connection);
                command.Parameters.AddWithValue("@AlbumId", album.GetAlbumId());
                command.Parameters.AddWithValue("@Title", album.GetAlbumTitle());
                command.Parameters.AddWithValue("@ImageLocation", album.GetImageLocation());
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAlbum(int albumId)
        {
            for (int i = albums.Count - 1; i >= 0; i--)
            {
                if (albums[i].GetAlbumId() == albumId)
                {
                    albums.RemoveAt(i);
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("DELETE FROM album WHERE album_id = @AlbumId", connection);
                command.Parameters.AddWithValue("@AlbumId", albumId);
                command.ExecuteNonQuery();
            }
        }

        public async Task<int> CountAlbums()
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT COUNT(*) FROM album", connection);
                int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                return count;
            }
        }

        public List<Album> GetAllAlbums()
        {
            return albums;
        }

        public Album GetAlbumById(int albumId)
        {
            foreach (var album in albums)
            {
                if (album.GetAlbumId() == albumId)
                {
                    return album;
                }
            }
            return null; // Return null if no album is found
        }

    }
}
