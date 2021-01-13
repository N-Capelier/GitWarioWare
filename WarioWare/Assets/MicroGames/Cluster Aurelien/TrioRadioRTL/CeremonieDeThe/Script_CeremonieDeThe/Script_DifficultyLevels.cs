using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadioRTL
{
    namespace CeremonieDeThe
    {

        /// <summary>
        /// Riwan HERVOUET
        /// </summary>

        public class Script_DifficultyLevels : TimedBehaviour
        {

            //1- Création des variables et récupération des objets
            //1.1- Game Objects
            public GameObject teaCup;
            public GameObject teaPot;
            public GameObject guirlande;

            //1.2- Int
            public int difficultyTeaPotSpeed;
            public int difficultyMouvementTeaCup;


            public override void Start()
            {
                base.Start(); //Do not erase this line!

                //2- écupération des données
                //2.1- Tea Pot
                Script_TeaPotController teaPotController = teaPot.GetComponent<Script_TeaPotController>();
                difficultyTeaPotSpeed = teaPotController.teaPotSpeed;

                //2.2- Tea Cup
                Script_TeaCup teaCupManager = teaCup.GetComponent<Script_TeaCup>();
                difficultyMouvementTeaCup = teaCupManager.mouvementTeaCupX;


                //3- Niveau de difficulté
                switch (currentDifficulty)
                {

                    case Difficulty.EASY:

                        teaPotController.teaPotSpeed = 10;

                        teaCupManager.mouvementTeaCupX = 2;

                        teaPot.transform.position = new Vector3(-2f, 0f, 0.0f);

                        guirlande.transform.position = new Vector3(0f, -4.2f, 0f);

                        break;

                    case Difficulty.MEDIUM:

                        teaPotController.teaPotSpeed = 5;

                        teaCupManager.mouvementTeaCupX = 3;

                        teaPot.transform.position = new Vector3(-2f, 1.5f, 0.0f);

                        guirlande.transform.position = new Vector3(0f, -2.7f, 0f);

                        break;

                    case Difficulty.HARD:

                        teaPotController.teaPotSpeed = 20;

                        teaCupManager.mouvementTeaCupX = 4;

                        teaPot.transform.position = new Vector3(-2f, 3f, 0.0f);

                        guirlande.transform.position = new Vector3(0f, -1.4f, 0f);

                        break;

                }
            }


            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }
            public override void TimedUpdate()
            {

            }
        }
    }
}