using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class SongManager
    {
        private List<Song> songs = new List<Song>();
        private DatabaseAccess dbAccess;
        private ArtistManager artistManager;

        public SongManager(DatabaseAccess dbAccess, ArtistManager artistManager)
        {
            this.dbAccess = dbAccess;
            this.artistManager = artistManager;
        }

        public async Task LoadSongsIntoProgramAsync()
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT song_id, name, artist_id, duration, song_location FROM song", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var song = new Song();
                        song.SetSongId(reader.GetInt32("song_id"));
                        song.SetName(reader.GetString("name"));
                        song.SetArtistId(reader.GetInt32("artist_id"));
                        song.SetDuration(reader.GetString("duration"));
                        song.SetSongLocation(reader.GetString("song_location"));

                        // Assuming ArtistManager can resolve an artist by ID synchronously
                        Artist artist = artistManager.GetArtistById(song.GetArtistId());
                        if (artist != null)
                        {
                            song.SetArtist(artist);
                        }

                        songs.Add(song);
                    }
                }
            }
        }

        public void AddSong(Song song)
        {
            songs.Add(song);
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("INSERT INTO song (name, artist_id, duration, song_location) VALUES (@Name, @ArtistId, @Duration, @SongLocation)", connection);
                command.Parameters.AddWithValue("@Name", song.GetName());
                command.Parameters.AddWithValue("@ArtistId", song.GetArtistId());
                command.Parameters.AddWithValue("@Duration", song.GetDuration());
                command.Parameters.AddWithValue("@SongLocation", song.GetSongLocation());
                command.ExecuteNonQuery();
            }
        }

        public void UpdateSong(Song song)
        {
            foreach (var existingSong in songs)
            {
                if (existingSong.GetSongId() == song.GetSongId())
                {
                    existingSong.SetName(song.GetName());
                    existingSong.SetDuration(song.GetDuration());
                    existingSong.SetSongLocation(song.GetSongLocation());
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("UPDATE song SET name = @Name, artist_id = @ArtistId, duration = @Duration, song_location = @SongLocation WHERE song_id = @SongId", connection);
                command.Parameters.AddWithValue("@SongId", song.GetSongId());
                command.Parameters.AddWithValue("@Name", song.GetName());
                command.Parameters.AddWithValue("@ArtistId", song.GetArtistId());
                command.Parameters.AddWithValue("@Duration", song.GetDuration());
                command.Parameters.AddWithValue("@SongLocation", song.GetSongLocation());
                command.ExecuteNonQuery();
            }
        }

        public void DeleteSong(int songId)
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
        }

        public List<Song> GetAllSongs()
        {
            return songs;
        }

        public Song GetSongById(int songId)
        {
            foreach (var song in songs)
            {
                if (song.GetSongId() == songId)
                {
                    return song;
                }
            }
            return null;  // Return null if no song found
        }
    }
}
