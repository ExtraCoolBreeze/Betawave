/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This view model was created to manage the interactions, updating and displaying of the UI on AddMedia content page  */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Betawave.Classes;

namespace Betawave.ViewModels
{
    public class AddMediaViewModel : INotifyPropertyChanged
    {   //declaring event handler
        public event PropertyChangedEventHandler PropertyChanged;
        //declaring class objects
        public DatabaseAccess dbAccess;
        public DatabaseManager databaseManager;
        public AlbumManager albumManager;
        public SongManager songManager;
        public ArtistManager artistManager;
        public AudioViewModel audioViewModel;
        //declaring commands
        public ICommand PlayAlbum1Command { get; private set; }
        public ICommand PlayAlbum2Command { get; private set; }
        public ICommand PlayAlbum3Command { get; private set; }

        public ICommand PlayPauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand SkipNextCommand { get; private set; }
        public ICommand SkipPreviousCommand { get; private set; }
        public ICommand ToggleShuffleCommand { get; private set; }

        //accessing class properties
        public string CurrentTrackName => audioViewModel.CurrentTrackName;
        public string CurrentTrackArtist => audioViewModel.CurrentTrackArtist;
        public string CurrentTrackImage => audioViewModel.CurrentTrackImage;
        public string TrackLength => audioViewModel.TrackLength;

        //creating volume property
        public float Volume
        {
            get => audioViewModel.Volume;
            set
            {
                if (audioViewModel.Volume != value)
                {
                    audioViewModel.Volume = value;
                    OnPropertyChanged(nameof(Volume));
                }
            }
        }
        //declaring album detail variables
        private string albumImagePath1;
        private string albumName1;
        private string artistName1;

        private string albumImagePath2;
        private string albumName2;
        private string artistName2;

        private string albumImagePath3;
        private string albumName3;
        private string artistName3;

        //creating album properties
        public string AlbumImagePath1
        {
            get { return albumImagePath1; }
            set { albumImagePath1 = value; OnPropertyChanged(); }
        }

        public string AlbumName1
        {
            get { return albumName1; }
            set { albumName1 = value; OnPropertyChanged(); }
        }

        public string ArtistName1
        {
            get { return artistName1; }
            set { artistName1 = value; OnPropertyChanged(); }
        }

        public string AlbumImagePath2
        {
            get { return albumImagePath2; }
            set { albumImagePath2 = value; OnPropertyChanged(); }
        }

        public string AlbumName2
        {
            get { return albumName2; }
            set { albumName2 = value; OnPropertyChanged(); }
        }

        public string ArtistName2
        {
            get { return artistName2; }
            set { artistName2 = value; OnPropertyChanged(); }
        }

        public string AlbumImagePath3
        {
            get { return albumImagePath3; }
            set { albumImagePath3 = value; OnPropertyChanged(); }
        }

        public string AlbumName3
        {
            get { return albumName3; }
            set { albumName3 = value; OnPropertyChanged(); }
        }

        public string ArtistName3
        {
            get { return artistName3; }
            set { artistName3 = value; OnPropertyChanged(); }
        }

        //class constuctor
        public AddMediaViewModel(AudioViewModel audioViewModel)
        {
            this.audioViewModel = audioViewModel;
            this.audioViewModel.PropertyChanged += AudioViewModel_PropertyChanged;

            dbAccess = new DatabaseAccess();
            databaseManager = new DatabaseManager(dbAccess);
            artistManager = new ArtistManager(dbAccess);
            albumManager = new AlbumManager(dbAccess);
            songManager = new SongManager(dbAccess);

            InitializeCommands();
            LoadData();
        }

