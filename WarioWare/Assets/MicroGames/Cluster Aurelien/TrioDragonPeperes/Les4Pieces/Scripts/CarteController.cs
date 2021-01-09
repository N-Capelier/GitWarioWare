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

        public class CarteController : MonoBehaviour
        {
            [SerializeField] bool isCarte1;
            [SerializeField] bool isCarte2;
            [SerializeField] bool isCarte3;
            [SerializeField] bool isCarte4;


            private CarteManager carteManager;

            public bool carteIsHere;

            private void Start()
            {
                carteManager = FindObjectOfType<CarteManager>();

                carteIsHere = true;

                #region We want that coin

                if (isCarte1)
                {
                    carteManager.weWantCoin1 = true;
                    Debug.Log("on veut la piece 1");
                }

                if (isCarte2)
                {
                    carteManager.weWantCoin2 = true;
                    Debug.Log("on veut la piece 2");
                }

                if (isCarte3)
                {
                    carteManager.weWantCoin3 = true;
                    Debug.Log("on veut la piece 3");
                }

                if (isCarte4)
                {
                    carteManager.weWantCoin4 = true;
                    Debug.Log("on veut la piece 4");
                }
                #endregion
            }

        }
    }
}
