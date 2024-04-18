using MySql.Data.MySqlClient;
using Betawave.Classes;
using System.Data.SqlClient;

public class DatabaseManager
{
    private DatabaseAccess dbAccess;
    public Account CurrentUser { get; private set; }

    public int CurrentUserRole { get; private set; }

    private Dictionary<int, Album> albums = new Dictionary<int, Album>();
    private Dictionary<int, Song> songs = new Dictionary<int, Song>();
    private Dictionary<int, Artist> artists = new Dictionary<int, Artist>();
    private Dictionary<int, BasePlaylist> playlists = new Dictionary<int, BasePlaylist>();


    public DatabaseManager(DatabaseAccess access)
    {
        dbAccess = access;
        CurrentUser = new Account();
    }

    //--------------------------------------------- Below are methods & functions for reading information into the program from the database---------------------------------

    public void LoadAllData()
    {
        LoadSongs();
        LoadArtists();
        LoadAlbums();
        LoadPlaylists();
    }
    public void LoadSongs()
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT song_id, name, duration, song_location FROM song", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    var song = new Song();
                    song.SetSongId(reader.GetInt32("song_id"));
                    song.SetName(reader.GetString("name"));
                    song.SetDuration(reader.GetString("duration"));
                    song.SetSongLocation(reader.GetString("song_location"));
                    
