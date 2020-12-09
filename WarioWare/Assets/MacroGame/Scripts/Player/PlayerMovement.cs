using UnityEngine;
using Islands;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public GameObject playerAvatar;
        public Island[] islands;
        public Island playerIsland;

        void Start()
        {
            //Initialize Connections
            islands = IslandCreator.Instance.islands;
            ClearConnections();
            GetNeighbors();
            playerAvatar.transform.position = playerIsland.button.transform.position;
            playerAvatar.transform.position += new Vector3(0, 15, 0);
        }

        /// <summary>
        /// Resets all Island UI buttons of the zone to not interactable.
        /// </summary>
        private void ClearConnections()
        {
            for(int i = 0; i < islands.Length; i++)
            {
                islands[i].button.interactable = false;
            }
        }

        /// <summary>
        /// Get the player's current island neighbors and set their UI to interactable.
        /// </summary>
        private void GetNeighbors()
        {
            playerIsland.button.interactable = true;
            for (int i = 0; i < playerIsland.neighbours.Length; i++)
            {
                playerIsland.neighbours[i].button.interactable = true;
            }
            playerIsland.button.Select();
        }

        /// <summary>
        /// Move the player from his current island to the target island. Moving costs 1 food, if 0 food then it costs 1 hp.
        /// </summary>
        /// <param name="targetIsland">Which island is the player going to.</param>
        public void Move(Island targetIsland)
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

                playerAvatar.transform.position = targetIsland.button.transform.position;
                playerAvatar.transform.position += new Vector3(0, 15, 0);
            }
        }


        /// <summary>
        /// Show the selected island's UI informations.
        /// </summary>
        /// <param name="targetIsland">Which island is the player selecting.</param>
        public void ShowSelectedIslandInfo(Island targetIsland)
        {
            if (targetIsland != playerIsland)
            {
                Debug.Log(targetIsland.gameObject.name + " has " + targetIsland.neighbours.Length + " neighbor(s).");
                //show UI here
            }
        }
    }
}

