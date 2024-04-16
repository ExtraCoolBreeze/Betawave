using System;
using System.Collections.Generic;

namespace Betawave.Classes
{

    // Then an account is created always generate 2 playlists, one in this class that is a queue object with Queue with the queue indentifier set to yes to match the database,
    // and a list called favourite in the favourites class with a unique identifier as yes again to match up with the database

    //INSERT INTO playlist (playlist_id, title, queue, favourite, account_id)
    //VALUES('<playlist_id>', '<title>', '<queue>', '<favourite>', '<account_id>');

    
    public class Queue : BasePlaylist
    {
        private bool checkQueued = false;
        private Queue<PlayerState> playerStates; // Assuming PlayerState is a class or struct you define

        public Queue() : base()
        {
            playerStates = new Queue<PlayerState>();
        }

        public string CreatePlaylist(string userInput)
        {
            SetTitle(userInput);
            return userInput;
        }

        public override void AddToPlaylist(Playlist_Track track)
        {
            base.AddToPlaylist(track);
        }

        public override void RemoveFromPlaylist(Playlist_Track track)
        {
            base.RemoveFromPlaylist(track);
        }

        public override void PrintPlaylistDetails()
        {
            base.PrintPlaylistDetails();
        }

        public void SetCheckQueued(string userInput)
        {
            if (bool.TryParse(userInput, out bool result))
            {
                checkQueued = result;
            }
            else
            {
                Console.WriteLine("Invalid input for checkQueued.");
            }
        }

        public bool GetCheckQueued()
        {
            return checkQueued;
        }

        // Enqueue a new player state
        public void EnqueuePlayerState(PlayerState state)
        {
            playerStates.Enqueue(state);
        }

        // Dequeue the next player state
        public PlayerState DequeuePlayerState()
        {
            return playerStates.Count > 0 ? playerStates.Dequeue() : default(PlayerState);
        }

        // Peek at the next player state without removing it
        public PlayerState PeekNextPlayerState()
        {
            return playerStates.Count > 0 ? playerStates.Peek() : default(PlayerState);
        }
    }

    // Define the PlayerState class or struct according to your needs
    public class PlayerState
    {
        // Include properties like CurrentTrack, IsPlaying, Volume, etc.
    }
}
