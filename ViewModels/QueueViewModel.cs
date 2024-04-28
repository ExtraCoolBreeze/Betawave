using System.Collections.ObjectModel;
using System.ComponentModel;

public class QueueViewModel : INotifyPropertyChanged
{
    private Song currentSong;

    public ObservableCollection<Song> Songs { get; set; }
    public Song CurrentSong
    {
        get => currentSong;
        set { currentSong = value; OnPropertyChanged(); }
    }

    public QueueViewModel()
    {
        Songs = new ObservableCollection<Song>();
        LoadSongs();
        SubscribeToChanges();
    }

    private void SubscribeToChanges()
    {
        // Assuming Player is accessible somehow, subscribe to its TrackChanged event
        Player.TrackChanged += Player_TrackChanged;
    }

    private void Player_TrackChanged(object sender, EventArgs e)
    {
        CurrentSong = Player.GetCurrentTrack(); // Ensure you have a method to get the current track as a Song object
    }

}
