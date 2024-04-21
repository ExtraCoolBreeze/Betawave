using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Betawave.Classes;
using Microsoft.Maui.Controls;
using Org.BouncyCastle.Utilities.Zlib;

namespace Betawave.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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


        public MainMenuViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            // This is where you would load your data
            AlbumImagePath1 = "path_to_album_image_1.jpg";
            AlbumName1 = "Album One";
            ArtistName1 = "Artist One";

            AlbumImagePath2 = "path_to_album_image_2.jpg";
            AlbumName2 = "Album Two";
            ArtistName2 = "Artist Two";

            AlbumImagePath3 = "path_to_album_image_3.jpg";
            AlbumName3 = "Album Three";
            ArtistName3 = "Artist Three";
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


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
