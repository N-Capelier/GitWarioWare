using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Soupe
{
    namespace EcraseMouche
    {
        /// <summary>
        /// Arthur Galland
        /// </summary>
        public class StainChoice : MonoBehaviour
        {

            [SerializeField]
            private List<Sprite> spritesOfJam;
            private int numberMax;
            private SpriteRenderer rend;

            // Start is called before the first frame update
            void Start()
            {
                rend = GetComponent<SpriteRenderer>();
                numberMax = Random.Range(0, spritesOfJam.Count);
                rend.sprite = spritesOfJam[numberMax];
            }

        }
    }
}


