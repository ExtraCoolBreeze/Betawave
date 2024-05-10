/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to manage the playlist objects */


using MySql.Data.MySqlClient;
using System.Data;

namespace Betawave.Classes
{
    public class PlaylistManager
    {
        private List<BasePlaylist> playlists = new List<BasePlaylist>();
        private DatabaseAccess dbAccess;
        private BasePlaylist playlist;

        public PlaylistManager(DatabaseAccess dbAccess)
        {
            playlist = new BasePlaylist();
            this.dbAccess = dbAccess;

        }

        public async Task<BasePlaylist> GetDetailsForPlaylist(string songName)
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT s.name AS SongName, a.title AS AlbumTitle, ar.name AS ArtistName, a.image_location AS ImageLocation FROM song s JOIN album_track at ON s.song_id = at.song_id JOIN album a ON at.album_id = a.album_id JOIN artist ar ON a.artist_id = ar.artist_id WHERE s.name = @songName", connection);
                command.Parameters.AddWithValue("@songName", songName);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        playlist.SetAlbumName(reader.GetString("AlbumTitle"));
                        playlist.SetImageLocation(reader.GetString("ImageLocation"));
                        playlist.SetArtistName(reader.GetString("ArtistName"));
                    }
                    else
                    {
                        return null;
                    }

                }

            }
            return playlist;
        }
    }
}
