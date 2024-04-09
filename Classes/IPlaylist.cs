using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public interface IPlaylist
    {
        // Method to add a song to the playlist
        void AddToPlaylist(string song);

        // Method to remove a song from the playlist
        void RemoveFromPlaylist(string song);

        // Method to print the details of the playlist
        void PrintPlaylistDetails();
    }

}