                    songs[song.GetSongId()] = song;
                }
            }
        } // Connection is automatically closed here due to the using statement
    }

    public void LoadArtists()
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT artist_id, name FROM artist", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var artist = new Artist();
                    artist.SetArtistId(reader.GetInt32("artist_id"));
                    artist.SetName(reader.GetString("name"));
                    
                    artists[artist.GetArtistId()] = artist;
                }
            }
        }
    }

    public void LoadAlbums()
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT album_id, title, image_location FROM album", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var album = new Album();
                    album.SetAlbumId(reader.GetInt32("album_id"));
                    album.SetAlbumTitle(reader.GetString("title"));
                    album.SetImageLocation(reader.GetString("image_location"));
                    var Track = new List<Song>(); // Initialize an empty list of songs; load actual songs separately if needed
                    
                    albums[album.GetAlbumId()] = album;
                }
            }
        }
    }

    public void LoadPlaylists()
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT playlist_id, title FROM playlist", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var playlist = new BasePlaylist();
                    playlist.SetPlaylistId(reader.GetInt32("playlist_id"));
                    playlist.SetTitle(reader.GetString("title"));
                    var Songs = new List<Song>(); // Assuming you have a List<Song> in Playlist
                    
                    playlists[playlist.GetPlaylistId()] = playlist;
                }
            }
        }
    }


    public async Task<bool> ReadInUserAccountTable(string username, string password)
    {
        if (await dbAccess.ValidateUser(username, password)) // Use await here as well
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT * FROM account WHERE username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        CurrentUser = new Account();
                        string usernameError, passwordError;
                        CurrentUser.SetAccountId(reader.GetInt32("account_id"));
                        CurrentUser.SetUsername(reader.GetString("username"), out usernameError);
                        CurrentUser.SetPassword(reader.GetString("userpassword"), out passwordError);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public List<Account_Role> ReadRolesForAccount(int accountId)
    {
        var roles = new List<Account_Role>();
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT fkaccount_id, fkrole_id FROM account_role WHERE fkaccount_id = @AccountId", connection);
            command.Parameters.AddWithValue("@AccountId", accountId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var accountRole = new Account_Role();
                    accountRole.SetAccountRole(reader.GetInt32("fkaccount_id"));
                    accountRole.SetRoleId(reader.GetInt32("fkrole_id"));
                    roles.Add(accountRole);
                }
            }
        }
        return roles;
    }

    public async Task<bool> LoadCurrentUser(string username, string password)
    {
        if (await dbAccess.ValidateUser(username, password)) // Use await to handle Task<bool>
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT account_id FROM account WHERE username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int accountId = reader.GetInt32("account_id");
                        CurrentUser = new Account();
                        string usernameError, passwordError;
                        CurrentUser.SetAccountId(accountId);
                        CurrentUser.SetUsername(username, out usernameError);
                        CurrentUser.SetPassword(password, out passwordError);  // Assuming password is pre-hashed

                        // Load the role using the fetched account ID
                        CurrentUserRole = GetRoleForAccount(accountId);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Method to retrieve the role ID for a given account ID within DatabaseManager
    public int GetRoleForAccount(int accountId)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT fkrole_id FROM account_role WHERE fkaccount_id = @AccountId", connection);
            command.Parameters.AddWithValue("@AccountId", accountId);
            var result = command.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                throw new InvalidOperationException("No role found for the account.");
            }
        }
    }

    public void CreateAlbum(Album album)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("INSERT INTO album (title, image_location) VALUES (@Title, @ImageLocation)", connection);
            command.Parameters.AddWithValue("@Title", album.GetAlbumTitle());
            command.Parameters.AddWithValue("@ImageLocation", album.GetImageLocation());
            command.ExecuteNonQuery();
            int albumId = (int)command.LastInsertedId;  // Get the ID of the newly inserted album

            album.SetAlbumId(albumId);  // Update the album object with its new ID

            // Insert tracks associated with the album
            foreach (var track in album.GetTracks())
            {
                var trackCommand = new MySqlCommand("INSERT INTO album_track (album_id, track_number, song_id) VALUES (@AlbumId, @TrackNumber, @SongId)", connection);
                trackCommand.Parameters.AddWithValue("@AlbumId", albumId);
                trackCommand.Parameters.AddWithValue("@TrackNumber", track.GetTrackNumber());
                trackCommand.Parameters.AddWithValue("@SongId", track.GetSongId());
                trackCommand.ExecuteNonQuery();
            }
        }
    }

    public Album GetAlbumById(int albumId)
    {
        Album album = new Album();
        using (var connection = dbAccess.ConnectToMySql())
        {
            // Fetch the album data
            var command = new MySqlCommand("SELECT title, image_location FROM album WHERE album_id = @AlbumId", connection);
            command.Parameters.AddWithValue("@AlbumId", albumId);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    album.SetAlbumId(albumId);
                    album.SetAlbumTitle(reader.GetString("title"));
                    album.SetImageLocation(reader.GetString("image_location"));
                }
            }

            // Fetch the tracks for this album
            command = new MySqlCommand("SELECT track_number, song_id FROM album_track WHERE album_id = @AlbumId ORDER BY track_number", connection);
            command.Parameters.AddWithValue("@AlbumId", albumId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var track = new Album_Track();
                    track.SetAlbumId(albumId);
                    track.SetTrackNumber(reader.GetInt32("track_number"));
                    track.SetSongId(reader.GetInt32("song_id"));
                    album.AddTrack(track);
                }
            }
        }
        return album;
    }

    public void UpdateAlbum(Album album)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("UPDATE album SET title = @Title, image_location = @ImageLocation WHERE album_id = @AlbumId", connection);
            command.Parameters.AddWithValue("@Title", album.GetAlbumTitle());
            command.Parameters.AddWithValue("@ImageLocation", album.GetImageLocation());
            command.Parameters.AddWithValue("@AlbumId", album.GetAlbumId());
            command.ExecuteNonQuery();
        }
    }

    public void DeleteAlbum(int albumId)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            // Delete the tracks first to maintain foreign key constraints
            var trackCommand = new MySqlCommand("DELETE FROM album_track WHERE album_id = @AlbumId", connection);
            trackCommand.Parameters.AddWithValue("@AlbumId", albumId);
            trackCommand.ExecuteNonQuery();

            // Now delete the album
            var command = new MySqlCommand("DELETE FROM album WHERE album_id = @AlbumId", connection);
            command.Parameters.AddWithValue("@AlbumId", albumId);
            command.ExecuteNonQuery();
        }
    }

    public void AddTrackToAlbum(int albumId, int songId, int trackNumber)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("INSERT INTO album_track (album_id, track_number, song_id) VALUES (@AlbumId, @TrackNumber, @SongId)", connection);
            command.Parameters.AddWithValue("@AlbumId", albumId);
            command.Parameters.AddWithValue("@TrackNumber", trackNumber);
            command.Parameters.AddWithValue("@SongId", songId);
            command.ExecuteNonQuery();
        }
    }

    public List<Album_Track> GetTracksForAlbum(int albumId)
    {
        List<Album_Track> tracks = new List<Album_Track>();
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT track_number, song_id FROM album_track WHERE album_id = @AlbumId ORDER BY track_number", connection);
            command.Parameters.AddWithValue("@AlbumId", albumId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int trackNumber = reader.GetInt32("track_number");
                    int songId = reader.GetInt32("song_id");

                    // Assuming you have a method to fetch a Song by ID
                    Song song = GetSongById(songId);  // This method needs to be implemented or already exist
                    var albumTrack = new Album_Track();
                    albumTrack.SetAlbumId(albumId);

                    tracks.Add(albumTrack);
                }
            }
        }
        return tracks;
    }

    public void RemoveTrackFromAlbum(int albumId, int trackNumber)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("DELETE FROM album_track WHERE album_id = @AlbumId AND track_number = @TrackNumber", connection);
            command.Parameters.AddWithValue("@AlbumId", albumId);
            command.Parameters.AddWithValue("@TrackNumber", trackNumber);
            command.ExecuteNonQuery();
        }
    }

    public void UpdateTrackInAlbum(int albumId, int oldTrackNumber, int newTrackNumber, int newSongId)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("UPDATE album_track SET track_number = @NewTrackNumber, song_id = @NewSongId WHERE album_id = @AlbumId AND track_number = @OldTrackNumber", connection);
            command.Parameters.AddWithValue("@AlbumId", albumId);
            command.Parameters.AddWithValue("@OldTrackNumber", oldTrackNumber);
            command.Parameters.AddWithValue("@NewTrackNumber", newTrackNumber);
            command.Parameters.AddWithValue("@NewSongId", newSongId);
            command.ExecuteNonQuery();
        }
    }

    public void CreateArtist(Artist artist)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("INSERT INTO artist (name) VALUES (@Name)", connection);
            command.Parameters.AddWithValue("@Name", artist.GetName());
            command.ExecuteNonQuery();
            int artistId = (int)command.LastInsertedId;  // Get the ID of the newly inserted artist

            artist.SetArtistId(artistId);  // Update the artist object with its new ID
        }
    }


    public Artist GetArtistById(int artistId)
    {
        Artist artist = null;
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT name FROM artist WHERE artist_id = @ArtistId", connection);
            command.Parameters.AddWithValue("@ArtistId", artistId);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    artist = new Artist();
                    artist.SetArtistId(artistId);
                    artist.SetName(reader.GetString("name"));
                }
            }
        }
        return artist;
    }

    public void UpdateArtist(Artist artist)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("UPDATE artist SET name = @Name WHERE artist_id = @ArtistId", connection);
            command.Parameters.AddWithValue("@Name", artist.GetName());
            command.Parameters.AddWithValue("@ArtistId", artist.GetArtistId());
            command.ExecuteNonQuery();
        }
    }

    public void DeleteArtist(int artistId)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("DELETE FROM artist WHERE artist_id = @ArtistId", connection);
            command.Parameters.AddWithValue("@ArtistId", artistId);
            command.ExecuteNonQuery();
        }
    }

    public void AddFeaturedArtist(int artistId, int songId)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("INSERT INTO featured_artists (artist_id, song_id) VALUES (@ArtistId, @SongId)", connection);
            command.Parameters.AddWithValue("@ArtistId", artistId);
            command.Parameters.AddWithValue("@SongId", songId);
            command.ExecuteNonQuery();
        }
    }

    public List<Featured_Artists> GetFeaturedArtistsBySong(int songId)
    {
        List<Featured_Artists> featuredArtists = new List<Featured_Artists>();
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT artist_id FROM featured_artists WHERE song_id = @SongId", connection);
            command.Parameters.AddWithValue("@SongId", songId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int artistId = reader.GetInt32("artist_id");
                    Artist artist = GetArtistById(artistId);  // Assuming this function is already defined
                    Song song = GetSongById(songId);          // Assuming this function is already defined

                    Featured_Artists featured = new Featured_Artists();
                    featured.SetArtistId(artist);
                    featured.SetSongId(song);
                    featuredArtists.Add(featured);
                }
            }
        }
        return featuredArtists;
    }

    public void RemoveFeaturedArtist(int artistId, int songId)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("DELETE FROM featured_artists WHERE artist_id = @ArtistId AND song_id = @SongId", connection);
            command.Parameters.AddWithValue("@ArtistId", artistId);
            command.Parameters.AddWithValue("@SongId", songId);
            command.ExecuteNonQuery();
        }
    }

    public void CreatePlaylist(BasePlaylist playlist)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("INSERT INTO playlist (title, queue, favourite, account_id) VALUES (@Title, @Queue, @Favourite, @AccountId)", connection);
            command.Parameters.AddWithValue("@Title", playlist.GetTitle());
            command.Parameters.AddWithValue("@Queue", playlist.GetQueue()); // Assuming fkqueue is now publicly accessible or has a getter method.
            command.Parameters.AddWithValue("@Favourite", playlist.GetFavourite()); // Similarly, assuming access to fkfavourite.
            command.Parameters.AddWithValue("@AccountId", playlist.GetAccountId()); // Assuming access or getter method for fkaccount_id.
            command.ExecuteNonQuery();
            int playlistId = (int)command.LastInsertedId;

            playlist.SetPlaylistId(playlistId); // Update the playlist object with its new ID
        }
    }

    public BasePlaylist GetPlaylistById(int playlistId)
    {
        BasePlaylist playlist = null;
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT title, queue, favourite, account_id FROM playlist WHERE playlist_id = @PlaylistId", connection);
            command.Parameters.AddWithValue("@PlaylistId", playlistId);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    playlist = new BasePlaylist();
                    playlist.SetPlaylistId(playlistId);
                    playlist.SetTitle(reader.GetString("title"));
                    playlist.SetQueue(reader.GetString("queue"));
                    playlist.SetFavourite(reader.GetString("favourite"));
                    playlist.SetAccountId(reader.GetInt32("account_id"));
                }
            }
        }
        return playlist;
    }

    public void UpdatePlaylist(BasePlaylist playlist)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("UPDATE playlist SET title = @Title, queue = @Queue, favourite = @Favourite, account_id = @AccountId WHERE playlist_id = @PlaylistId", connection);
            command.Parameters.AddWithValue("@Title", playlist.GetTitle());
            command.Parameters.AddWithValue("@Queue", playlist.GetQueue());
            command.Parameters.AddWithValue("@Favourite", playlist.GetFavourite());
            command.Parameters.AddWithValue("@AccountId", playlist.GetAccountId());
            command.Parameters.AddWithValue("@PlaylistId", playlist.GetPlaylistId());
            command.ExecuteNonQuery();
        }
    }

    public void DeletePlaylist(int playlistId)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var trackCommand = new MySqlCommand("DELETE FROM playlist_track WHERE playlist_id = @PlaylistId", connection);
            trackCommand.Parameters.AddWithValue("@PlaylistId", playlistId);
            trackCommand.ExecuteNonQuery();

            var command = new MySqlCommand("DELETE FROM playlist WHERE playlist_id = @PlaylistId", connection);
            command.Parameters.AddWithValue("@PlaylistId", playlistId);
            command.ExecuteNonQuery();
        }
    }

    public void CreateSong(Song song)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("INSERT INTO song (name, duration, song_location) VALUES (@Name, @Duration, @SongLocation)", connection);
            command.Parameters.AddWithValue("@Name", song.GetName());
            command.Parameters.AddWithValue("@Duration", song.GetDuration());
            command.Parameters.AddWithValue("@SongLocation", song.GetSongLocation());
            command.ExecuteNonQuery();
            int songId = (int)command.LastInsertedId;

            song.SetSongId(songId); // Update the song object with its new ID

            // Insert featured artists associated with the song
            foreach (var featured in song.GetFeaturedArtists())
            {
                var featuredCommand = new MySqlCommand("INSERT INTO featured_artists (song_id, artist_id) VALUES (@SongId, @ArtistId)", connection);
                featuredCommand.Parameters.AddWithValue("@SongId", songId);
                featuredCommand.Parameters.AddWithValue("@ArtistId", featured.GetArtistId());
                featuredCommand.ExecuteNonQuery();
            }
        }
    }


    public Song GetSongById(int songId)
    {
        Song song = null;
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT name, duration, song_location FROM song WHERE song_id = @SongId", connection);
            command.Parameters.AddWithValue("@SongId", songId);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    song = new Song();
                    song.SetSongId(songId);
                    song.SetName(reader.GetString("name"));
                    song.SetDuration(reader.GetString("duration"));
                    song.SetSongLocation(reader.GetString("song_location"));
                }
            }

            // Fetch the featured artists for this song
            command = new MySqlCommand("SELECT artist_id FROM featured_artists WHERE song_id = @SongId", connection);
            command.Parameters.AddWithValue("@SongId", songId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Artist artist = GetArtistById(reader.GetInt32("artist_id"));  // Assuming GetArtistById exists
                    song.AddFeaturedArtist(artist);
                }
            }
        }
        return song;
    }

    public void UpdateSong(Song song)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("UPDATE song SET name = @Name, duration = @Duration, song_location = @SongLocation WHERE song_id = @SongId", connection);
            command.Parameters.AddWithValue("@Name", song.GetName());
            command.Parameters.AddWithValue("@Duration", song.GetDuration());
            command.Parameters.AddWithValue("@SongLocation", song.GetSongLocation());
            command.Parameters.AddWithValue("@SongId", song.GetSongId());
            command.ExecuteNonQuery();
        }
    }

    public void DeleteSong(int songId)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var featuredCommand = new MySqlCommand("DELETE FROM featured_artists WHERE song_id = @SongId", connection);
            featuredCommand.Parameters.AddWithValue("@SongId", songId);
            featuredCommand.ExecuteNonQuery();

            var command = new MySqlCommand("DELETE FROM song WHERE song_id = @SongId", connection);
            command.Parameters.AddWithValue("@SongId", songId);
            command.ExecuteNonQuery();
        }
    }

    public void AddTrackToPlaylist(Playlist_Track track)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("INSERT INTO playlist_track (playlist_id, track_number, artist, title, duration, track_uri, song_id) VALUES (@PlaylistId, @TrackNumber, @Artist, @Title, @Duration, @TrackUri, @SongId)", connection);
            command.Parameters.AddWithValue("@PlaylistId", track.GetPlaylistId());
            command.Parameters.AddWithValue("@TrackNumber", track.GetTrackNumber());
            command.Parameters.AddWithValue("@Artist", track.GetArtist());
            command.Parameters.AddWithValue("@Title", track.GetTitle());
            command.Parameters.AddWithValue("@Duration", track.GetDuration().TotalSeconds);
            command.Parameters.AddWithValue("@TrackUri", track.GetTrackUri());
            command.Parameters.AddWithValue("@SongId", track.GetSongId());
            command.ExecuteNonQuery();
        }
    }

    public List<Playlist_Track> GetTracksForPlaylist(int playlistId)
    {
        List<Playlist_Track> tracks = new List<Playlist_Track>();
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT track_number, artist, title, duration, track_uri, song_id FROM playlist_track WHERE playlist_id = @PlaylistId ORDER BY track_number", connection);
            command.Parameters.AddWithValue("@PlaylistId", playlistId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var track = new Playlist_Track(new Song()); // Assuming Song constructor handles defaults
                    track.SetPlaylistId(playlistId);
                    track.SetTrackNumber(reader["track_number"].ToString());
                    track.SetArtist(reader.GetString("artist"));
                    track.SetTitle(reader.GetString("title"));
                    track.SetDuration(TimeSpan.FromSeconds(reader.GetDouble("duration")));
                    track.SetTrackUri(reader.GetString("track_uri"));
                    track.SetSongId(reader.GetInt32("song_id"));
                    tracks.Add(track);
                }
            }
        }
        return tracks;
    }

    public void UpdatePlaylistTrack(Playlist_Track track)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("UPDATE playlist_track SET artist = @Artist, title = @Title, duration = @Duration, track_uri = @TrackUri, song_id = @SongId WHERE playlist_id = @PlaylistId AND track_number = @TrackNumber", connection);
            command.Parameters.AddWithValue("@Artist", track.GetArtist());
            command.Parameters.AddWithValue("@Title", track.GetTitle());
            command.Parameters.AddWithValue("@Duration", track.GetDuration().TotalSeconds);
            command.Parameters.AddWithValue("@TrackUri", track.GetTrackUri());
            command.Parameters.AddWithValue("@SongId", track.GetSongId());
            command.Parameters.AddWithValue("@PlaylistId", track.GetPlaylistId());
            command.Parameters.AddWithValue("@TrackNumber", track.GetTrackNumber());
            command.ExecuteNonQuery();
        }
    }

    public void RemoveTrackFromPlaylist(int playlistId, int trackNumber)
    {
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("DELETE FROM playlist_track WHERE playlist_id = @PlaylistId AND track_number = @TrackNumber", connection);
            command.Parameters.AddWithValue("@PlaylistId", playlistId);
            command.Parameters.AddWithValue("@TrackNumber", trackNumber);
            command.ExecuteNonQuery();
        }
    }

    //-------------------------------------------------------------------------Below are method and functions that write information from the program to the database-----------------------------------------------------------------------------------

