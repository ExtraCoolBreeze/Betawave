using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class PlaylistControl
    {
        private int playlistId;
        private string title;
        private List<Song> songs = new List<Song>();
        private int createdByAccountId;

        public int GetPlaylistId()
        {
            return playlistId;
        }

        public void SetPlaylistId(int value)
        {
            playlistId = value;
        }

        public string GetTitle()
        {
            return title;
        }

        public void SetTitle(string value)
        {
            title = value;
        }

        public List<Song> GetSongs()
        {
            return songs;
        }

        public void AddSong(Song song)
        {
            songs.Add(song);
        }

        public void RemoveSong(Song song)
        {
            songs.Remove(song);
        }

        public int GetCreatedByAccountId()
        {
            return createdByAccountId;
        }

        public void SetCreatedByAccountId(int value)
        {
            createdByAccountId = value;
        }

        public void PrintPlaylistDetails()
        {
            Console.WriteLine($"Playlist Title: {GetTitle()}");
            foreach (var song in GetSongs())
            {
                Console.WriteLine($"Song: {song.GetName()}");
            }
        }
    }

}
