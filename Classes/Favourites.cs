/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to store the details about user favourites, it was created based on the requirements spec but I ran out of time to fully implement it.
*/

/*
namespace Betawave.Classes
{

    public class Favourites : BasePlaylist
    {
        private List<Song> favouriteSongs = new List<Song>();

        public Favourites() : base()
        {
            // Load favourites from the database
        }

        public void AddToFavourites(Song song)
        {
            if (!favouriteSongs.Contains(song))
            {
                favouriteSongs.Add(song);
                // Add to database
            }
        }

        public bool RemoveFromFavourites(Song song)
        {
            bool removed = favouriteSongs.Remove(song);
            if (removed)
            {
                // Remove from database
            }
            return removed;
        }

        public void PrintFavourites()
        {
            Console.WriteLine("Favourite Songs:");
            foreach (Song song in favouriteSongs)
            {
                Console.WriteLine($"Song: {song.GetName()}");
            }
        }
    }

}
*/