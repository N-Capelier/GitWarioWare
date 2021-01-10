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
        public class CoinController : MonoBehaviour
        {
            [SerializeField] GameObject hiddenCoin;

            public bool hideCoins;



            public void Update()
            {
                if (hideCoins)
                {
                    Instantiate(hiddenCoin, transform.position, transform.rotation);
                }
            }
        }

    }
}
