namespace Betawave.Classes
{

    // When an account is created always generate 2 playlists, one in this class that is a queue object with Queue with the queue indentifier set to yes to match the database,
    // and a list called favourite in the favourites class with a unique identifier as yes again to match up with the database

    //INSERT INTO playlist (playlist_id, title, queue, favourite, account_id)
    //VALUES('<playlist_id>', '<title>', '<queue>', '<favourite>', '<account_id>');


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

}