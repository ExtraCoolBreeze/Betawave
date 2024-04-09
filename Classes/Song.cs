using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    using System;

    public class Song
    {
        private static int _nextSongId = 1;
        private int songId;
        private string name;
        private string duration;
        private string songLocation;

        public Song()
        {
            songId = _nextSongId++;
            name = string.Empty;
            duration = string.Empty;
            songLocation = string.Empty;
        }

        // Getter for SongId, no setter since SongId should be read-only after initialization
        public int GetSongId()
        {
            return songId;
        }

        // Getter and Setter for Name
        public string GetName()
        {
            return name;
        }

        public void SetName(string nameValue)
        {
            name = nameValue;
        }

        // Getter and Setter for Duration
        public string GetDuration()
        {
            return duration;
        }

        public void SetDuration(string durationValue)
        {
            duration = durationValue;
        }

        // Getter and Setter for SongLocation
        public string GetSongLocation()
        {
            return songLocation;
        }

        public void SetSongLocation(string locationValue)
        {
            songLocation = locationValue;
        }

        public void PrintSongDetails()
        {
            Console.WriteLine($"Song ID: {songId}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Duration: {duration}");
            Console.WriteLine($"Location: {songLocation}");
        }
    }


}
