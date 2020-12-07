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

        // Start is called before the first frame update
        void Start()
        {
            //Initialize Connections
            ClearConnections();
            GetNeighbors();
            playerAvatar.transform.position = playerIsland.islandButton.transform.position;
            playerAvatar.transform.position += new Vector3(0, 15, 0);
            //playerIsland.islandButton.Select();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void ClearConnections()
        {
            for(int i = 0; i < islands.Length; i++)
            {
                islands[i].islandButton.interactable = false;
            }
        }

        private void GetNeighbors()
        {
            playerIsland.islandButton.interactable = true;
            for (int i = 0; i < playerIsland.neighbors.Length; i++)
            {
                playerIsland.neighbors[i].islandButton.interactable = true;
            }
            playerIsland.islandButton.Select();
        }

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

