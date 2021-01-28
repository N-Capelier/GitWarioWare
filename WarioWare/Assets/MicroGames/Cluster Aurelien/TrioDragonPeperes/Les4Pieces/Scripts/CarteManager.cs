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
        public class CarteManager : MonoBehaviour
        {
            public GameObject spotCarte;

            public List<GameObject> cartesEasy;
            public List<GameObject> cartesMedium;
            public List<GameObject> cartesHard;

            public bool weWantCoin1;
            public bool weWantCoin2;
            public bool weWantCoin3;
            public bool weWantCoin4;

            public Animator[] anim;


            public void SpawnRandomCarteEasy()
            {
                int count = cartesEasy.Count;
                if (count == 0)
                {
                    return;
                }

                int i = Random.Range(0, count - 1);
                GameObject go = cartesEasy[i];
                cartesEasy.Remove(go);

                Instantiate(go, spotCarte.transform);

                for (int t = 0; t < anim.Length; t++)
                {
                    anim[t].enabled = true;
                }
            }

            public void SpawnRandomCarteMedium()
            {
                int count = cartesMedium.Count;
                if (count == 0)
                {
                    return;
                }

                int i = Random.Range(0, count - 1);
                GameObject go = cartesMedium[i];
                cartesMedium.Remove(go);

                Instantiate(go, spotCarte.transform);

                for (int t = 0; t < anim.Length; t++)
                {
                    anim[t].enabled = true;
                }
            }

            public void SpawnRandomCarteHard()
            {
                int count = cartesHard.Count;
                if (count == 0)
                {
                    return;
                }

                int i = Random.Range(0, count - 1);
                GameObject go = cartesHard[i];
                cartesHard.Remove(go);

                Instantiate(go, spotCarte.transform);

                for (int t = 0; t < anim.Length; t++)
                {
                    anim[t].enabled = true;
                }
            }


        }
    }
}
