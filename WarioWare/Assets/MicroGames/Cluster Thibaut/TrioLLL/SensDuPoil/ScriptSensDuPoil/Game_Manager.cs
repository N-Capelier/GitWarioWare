using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace LLL
{
    namespace SensDuPoil
    {
        public class Game_Manager : TimedBehaviour
        {
            public enum Catstate { IDLE, NEEDY, PET, HAPPY, ANGRY} //Les différents états du chat

            [Header("Cat Mood")] //Etat actuel du chat
            Catstate currentCatState;

            [Header("Compteur de patpat")]
            public int PetCounter = 0; //Compte le nombre actuel de PatPat

            [Header("Objectif")]
            public int PetObjective; //L'objectif caché de PatPat à atteindre et ne pas dépasser

            public GameObject Hand;
            public Sprite HandNormal;
            public Sprite HandPet;
            public Sprite HandAngry;
            public GameObject heartParticle;
            private List<GameObject> heartObjects;


            private SpriteRenderer srHand;
            private Animator anim;

            public bool canPet;

            IEnumerator PetTimer(float wait) //Permet au chat de ne pas repasser immédiatement dans l'état Needy (et donc d'éviter le gros spam)
            {
                yield return new WaitForSeconds(wait);
                canPet = true;          
            }

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                heartObjects = new List<GameObject>();
                currentCatState = Catstate.IDLE;
                canPet = true;
                anim = GetComponent<Animator>();
                srHand = Hand.GetComponent<SpriteRenderer>();
                switch (currentDifficulty) //Permet de gérer la difficulté; Le nombre de PatPat requis change à chaque fois. C'est un nombre aléatoire dans une plage déterminée.
                {
                    case Difficulty.EASY:
                        PetObjective = Random.Range(5, 7);
                        break;
                    case Difficulty.MEDIUM:
                        PetObjective = Random.Range(8, 10);
                        break;
                    case Difficulty.HARD:
                        PetObjective = Random.Range(11, 13);
                        break;
                }
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
            }

            //FixedUpdate is called on a fixed time.
            public void Update()
            {

                if (Input.GetButtonDown("A_Button") && (canPet == true) && (currentCatState != Catstate.ANGRY)) //Appuier sur le bouton A augmente le compteur de pat pat et déclenche l'état associé
                {
                    canPet = false;
                    PetCounter++;


                    if (PetCounter == PetObjective) //Détermine si l'objectif est atteint ou dépassé
                    {
                        currentCatState = Catstate.HAPPY;
                        StartCoroutine(PetTimer(0.1f));
                    }
                    else if (PetCounter > PetObjective)
                    {
                        currentCatState = Catstate.ANGRY;
                        for (int i = heartObjects.Count - 1; i >= 0; i--)
                        {
                            Destroy(heartObjects[i]);
                        }
                    }
                    else if (PetCounter < PetObjective)
                    {
                        heartObjects.Add(Instantiate(heartParticle));

                        if(PetCounter == 1)
                        {
                            currentCatState = Catstate.NEEDY;
                        }
                        else
                        {
                            currentCatState = Catstate.PET;
                            anim.Play("Chat_Pet", -1, 0f);
                        }
                        StartCoroutine(PetTimer(0.1f));
                    }
                    Debug.Log(PetCounter);
                }
                anim.SetInteger("State", (int)currentCatState);

                switch (currentCatState) //Permet de transitionner entre les différents états du chat
                {
                    case Catstate.IDLE:
                        srHand.sprite = HandNormal;
                        break;
                    case Catstate.NEEDY: //Le chat réclame des PatPat
                        srHand.sprite = HandNormal;
                        //Afficher les sprites du chat needy
                        break;

                    case Catstate.PET: //Le chat reçois des PatPat
                        srHand.sprite = HandPet;
                        //Afficher les sprites du pat pat
                        //Son de ronron
                        break;

                    case Catstate.HAPPY: //Le chat est heureux
                        //Son de chat content
                        break;

                    case Catstate.ANGRY: //Le chat a recu trop de PatPat
                        srHand.sprite = HandAngry;
                        //Son de chat pas content
                        break;
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 8)
                {
                    if (currentCatState == Catstate.HAPPY) //Si au dernier tick le chat est dans l'état heureux, c'est gagné
                    {
                        bool win = true;
                        Debug.Log("win");
                        Manager.Instance.Result(win);
                    }
                }
            }
        }
    }
}