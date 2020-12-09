using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public GameObject playerAvatar;
        public TempIslands[] islands;
        public TempIslands playerIsland;

        void Start()
        {
            //Initialize Connections
            ClearConnections();
            GetNeighbors();
            playerAvatar.transform.position = playerIsland.islandButton.transform.position;
            playerAvatar.transform.position += new Vector3(0, 15, 0);
        }

        /// <summary>
        /// Resets all Island UI buttons of the zone to not interactable.
        /// </summary>
        private void ClearConnections()
        {
            for(int i = 0; i < islands.Length; i++)
            {
                islands[i].islandButton.interactable = false;
            }
        }

        /// <summary>
        /// Get the player's current island neighbors and set their UI to interactable.
        /// </summary>
        private void GetNeighbors()
        {
            playerIsland.islandButton.interactable = true;
            for (int i = 0; i < playerIsland.neighbors.Length; i++)
            {
                playerIsland.neighbors[i].islandButton.interactable = true;
            }
            playerIsland.islandButton.Select();
        }

        /// <summary>
        /// Move the player from his current island to the target island. Moving costs 1 food, if 0 food then it costs 1 hp.
        /// </summary>
        /// <param name="targetIsland">Which island is the player going to.</param>
        public void Move(TempIslands targetIsland)
        {
            if(targetIsland != playerIsland)
            {
                playerIsland = targetIsland;
                ClearConnections();
                GetNeighbors();
                
                if (PlayerManager.Instance.food>0)
                {
                    PlayerManager.Instance.GainFood(-1);
                }
                else
                {
                    PlayerManager.Instance.TakeDamage(1);
                }

                //Lancer le cap

                playerAvatar.transform.position = targetIsland.islandButton.transform.position;
                playerAvatar.transform.position += new Vector3(0, 15, 0);
            }
        }


        /// <summary>
        /// Show the selected island's UI informations.
        /// </summary>
        /// <param name="targetIsland">Which island is the player selecting.</param>
        public void ShowSelectedIslandInfo(TempIslands targetIsland)
        {
            if (targetIsland != playerIsland)
            {
                Debug.Log(targetIsland.gameObject.name + " has " + targetIsland.neighbors.Length + " neighbor(s).");
                //show UI here
            }
        }
    }
}

