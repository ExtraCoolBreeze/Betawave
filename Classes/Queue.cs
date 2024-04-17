using System;
using System.Collections.Generic;

namespace Betawave.Classes
{

    // When an account is created always generate 2 playlists, one in this class that is a queue object with Queue with the queue indentifier set to yes to match the database,
    // and a list called favourite in the favourites class with a unique identifier as yes again to match up with the database

    //INSERT INTO playlist (playlist_id, title, queue, favourite, account_id)
    //VALUES('<playlist_id>', '<title>', '<queue>', '<favourite>', '<account_id>');


    public class Queue : BasePlaylist
    {
        private bool checkQueued = false;

        public Queue() : base()
        {
         
        }

        public string CreatePlaylist(string userInput)
        {
            SetTitle(userInput);
            return userInput;
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
    }
}