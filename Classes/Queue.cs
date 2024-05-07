/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to store and manage play queues based on the requirements spec. However the requirement was changed as I ran out of time to implement and is now handled differently  */


/*namespace Betawave.Classes
{

    public class Queue : BasePlaylist
    {
        private Queue<Song> songQueue = new Queue<Song>();

        public Queue() : base()
        {
        }

        public void EnqueueSong(Song song)
        {
            songQueue.Enqueue(song);
        }

        public Song DequeueSong()
        {
            if (songQueue.Count > 0)
            {
                return songQueue.Dequeue();
            }
            else
            {
                return null;
            }
        }


        public void ClearQueue()
        {
            songQueue.Clear();
        }

        public void PrintQueue()
        {
            Console.WriteLine($"Current Queue: {songQueue.Count} songs.");
            foreach (Song song in songQueue)
            {
                Console.WriteLine($"Song: {song.GetName()}");
            }
        }
    }

}*/