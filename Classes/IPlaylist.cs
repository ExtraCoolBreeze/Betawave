/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to ensure specific functionality of playlists */

//This interface is complete. Method names may need changing

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
