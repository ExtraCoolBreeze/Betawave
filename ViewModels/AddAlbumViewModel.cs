using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Betawave.Classes;
using Microsoft.Maui.Controls;

namespace Betawave.ViewModels
{
    public class AddAlbumViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ResetFieldsCommand { get; private set; }

       
        private string albumName;
        private string artistName;
        private string albumArtPath;
        private string song1, song1Location;
        private string song2, song2Location;
        private string song3, song3Location;
        private string song4, song4Location;
        private string song5, song5Location;
        private string song6, song6Location;
        private string song7, song7Location;
        private string song8, song8Location;
        private string song9, song9Location;
        private string song10, song10Location;
        private string song11, song11Location;
        private ArtistManager artistManager = new ArtistManager(new DatabaseAccess());
        private AlbumManager albumManager = new AlbumManager(new DatabaseAccess(), artistManager);


        public ICommand AddAlbumCommand { get; private set; }

        public AddAlbumViewModel()
        {
            AddAlbumCommand = new Command(AddAlbumAsync);
            ResetFieldsCommand = new Command(ResetFields);
            AddAlbumCommand = new Command(async () => await AddNewAlbum());
            ResetFields();
        }

        public string AlbumName
        {
            get => albumName;
            set { albumName = value; OnPropertyChanged(); }
        }

        public string ArtistName
        {
            get => artistName;
            set { artistName = value; OnPropertyChanged(); }
        }

        public string AlbumArtPath
        {
            get => albumArtPath;
            set { albumArtPath = value; OnPropertyChanged(); }
        }

        public string Song1
        {
            get => song1;
            set { song1 = value; OnPropertyChanged(); }
        }

        public string Song1Location
        {
            get => song1Location;
            set { song1Location = value; OnPropertyChanged(); }
        }

        public string Song2
        {
            get => song2;
            set { song2 = value; OnPropertyChanged(); }
        }

        public string Song2Location
        {
            get => song2Location;
            set { song2Location = value; OnPropertyChanged(); }
        }

        public string Song3
        {
            get => song3;
            set { song3 = value; OnPropertyChanged(); }
        }

        public string Song3Location
        {
            get => song3Location;
            set { song3Location = value; OnPropertyChanged(); }
        }

        public string Song4
        {
            get => song4;
            set { song4 = value; OnPropertyChanged(); }
        }

        public string Song4Location
        {
            get => song4Location;
            set { song4Location = value; OnPropertyChanged(); }
        }

        public string Song5
        {
            get => song5;
            set { song5 = value; OnPropertyChanged(); }
        }

        public string Song5Location
        {
            get => song5Location;
            set { song5Location = value; OnPropertyChanged(); }
        }

        public string Song6
        {
            get => song6;
            set { song6 = value; OnPropertyChanged(); }
        }

        public string Song6Location
        {
            get => song6Location;
            set { song6Location = value; OnPropertyChanged(); }
        }

        public string Song7
        {
            get => song7;
            set { song7 = value; OnPropertyChanged(); }
        }

        public string Song7Location
        {
            get => song7Location;
            set { song7Location = value; OnPropertyChanged(); }
        }

        public string Song8
        {
            get => song8;
            set { song8 = value; OnPropertyChanged(); }
        }

        public string Song8Location
        {
            get => song8Location;
            set { song8Location = value; OnPropertyChanged(); }
        }

        public string Song9
        {
            get => song9;
            set { song9 = value; OnPropertyChanged(); }
        }

        public string Song9Location
        {
            get => song9Location;
            set { song9Location = value; OnPropertyChanged(); }
        }

        public string Song10
        {
            get => song10;
            set { song10 = value; OnPropertyChanged(); }
        }

        public string Song10Location
        {
            get => song10Location;
            set { song10Location = value; OnPropertyChanged(); }
        }

        public string Song11
        {
            get => song11;
            set { song11 = value; OnPropertyChanged(); }
        }

        public string Song11Location
        {
            get => song11Location;
            set { song11Location = value; OnPropertyChanged(); }
        }

        public void ResetFields()
        {
            AlbumName = string.Empty;
            ArtistName = string.Empty;
            AlbumArtPath = string.Empty;
            Song1 =string.Empty;
            Song2 =string.Empty;
            Song3 =string.Empty;
            Song4 =string.Empty;
            Song5 =string.Empty;
            Song6 =string.Empty;
            Song7 =string.Empty;
            Song8 =string.Empty;
            Song9 =string.Empty;
            Song10 =string.Empty;
            Song11 =string.Empty;

            Song1Location = string.Empty;
            Song2Location = string.Empty;
            Song3Location = string.Empty;
            Song4Location = string.Empty;
            Song5Location = string.Empty;
            Song6Location = string.Empty;
            Song7Location = string.Empty;
            Song8Location = string.Empty;
            Song9Location = string.Empty;
            Song10Location = string.Empty;
            Song11Location = string.Empty;
        }

        public bool ValidateInputs()
        {
            return !string.IsNullOrEmpty(AlbumName) && !string.IsNullOrEmpty(ArtistName) && !string.IsNullOrEmpty(AlbumArtPath);
        }

        public async Task<Artist> CreateArtistAsync(string artistName)
        {
            var artist = artistManager.GetArtistByName(artistName);

            if (artist == null)
            {
                artist = new Artist();
                artist.SetName(artistName);

                artistManager.AddArtist(artist);

            }

            return artist;
        }



        public Album CreateAlbum(Artist artist)
        {
            Album newAlbum = new Album(artistManager);

            newAlbum.SetAlbumTitle(AlbumName);
            newAlbum.SetImageLocation(albumArtPath);
            if (artist != null)
            {
                newAlbum.SetArtistId(artist.GetArtistId());
            }

            return newAlbum;
        }


        private async Task ShowFeedbackAsync(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }


        private List<Album_Track> CreateTracksForAlbum(Album album)
        {
            var tracks = new List<Album_Track>();

            var track = new Album_Track
            {
                SongId = 1,          // Example song ID
                TrackNumber = 1,
                AlbumId = album.GetAlbumId()
            };
            tracks.Add(track);

            return tracks;
        }

        public async void AddAlbumAsync(object obj)
        {
            if (!ValidateInputs())
            {
                await ShowFeedbackAsync("Input Required", "Please fill in the album name, artist name, and album art path fields.");
                return;
            }

            Artist artist = await CreateArtistAsync(ArtistName);
            if (artist == null)
            {
                await ShowFeedbackAsync("Error", "Failed to create or find the artist.");
                return;
            }

            Album album = CreateAlbum(artist);
            if (album == null)
            {
                await ShowFeedbackAsync("Error", "Failed to create the album.");
                return;
            }

            List<Album_Track> tracks = CreateTracksForAlbum(album);

            await ShowFeedbackAsync("Success", "Album added successfully.");
            await Shell.Current.GoToAsync("//AddMediaScreen");
        }

        //This program has an artificial limit to read and write 3 albums only

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
