/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to manage album objects */

using MySql.Data.MySqlClient;
using System.Data;

namespace Betawave.Classes
{
    //declaring the class
    public class AlbumManager
    {
        //declaring objects
        private List<Album> albums = new List<Album>();
        private DatabaseAccess dbAccess;
        private ErrorLogger errorLogger;

        //class constructor passing in Database access class
        public AlbumManager(DatabaseAccess dbAccess)
        {
            this.dbAccess = dbAccess;
            errorLogger = new ErrorLogger("C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\BetawaveErrorLog.txt");
        }

        /// <summary>
        /// When called this method connects to the database and searches for the all the details from the first 3 database entries
        /// it then adds that album data to an album object and finally adds that album to a list of album objects
        /// </summary>
        /// <returns></returns>
        public async Task LoadAlbums()
        {
            try
            {
                //connecting to database
                using (var connection = dbAccess.ConnectToMySql())
                { //running command query and reading in results
                    MySqlCommand command = new MySqlCommand("SELECT album_id, title, image_location, artist_id FROM album ORDER BY album_id ASC LIMIT 3", connection);
                    using (var reader = await command.ExecuteReaderAsync())
                    {   //clearing all previous albums before writing new content
                        albums.Clear();
                        while (await reader.ReadAsync())
                        {   //writing all data to album variable
                            Album album = new Album();
                            album.SetAlbumId(reader.GetInt32("album_id"));
                            album.SetAlbumTitle(reader.GetString("title"));
                            album.SetImageLocation(reader.GetString("image_location"));
                            album.SetArtistId(reader.GetInt32("artist_id"));
                            //adding to list of albums
                            albums.Add(album);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
            }
                
        }

        /// <summary>
        /// When called and passed an album object, this function checks if there is already 3 or more albums in the database, if there is then it stops and returns false
        /// if there is less than 3, the passed albums properites are added to the database and then a true value is returned
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public async Task<bool> AddAlbum(Album album)
        {
            try
            {
                //counting all albums in list before adding
                if (await CountAlbums() >= 3)
                {   //if too many albums exist to add new album return false. This is to artificially limit the program due to time
                    return false;
                }
                //adds to list then connects to database
                albums.Add(album);
                using (var connection = dbAccess.ConnectToMySql())
                {   //writes query to add data to database
                    var command = new MySqlCommand("INSERT INTO album (title, image_location, artist_id) VALUES (@Title, @ImageLocation, @ArtistId)", connection);
                    command.Parameters.AddWithValue("@Title", album.GetAlbumTitle());
                    command.Parameters.AddWithValue("@ImageLocation", album.GetImageLocation());
                    command.Parameters.AddWithValue("@ArtistId", album.GetArtistId());
                    await command.ExecuteNonQueryAsync();
                }   //returns true if write achieved
                return true;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                throw;
            }
        }


        /// <summary>
        /// When this function is called it counts the number of album entries in the database and return the amount
        /// It connects to the database and counts the entries and then returns a number
        /// /// </summary>
        /// <returns></returns>
        public async Task<int> CountAlbums()
        {
            try
            {
                //connects to database runs command to count albums and returns the count
                using (var connection = dbAccess.ConnectToMySql())
                {
                    var command = new MySqlCommand("SELECT COUNT(*) FROM album", connection);
                    int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return count;
                }
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// //When called and pass a song name, this function searches the database for the associated album image
        /// if it songs the image it returns it, otherwise it returns as a default
        /// </summary>
        /// <param name="songName"></param>
        /// <returns></returns>
        public async Task<string> GetAlbumImageBySongName(string songName)
        {
            try
            {   //connecting to database and querying to get image information based on song name
                using (var connection = dbAccess.ConnectToMySql())
                {
                    var command = new MySqlCommand("SELECT a.image_location FROM album a JOIN album_track at ON a.album_id = at.album_id JOIN song s ON at.song_id = s.song_id WHERE s.name = @SongName", connection);
                    command.Parameters.AddWithValue("@SongName", songName);
                    string albumImage = Convert.ToString(await command.ExecuteScalarAsync());
                    //checks if the album image was correctly received
                    if (!string.IsNullOrEmpty(albumImage))
                    {   //returns value
                        return albumImage;
                    }
                    else
                    {   //returns default so picture always shows
                        return "default_album_cover.png";
                    }
                }
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                throw;
            }
        }


        /// <summary>
        /// When called this functions the list of album object belonging to the class
        /// </summary>
        /// <returns></returns>
        public List<Album> GetAllAlbums()
        {
            return albums;
        }

    }
}