        /// <summary>
        /// When called this method creates and binds commands
        /// </summary>
        private void InitializeCommands()
        {   
            PlayAlbum1Command = new Command(() => PlayAlbum(0));
            PlayAlbum2Command = new Command(() => PlayAlbum(1));
            PlayAlbum3Command = new Command(() => PlayAlbum(2));

            PlayPauseCommand = new Command(() => audioViewModel.TogglePlayPause());
            StopCommand = new Command(() => audioViewModel.StopAudio());
            SkipNextCommand = new Command(() => audioViewModel.SkipNext());
            SkipPreviousCommand = new Command(() => audioViewModel.SkipPrevious());
            ToggleShuffleCommand = new Command(() => audioViewModel.ToggleShuffle());
        }

        /// <summary>
        /// This method makes sure all data is loaded from the database required to be displayed on the add media page
        /// </summary>
        public async Task LoadData()
        {
            //loads albums and artists
            await albumManager.LoadAlbums();
            await artistManager.LoadArtists();
            List<Album> albums = albumManager.GetAllAlbums(); //adds objects to list of objects
            List<Artist> artists = artistManager.GetAllArtists();
            //is the lists are not empty
            if (albums.Count > 0 && artists.Count > 0)
            {
                AlbumImagePath1 = albums[0].GetImageLocation();
                AlbumName1 = albums[0].GetAlbumTitle();

                Artist artist1 = artistManager.GetArtistById(albums[0].GetArtistId());
                //if artist not null
                string artistName1;
                if (artist1 != null)
                {   
                    // get artist name
                    artistName1 = artist1.GetName();
                }
                else
                {
                   // if null return error
                    artistName1 = "Unknown Artist";
                }

                //add to property
                ArtistName1 = artistName1;

            }
            //for second album repeat the process
            if (albums.Count > 1)
            {
                AlbumImagePath2 = albums[1].GetImageLocation();
                AlbumName2 = albums[1].GetAlbumTitle();

                Artist artist2 = artistManager.GetArtistById(albums[1].GetArtistId());
                // Initialize ArtistName2
                string artistName2;

                // Check if artist is not null
                if (artist2 != null)
                {
                    // If artist null, get the artist name
                    artistName2 = artist2.GetName();
                }
                else
                {
                    // If artist is null, print error
                    artistName2 = "Unknown Artist";
                }

                // add to property
                ArtistName2 = artistName2;

            }
            //for third album repeat the process
            if (albums.Count > 2)
            {
                AlbumImagePath3 = albums[2].GetImageLocation();
                AlbumName3 = albums[2].GetAlbumTitle();

                Artist artist3 = artistManager.GetArtistById(albums[2].GetArtistId());
                string artistName3;

                // Check if artist is not null
                if (artist3 != null)
                {
                    // If artist not null, get the artist name
                    artistName3 = artist3.GetName();
                }
                else
                {
                    // if null, print error
                    artistName3 = "Unknown Artist";
                }

                // add to property
                ArtistName3 = artistName3;
            }
        }

        /// <summary>
        /// When called and passed an album index from albums loaded into add media page, this method grabs the associated album based on the passed album index then creates and plays a playlist of the album songs
        /// </summary>
        /// <param name="albumIndex"></param>
        public async void PlayAlbum(int albumIndex)
        {
            List<Album> albums = albumManager.GetAllAlbums();
            if (albumIndex < albums.Count)
            {
                Album album = albums[albumIndex];
                List<Song> songsForAlbum = await songManager.GetSongsForAlbum(album.GetAlbumId());
                BasePlaylist playlist = new BasePlaylist();

                // Set the album details in the playlist
                playlist.SetAlbumName(album.GetAlbumTitle());

                // Fetch the artist and set the artist name in the playlist
                Artist artist = artistManager.GetArtistById(album.GetArtistId());
                if (artist != null)
                {
                    playlist.SetArtistName(artist.GetName());
                }
                else
                {
                    playlist.SetArtistName("Unknown Artist"); // set error
                }

                // Add songs to the playlist
                foreach (Song song in songsForAlbum)
                {
                    playlist.AddToPlaylist(song);
                }

                // Play the playlist
                audioViewModel.SetPlaylistAndPlay(playlist);
            }
        }


        //method to trigger when properties change
        public void AudioViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        //method to trigger when properties change
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
