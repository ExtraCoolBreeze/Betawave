using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class PlaylistManager
    {
        private List<BasePlaylist> playlists = new List<BasePlaylist>();
        private DatabaseAccess dbAccess;
        private ArtistManager artistManager;

        public PlaylistManager(DatabaseAccess dbAccess, ArtistManager artistManager)
        {
            this.dbAccess = dbAccess;
        }

        public async Task LoadPlaylistsAsync()
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT playlist_id, title, queue, favourite, account_id FROM playlist", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var playlist = new BasePlaylist();
                        playlist.SetPlaylistId(reader.GetInt32("playlist_id"));
                        playlist.SetTitle(reader.GetString("title"));
                        playlist.SetQueue(reader.GetString("queue"));
                        playlist.SetFavourite(reader.GetString("favourite"));
                        playlist.SetAccountId(reader.GetInt32("account_id"));

                        // Assuming that songs need to be loaded separately
                        LoadSongsForPlaylist(playlist);

                        playlists.Add(playlist);
                    }
                }
            }
        }

        private void LoadSongsForPlaylist(BasePlaylist playlist)
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT s.song_id, s.name, s.song_location FROM playlist_track pt INNER JOIN song s ON pt.song_id = s.song_id WHERE pt.playlist_id = @PlaylistId", connection);
                command.Parameters.AddWithValue("@PlaylistId", playlist.GetPlaylistId());

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var song = new Song();
                        song.SetSongId(reader.GetInt32("song_id"));
                        song.SetName(reader.GetString("name"));
                        song.SetSongLocation(reader.GetString("song_location"));

                        // Assuming there is a method to get the artist ID from the song data
                        int artistId = reader.GetInt32("artist_id"); // Make sure you have artist_id in your song data
                        Artist artist = artistManager.GetArtistById(artistId);
                        if (artist != null)
                        {
                            song.SetArtist(artist);
                        }

                        playlist.AddSong(song);
                    }
                }
            }
        }




        public void AddPlaylist(BasePlaylist playlist)
        {
            playlists.Add(playlist);
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("INSERT INTO playlist (title, queue, favourite, account_id) VALUES (@Title, @Queue, @Favourite, @AccountId)", connection);
                command.Parameters.AddWithValue("@Title", playlist.GetTitle());
                command.Parameters.AddWithValue("@Queue", playlist.GetQueue());
                command.Parameters.AddWithValue("@Favourite", playlist.GetFavourite());
                command.Parameters.AddWithValue("@AccountId", playlist.GetAccountId());
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePlaylist(BasePlaylist playlist)
        {
            for (int i = 0; i < playlists.Count; i++)
            {
                if (playlists[i].GetPlaylistId() == playlist.GetPlaylistId())
                {
                    playlists[i].SetTitle(playlist.GetTitle());
                    playlists[i].SetQueue(playlist.GetQueue());
                    playlists[i].SetFavourite(playlist.GetFavourite());
                    playlists[i].SetAccountId(playlist.GetAccountId());
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("UPDATE playlist SET title = @Title, queue = @Queue, favourite = @Favourite, account_id = @AccountId WHERE playlist_id = @PlaylistId", connection);
                command.Parameters.AddWithValue("@PlaylistId", playlist.GetPlaylistId());
                command.Parameters.AddWithValue("@Title", playlist.GetTitle());
                command.Parameters.AddWithValue("@Queue", playlist.GetQueue());
                command.Parameters.AddWithValue("@Favourite", playlist.GetFavourite());
                command.Parameters.AddWithValue("@AccountId", playlist.GetAccountId());
                command.ExecuteNonQuery();
            }
        }

        public void DeletePlaylist(int playlistId)
        {
            for (int i = playlists.Count - 1; i >= 0; i--)
            {
                if (playlists[i].GetPlaylistId() == playlistId)
                {
                    playlists.RemoveAt(i);
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("DELETE FROM playlist WHERE playlist_id = @PlaylistId", connection);
                command.Parameters.AddWithValue("@PlaylistId", playlistId);
                command.ExecuteNonQuery();
            }
        }

        public List<BasePlaylist> GetAllPlaylists()
        {
            return playlists;
        }

        public BasePlaylist GetPlaylistById(int playlistId)
        {
            for (int i = 0; i < playlists.Count; i++)
            {
                if (playlists[i].GetPlaylistId() == playlistId)
                {
                    return playlists[i];
                }
            }
            return null;
        }
    }
}
