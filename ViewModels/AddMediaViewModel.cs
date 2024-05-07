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
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AlbumManager albumManager;
        public SongManager songManager;
        private AudioViewModel audioViewModel;

        public ICommand PlayAlbum1Command { get; private set; }
        public ICommand PlayAlbum2Command { get; private set; }
        public ICommand PlayAlbum3Command { get; private set; }
        public ICommand DeleteAlbum1Command { get; private set; }
        public ICommand DeleteAlbum2Command { get; private set; }
        public ICommand DeleteAlbum3Command { get; private set; }

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

        public AddMediaViewModel(AudioViewModel audioViewModel)
        {
            PlayAlbum1Command = new Command(() => PlayAlbum(0));
            PlayAlbum2Command = new Command(() => PlayAlbum(1));
            PlayAlbum3Command = new Command(() => PlayAlbum(2));

            DeleteAlbum1Command = new Command(() => DeleteAlbum(0));
            DeleteAlbum2Command = new Command(() => DeleteAlbum(1));
            DeleteAlbum3Command = new Command(() => DeleteAlbum(2));

            PlayPauseCommand = new Command(() => audioViewModel.TogglePlayPause());
            StopCommand = new Command(() => audioViewModel.StopAudio());
            SkipNextCommand = new Command(() => audioViewModel.SkipNext());
            SkipPreviousCommand = new Command(() => audioViewModel.SkipPrevious());
            ToggleShuffleCommand = new Command(() => audioViewModel.ToggleShuffle());

            this.audioViewModel = audioViewModel;
            this.audioViewModel.PropertyChanged += AudioViewModel_PropertyChanged;

            var dbAccess = new DatabaseAccess();
            var manager = new DatabaseManager(dbAccess);
            manager.LoadAllDataAsync();

            var artistManager = new ArtistManager(dbAccess);
            albumManager = new AlbumManager(dbAccess, artistManager);
            songManager = new SongManager(dbAccess, artistManager, albumManager);

            LoadData();
        }

        public async void LoadData()
        {
            await albumManager.LoadAlbumsAsync();
            var albums = albumManager.GetAllAlbums();

            if (albums.Count > 0)
            {
                AlbumImagePath1 = albums[0].GetImageLocation();
                AlbumName1 = albums[0].GetAlbumTitle();
                ArtistName1 = albums[0].GetArtist()?.GetName();
            }

            if (albums.Count > 1)
            {
                AlbumImagePath2 = albums[1].GetImageLocation();
                AlbumName2 = albums[1].GetAlbumTitle();
                ArtistName2 = albums[1].GetArtist()?.GetName();
            }

            if (albums.Count > 2)
            {
                AlbumImagePath3 = albums[2].GetImageLocation();
                AlbumName3 = albums[2].GetAlbumTitle();
                ArtistName3 = albums[2].GetArtist()?.GetName();
            }
        }

        private async void PlayAlbum(int albumIndex)
        {
            var albums = albumManager.GetAllAlbums();
            if (albumIndex < albums.Count)
            {
                Album album = albums[albumIndex];
                var songsForAlbum = await songManager.GetSongsForAlbum(album.GetAlbumId());
                BasePlaylist playlist = new BasePlaylist();
                foreach (var song in songsForAlbum)
                {
                    playlist.AddSong(song);
                }

                audioViewModel.SetPlaylistAndPlay(playlist);
            }
        }

        public void DeleteAlbum(int albumIndex)
        {
            var albums = albumManager.GetAllAlbums();
            if (albumIndex < albums.Count)
            {
                int albumId = albums[albumIndex].GetAlbumId(); // Retrieve album ID
                albumManager.DeleteAlbum(albumId); // Pass the album ID
                LoadData(); // Reload data after deletion
            }
        }


        public void AudioViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