/*    public void SaveAlbum(Album album)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Check if the album already exists
                    var checkAlbumCommand = new SqlCommand("SELECT COUNT(*) FROM album WHERE album_id = @AlbumId", connection, transaction);
                    checkAlbumCommand.Parameters.AddWithValue("@AlbumId", album.AlbumId);
                    int albumExists = (int)checkAlbumCommand.ExecuteScalar();

                    if (albumExists == 0)
                    {
                        // Save the new Album
                        var albumCommand = new SqlCommand("INSERT INTO album (album_id, title, image_location) VALUES (@Id, @Title, @Image)", connection, transaction);
                        albumCommand.Parameters.AddWithValue("@Id", album.AlbumId);
                        albumCommand.Parameters.AddWithValue("@Title", album.AlbumTitle);
                        albumCommand.Parameters.AddWithValue("@Image", album.ImageLocation);
                        albumCommand.ExecuteNonQuery();

                        // Save each Song and its relationship in Album_Track
                        foreach (var song in album.Tracks)
                        {
                            var songCommand = new SqlCommand("INSERT INTO song (song_id, name, duration, song_location) VALUES (@SongId, @Name, @Duration, @Location)", connection, transaction);
                            songCommand.Parameters.AddWithValue("@SongId", song.SongId);
                            songCommand.Parameters.AddWithValue("@Name", song.Name);
                            songCommand.Parameters.AddWithValue("@Duration", song.Duration);
                            songCommand.Parameters.AddWithValue("@Location", song.SongLocation);
                            songCommand.ExecuteNonQuery();

                            var trackCommand = new SqlCommand("INSERT INTO album_track (album_id, song_id) VALUES (@AlbumId, @SongId)", connection, transaction);
                            trackCommand.Parameters.AddWithValue("@AlbumId", album.AlbumId);
                            trackCommand.Parameters.AddWithValue("@SongId", song.SongId);
                            trackCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Album already exists. No new songs were added.");
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Failed to save album", ex);
                }
            }
        }
    }*/


}