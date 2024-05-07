/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This view model was created to manage the interactions, updating and displaying of the UI on AddAlbum content page */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Betawave.Classes;

namespace Betawave.ViewModels
{
    public class AddAlbumViewModel : INotifyPropertyChanged
    {

        private string albumName;
        private string artistName;
        private string albumArtPath;
        private string song1;
        private string song2;
        private string song3;
        private string song4;
        private string song5;
        private string song6;
        private string song7;
        private string song8;
        private string song9;
        private string song10;
        private string song11;

        private string song1Location;
        private string song2Location;
        private string song3Location;
        private string song4Location;
        private string song5Location;
        private string song6Location;
        private string song7Location;
        private string song8Location;
        private string song9Location;
        private string song10Location;
        private string song11Location;

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
            set
            {
                song11 = value;
                OnPropertyChanged();
            }

        }

        public string Song11Location
        {
            get => song11Location;
            set { song11Location = value; OnPropertyChanged(); }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private ArtistManager artistManager;
        private AlbumManager albumManager;
        private DatabaseAccess databaseAccess;
        public ICommand AddAlbumCommand { get; private set; }

        public AddAlbumViewModel()
        {
            artistManager = new ArtistManager(new DatabaseAccess());
            albumManager = new AlbumManager(new DatabaseAccess(), artistManager);
            AddAlbumCommand = new Command(async () => await AddNewAlbum());
        }

        public void ResetFields()
        {
            AlbumName = string.Empty;
            ArtistName = string.Empty;
            AlbumArtPath = string.Empty;
            Song1 = string.Empty;
            Song2 = string.Empty;
            Song3 = string.Empty;
            Song5 = string.Empty;
            Song6 = string.Empty;
            Song7 = string.Empty;
            Song8 = string.Empty;
            Song9 = string.Empty;
            Song10 = string.Empty;
            Song11 = string.Empty;

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

        private async Task AddNewAlbum()
        {
            if (await albumManager.CountAlbums() >= 3)
            {
                await ShowAlert("Limit Reached", "The maximum number of albums (3) has been reached.");
                return;
            }

            Artist artist = await CreateArtistAsync(ArtistName);
            if (artist == null)
            {
                await ShowAlert("Error", "Failed to create or find the artist.");
                return;
            }

            Album newAlbum = CreateAlbum(artist);
            bool isAdded = await albumManager.AddAlbum(newAlbum);
            if (!isAdded)
            {
                await ShowAlert("Limit Reached", "The maximum number of albums (3) has been reached. No more albums can be added.");
            }
            else
            {
                await ShowAlert("Success", "Album added successfully.");
            }
        }

        private Album CreateAlbum(Artist artist)
        {
            if (artist == null)
            {
                return null;
            }


            Album newAlbum = new Album(artistManager);
            newAlbum.SetAlbumTitle(AlbumName);
            newAlbum.SetImageLocation(AlbumArtPath);
            newAlbum.SetArtistId(artist.GetArtistId());

            return newAlbum;
        }


        private async Task<Artist> CreateArtistAsync(string artistName)
        {
            Artist artist = await artistManager.GetArtistByName(artistName);
            if (artist == null)
            {
                artist = new Artist();
                artist.SetName(artistName);  // Assuming Artist class has a public setter for Name
                artistManager.AddArtist(artist);
            }
            return artist;
        }

        private async Task ShowAlert(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
