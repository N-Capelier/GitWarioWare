using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dragons_Peperes
{
    namespace Les4Pieces
    {
        /// <summary>
        /// Mael Ricou
        /// </summary>
        public class CoinManager : MonoBehaviour
        {
            public List<GameObject> coinsEasy = new List<GameObject>();

            public List<GameObject> spotsEasy = new List<GameObject>();


            public List<GameObject> coinsMedium = new List<GameObject>();

            public List<GameObject> spotsMedium = new List<GameObject>();

            public List<GameObject> coinsHard = new List<GameObject>();

            public List<GameObject> spotsHard = new List<GameObject>();


            //public List<GameObject> hiddenCoin = new List<GameObject>();

            public void SpawnCoinsEasy()
            {
                for (int i = 0; i < spotsEasy.Count; i++)
                {
                    int rand = Random.Range(0, coinsEasy.Count);
                    Instantiate(coinsEasy[rand], spotsEasy[i].transform.position, Quaternion.identity, spotsEasy[i].transform);
                    coinsEasy.RemoveAt(rand);
                }
            }

            public void SpawnCoinsMedium()
            {

                for (int i = 0; i < spotsMedium.Count; i++)
                {
                    int rand = Random.Range(0, coinsMedium.Count);
                    Instantiate(coinsMedium[rand], spotsMedium[i].transform.position, Quaternion.identity, spotsMedium[i].transform);
                    coinsMedium.RemoveAt(rand);
                }
            }

            public void SpawnCoinsHard()
            {

                for (int i = 0; i < spotsMedium.Count; i++)
                {
                    int rand = Random.Range(0, coinsHard.Count);
                    Instantiate(coinsHard[rand], spotsMedium[i].transform.position, Quaternion.identity, spotsHard[i].transform);
                    coinsHard.RemoveAt(rand);
                }
            }

        }
    }
}
