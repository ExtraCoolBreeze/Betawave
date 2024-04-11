using Betawave.Classes;

    public class Playlist_Track
    {
        private string _title;
        private int _trackNumber;
        private string _artist;
        private string _trackUri;
        private TimeSpan _duration;

        public Playlist_Track(Song song)
        {
            _title = "";
            _trackNumber = 0;
            _artist = "";
            _trackUri = song.GetSongLocation();
            _duration = TimeSpan.Zero; // Initializes duration to 0
        }

        public void SetTitle(string title)
        {
            _title = title;
        }

        public void SetArtist(string artist)
        {
            _artist = artist;
        }

        public void SetDuration(TimeSpan duration)
        {
            _duration = duration;
        }

        public void SetTrackNumber(string userInput)
        {
            if (int.TryParse(userInput, out int parsedTrackNumber))
            {
                _trackNumber = parsedTrackNumber;
            }
            else
            {
                Console.WriteLine("Invalid input for track number.");
            }
        }

        public void SetTrackUri(string _trackUri)
        {
            _trackUri = _trackUri;
        }

        public string GetTrackUri()
        { 
            return _trackUri;
        }

        public int GetTrackNumber()
        {
            return _trackNumber;
        }

        public void PrintPlaylistTrack()
        {
            Console.WriteLine($"Title: {_title}, Track Number: {GetTrackNumber()}, Artist: {_artist}, Duration: {_duration}");
        }
    }

