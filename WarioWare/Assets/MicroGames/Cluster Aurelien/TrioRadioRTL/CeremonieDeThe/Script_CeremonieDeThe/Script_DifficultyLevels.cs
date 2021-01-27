using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

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
            public float difficultyMaxRotation;


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

                difficultyMaxRotation = teaCupManager.maxRotationDegree;


                //3- Niveau de difficulté
                switch (currentDifficulty)
                {

                    case Difficulty.EASY:

                        teaCupManager.mouvementTeaCupX = 0;

                        teaCupManager.maxRotationDegree = 0f;

                        teaPot.transform.position = new Vector3(-2f, 0f, 0.0f);

                        guirlande.transform.position = new Vector3(0f, -3.5f, 0f);

                        teaCup.transform.position = new Vector3(0f, -4.45f, 0f);

                        break;

                    case Difficulty.MEDIUM:

                        teaCupManager.mouvementTeaCupX = 10;

                        teaCupManager.maxRotationDegree = 2.5f;

                        teaPot.transform.position = new Vector3(-2f, 1.5f, 0.0f);

                        guirlande.transform.position = new Vector3(0f, -3.5f, 0f);

                        teaCup.transform.position = new Vector3(-2f, -4.45f, 0f);

                        break;

                    case Difficulty.HARD:

                        teaCupManager.mouvementTeaCupX = 10;

                        teaCupManager.maxRotationDegree = 2.5f;

                        teaPot.transform.position = new Vector3(-2f, 2.75f, 0.0f);

                        guirlande.transform.position = new Vector3(0f, -1.5f, 0f);
                        
                        teaCup.transform.position = new Vector3(-2f, -4.45f, 0f);

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