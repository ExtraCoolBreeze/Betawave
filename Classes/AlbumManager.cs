﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class AlbumManager
    {
        private List<Album> albums = new List<Album>();
        private  DatabaseAccess dbAccess;
        private  ArtistManager artistManager;

        public AlbumManager(DatabaseAccess dbAccess, ArtistManager artistManager)
        {
            this.dbAccess = dbAccess;
            this.artistManager = artistManager;
        }

        public async Task LoadAlbumsAsync()
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT album_id, title, image_location, artist_id FROM album", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var album = new Album(artistManager);
                        album.SetAlbumId(reader.GetInt32("album_id"));
                        album.SetAlbumTitle(reader.GetString("title"));
                        album.SetImageLocation(reader.GetString("image_location"));
                        album.SetArtistId(reader.GetInt32("artist_id"));

                        // Set the artist using the method
                        Artist artist = artistManager.GetArtistById(album.GetArtistId());
                        if (artist != null)
                        {
                            album.SetArtist(artist);
                        }

                        albums.Add(album);
                    }
                }
            }
        }


        public void AddAlbum(Album album)
        {
            albums.Add(album);
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("INSERT INTO album (title, image_location, artist_id) VALUES (@Title, @ImageLocation, @ArtistId)", connection);
                command.Parameters.AddWithValue("@Title", album.GetAlbumTitle());
                command.Parameters.AddWithValue("@ImageLocation", album.GetImageLocation());
                command.Parameters.AddWithValue("@ArtistId", album.GetArtistId());
                command.ExecuteNonQuery();
            }
        }

        public void UpdateAlbum(Album album)
        {
            for (int i = 0; i < albums.Count; i++)
            {
                if (albums[i].GetAlbumId() == album.GetAlbumId())
                {
                    albums[i].SetAlbumTitle(album.GetAlbumTitle());
                    albums[i].SetImageLocation(album.GetImageLocation());
                    albums[i].SetArtistId(album.GetArtistId());
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("UPDATE album SET title = @Title, image_location = @ImageLocation, artist_id = @ArtistId WHERE album_id = @AlbumId", connection);
                command.Parameters.AddWithValue("@AlbumId", album.GetAlbumId());
                command.Parameters.AddWithValue("@Title", album.GetAlbumTitle());
                command.Parameters.AddWithValue("@ImageLocation", album.GetImageLocation());
                command.Parameters.AddWithValue("@ArtistId", album.GetArtistId());
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

        public List<Album> GetAllAlbums()
        {
            return albums;
        }

        public Album GetAlbumById(int albumId)
        {
            for (int i = 0; i < albums.Count; i++)
            {
                if (albums[i].GetAlbumId() == albumId)
                {
                    return albums[i];
                }
            }
            return null; // Return null if no album is found
        }
    }
}
