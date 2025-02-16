﻿/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This view model was created to manage the interactions, updating and displaying of the UI on the Main menu content page  */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Betawave.Classes;


namespace Betawave.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AlbumManager albumManager;
        public SongManager songManager;
        public ArtistManager artistManager;
        public AudioViewModel audioViewModel;

        public ICommand PlayAlbum1Command { get; private set; }
        public ICommand PlayAlbum2Command { get; private set; }
        public ICommand PlayAlbum3Command { get; private set; }

        public ICommand PlayPauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand SkipNextCommand { get; private set; }
        public ICommand SkipPreviousCommand { get; private set; }
        public ICommand ToggleShuffleCommand { get; private set; }

        public string CurrentTrackName => audioViewModel.CurrentTrackName;
        public string CurrentTrackArtist => audioViewModel.CurrentTrackArtist;
        public string CurrentTrackImage => audioViewModel.CurrentTrackImage;
        public double CurrentTrackPosition => audioViewModel.CurrentTrackPosition;
        public double TrackLength => audioViewModel.TrackLength;

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

        public bool Shuffle
        {
            get => audioViewModel.Shuffle;
            set
            {
                if (audioViewModel.Shuffle != value)
                {
                    audioViewModel.Shuffle = value;
                    OnPropertyChanged(nameof(Shuffle));
                }
            }
        }

        private string albumImagePath1;
        private string albumName1;
        private string artistName1;

        private string albumImagePath2;
        private string albumName2;
        private string artistName2;

        private string albumImagePath3;
        private string albumName3;
        private string artistName3;

        public string AlbumImagePath1
        {
            get => albumImagePath1;
            set { albumImagePath1 = value; OnPropertyChanged(); }
        }

        public string AlbumName1
        {
            get => albumName1;
            set { albumName1 = value; OnPropertyChanged(); }
        }

        public string ArtistName1
        {
            get => artistName1;
            set { artistName1 = value; OnPropertyChanged(); }
        }

        public string AlbumImagePath2
        {
            get => albumImagePath2;
            set { albumImagePath2 = value; OnPropertyChanged(); }
        }

        public string AlbumName2
        {
            get => albumName2;
            set { albumName2 = value; OnPropertyChanged(); }
        }

        public string ArtistName2
        {
            get => artistName2;
            set { artistName2 = value; OnPropertyChanged(); }
        }

        public string AlbumImagePath3
        {
            get => albumImagePath3;
            set { albumImagePath3 = value; OnPropertyChanged(); }
        }

        public string AlbumName3
        {
            get => albumName3;
            set { albumName3 = value; OnPropertyChanged(); }
        }

        public string ArtistName3
        {
            get => artistName3;
            set { artistName3 = value; OnPropertyChanged(); }
        }



        public MainMenuViewModel(AudioViewModel audioViewModel)
        {
            
            PlayAlbum1Command = new Command(() => PlayAlbum(0));
            PlayAlbum2Command = new Command(() => PlayAlbum(1));
            PlayAlbum3Command = new Command(() => PlayAlbum(2));

            PlayPauseCommand = new Command(() => audioViewModel.TogglePlayPause());
            StopCommand = new Command(() => audioViewModel.StopAudio());
            SkipNextCommand = new Command(() => audioViewModel.SkipNext());
            SkipPreviousCommand = new Command(() => audioViewModel.SkipPrevious());
            ToggleShuffleCommand = new Command(() => audioViewModel.ToggleShuffle());

            this.audioViewModel = audioViewModel;
            this.audioViewModel.PropertyChanged += AudioViewModel_PropertyChanged;
            var dbAccess = new DatabaseAccess();
            var manager = new DatabaseManager(dbAccess);
            manager.LoadInAllManagerClassData();

            artistManager = new ArtistManager(dbAccess);
            albumManager = new AlbumManager(dbAccess);
            songManager = new SongManager(dbAccess);

            LoadData();
        }

        public async void LoadData()
        {
            await albumManager.LoadAlbums();
            await artistManager.LoadArtists();
            List<Album> albums = albumManager.GetAllAlbums();
            List<Artist> artists = artistManager.GetAllArtists();

            if (albums.Count > 0 && artists.Count > 0)
            {
                AlbumImagePath1 = albums[0].GetImageLocation();
                AlbumName1 = albums[0].GetAlbumTitle();

                Artist artist1 = artistManager.GetArtistById(albums[0].GetArtistId());
                if (artist1 != null)
                {
                    ArtistName1 = artist1.GetName();
                }
                else
                {
                    ArtistName1 = "Unknown Artist";
                }
            }

            if (albums.Count > 1)
            {
                AlbumImagePath2 = albums[1].GetImageLocation();
                AlbumName2 = albums[1].GetAlbumTitle();

                Artist artist2 = artistManager.GetArtistById(albums[1].GetArtistId());
                if (artist2 != null)
                {
                    ArtistName2 = artist2.GetName();
                }
                else
                {
                    ArtistName2 = "Unknown Artist";
                }
            }

            if (albums.Count > 2)
            {
                AlbumImagePath3 = albums[2].GetImageLocation();
                AlbumName3 = albums[2].GetAlbumTitle();

                Artist artist3 = artistManager.GetArtistById(albums[2].GetArtistId());
                if (artist3 != null)
                {
                    ArtistName3 = artist3.GetName();
                }
                else
                {
                    ArtistName3 = "Unknown Artist";
                }
            }
        }




        // also this   SELECT COUNT(*) FROM your_table; Control the number of rows inserted by checking the current count of rows before inserting new data. 
        // use this to limit how many accounts or items that can be input into a table. MUST USE!

        /*        DELIMITER //
        CREATE TRIGGER check_row_count_before_insert
        BEFORE INSERT ON your_table
        FOR EACH ROW
        BEGIN
         DECLARE row_count INT;
        SET row_count = (SELECT COUNT(*) FROM your_table);
        IF row_count >= 1000 THEN
            SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cannot insert more than 1000 rows';
        END IF;
        END;
    //
    DELIMITER ;*/

        private async void PlayAlbum(int albumIndex)
        {
            List<Album> albums = albumManager.GetAllAlbums();
            if (albumIndex < albums.Count)
            {
                Album album = albums[albumIndex];
                List<Song> songsForAlbum = await songManager.GetSongsForAlbum(album.GetAlbumId());
                BasePlaylist playlist = new BasePlaylist();
                foreach (Song song in songsForAlbum)
                {
                    playlist.AddToPlaylist(song);
                }
                playlist.SetAlbumName(album.GetAlbumTitle());

                Artist artist1 = artistManager.GetArtistById(album.GetArtistId());
                string ArtistName = artist1.GetName();
                playlist.SetArtistName(ArtistName);
                audioViewModel.SetPlaylistAndPlay(playlist);
            }
        }

        private void AudioViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Relay the property changed event to the View
            OnPropertyChanged(e.PropertyName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
