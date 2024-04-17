using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Betawave.Classes
{
    public class Favourites : BasePlaylist
    {
        private string _favourite;

        public Favourites() : base()
        {
            _favourite = "";
        }

        public string CreatePlaylist(string userInput)
        {
            SetTitle(userInput);
            return userInput;
        }

        public void SetFavourite(string userInput)
        {
            _favourite = userInput;
        }

        public string GetFavourite()
        {
            return _favourite;
        }

        public new void PrintPlaylistDetails()
        {
            Console.WriteLine($"Favourites Playlist Title: {GetTitle()}, Favourite: {_favourite}");
            foreach (var track in GetTracks())
            {
                track.PrintPlaylistTrack();
            }
        }
    }
}
