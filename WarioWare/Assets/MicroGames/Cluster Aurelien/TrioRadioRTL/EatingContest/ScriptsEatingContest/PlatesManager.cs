using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DEUTSCHMANN Lucas
/// </summary>

namespace TrioRadioRTL
{
    namespace EatingContest
    {

        public class PlatesManager : MonoBehaviour
        {
            bool isRotten;
            public PlayerController playerController;

            [Header("Graphs")]

            SpriteRenderer plate;
            //public Sprite goodPlateSpriteEmpty;
            public List<Sprite> Plates = new List<Sprite>();
            public Sprite rottenPlateSprite;
            

            // Start is called before the first frame update
            void Start()
            {
                
                plate = gameObject.GetComponent<SpriteRenderer>();
                
                
            }

            // Update is called once per frame
            void Update()
            {
                isRotten = playerController.rottenPlate;

                if (isRotten)
                {
                    plate.sprite = rottenPlateSprite;
                }
                else
                {
                    ChangeSpriteWithChomps(playerController.chomp, playerController.numberOfChomps);
                }
                
                
            }

            void ChangeSpriteWithChomps(int chomps, int totalChomps)
            {
                if ((totalChomps - chomps) >= 0)
                {
                    plate.sprite = Plates[totalChomps - chomps];
                }
                
            }
        }
    }
}
