﻿/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to manage and control song objects and associated information */

using MySql.Data.MySqlClient;
using System.Data;

namespace Betawave.Classes
{
    public class SongManager
    {
        private List<Song> songs = new List<Song>();
        private DatabaseAccess dbAccess;

        public SongManager(DatabaseAccess dbAccess) 
        {
            this.dbAccess = dbAccess;
        }

        public async Task LoadSongsIntoProgram()
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT song_id, name, song_location FROM song", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var song = new Song();
                        song.SetSongId(reader.GetInt32("song_id"));
                        song.SetSongName(reader.GetString("name"));
                        song.SetSongLocation(reader.GetString("song_location"));

                        songs.Add(song);
                    }
                }
            }
        }

        public void AddSongToDatabase(Song song)
        {
            songs.Add(song);
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("INSERT INTO song (name, song_location) VALUES (@Name, @SongLocation)", connection);
                command.Parameters.AddWithValue("@Name", song.GetSongName());
                command.Parameters.AddWithValue("@SongLocation", song.GetSongLocation());
                command.ExecuteNonQuery();
            }
        }

/*        public void UpdateSong(Song song)
        {
            foreach (var existingSong in songs)
            {
                if (existingSong.GetSongId() == song.GetSongId())
                {
                    existingSong.SetName(song.GetName());
                    existingSong.SetSongLocation(song.GetSongLocation());
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("UPDATE song SET name = @Name, song_location = @SongLocation WHERE song_id = @SongId", connection);
                command.Parameters.AddWithValue("@SongId", song.GetSongId());
                command.Parameters.AddWithValue("@Name", song.GetName());
                command.Parameters.AddWithValue("@SongLocation", song.GetSongLocation());
                command.ExecuteNonQuery();
            }
        }*/

/*        public void DeleteSong(int songId)
        {
            for (int i = 0; i < songs.Count; i++)
            {
                if (songs[i].GetSongId() == songId)
                {
                    songs.RemoveAt(i);
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("DELETE FROM song WHERE song_id = @SongId", connection);
                command.Parameters.AddWithValue("@SongId", songId);
                command.ExecuteNonQuery();
            }
        }*/

/*        public List<Song> GetAllSongs()
        {
            return songs;
        }
*/
/*        public Song GetSongById(int songId)
        {
            foreach (var song in songs)
            {
                if (song.GetSongId() == songId)
                {
                    return song;
                }
            }
            return null;
        }*/


        public async Task<List<Song>> GetSongsForAlbum(int albumId)
        {
            List<Song> albumSongs = new List<Song>();
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand(
                    "SELECT s.song_id, s.name, s.song_location, a.artist_id, ar.name as artist_name " +
                    "FROM song s " +
                    "JOIN album_track at ON s.song_id = at.song_id " +
                    "JOIN album a ON at.album_id = a.album_id " +
                    "JOIN artist ar ON a.artist_id = ar.artist_id " +
                    "WHERE at.album_id = @AlbumId", connection);

                command.Parameters.AddWithValue("@AlbumId", albumId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var song = new Song();
                        song.SetSongId(reader.GetInt32("song_id"));
                        song.SetSongName(reader.GetString("name"));
                        song.SetSongLocation(reader.GetString("song_location"));

                        albumSongs.Add(song);
                    }
                }
            }
            return albumSongs;
        }
    }
}
