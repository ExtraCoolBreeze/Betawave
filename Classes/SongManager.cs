/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to manage, load and write and control song objects and associated information */

using MySql.Data.MySqlClient;
using System.Data;

namespace Betawave.Classes;

//declaring class
public class SongManager
{
    //declaring variables
    private List<Song> songs = new List<Song>();
    private DatabaseAccess dbAccess;

    //class constructor
    public SongManager(DatabaseAccess dbAccess)
    {
        this.dbAccess = dbAccess;
    }

    /// <summary>
    /// This method loads all songs from the database into a list of song objects
    /// It connects to the database, runs a query to get all song information from the song table, it reads them into song objects and stores those objects in a list
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// This function when passed a song object saves that songs name and song_location to the database.
    /// It connects to the database runs a insert into query with song name and song location
    /// </summary>
    /// <param name="song"></param>
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

    /// <summary>
    /// When called and passed an int, it searches the database for all songs associated with that album id.
    /// It connects, runs a select query based on the album id and sets the results to a song object and the saves those songs to a list of song objects
    /// </summary>
    /// <param name="albumId"></param>
    /// <returns></returns>
    public async Task<List<Song>> GetSongsForAlbum(int albumId)
    {
        List<Song> albumSongs = new List<Song>();
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT s.song_id, s.name, s.song_location FROM song s JOIN album_track at ON s.song_id = at.song_id WHERE at.album_id = @AlbumId", connection);

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
