using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

    namespace Soupe
{
    namespace EcraseMouche
    {
        /// <summary>
        /// Arthur Galland
        /// </summary>
        public class SwatterBehevior : MonoBehaviour
        {
            public static bool flyIsDead = false;
            private bool flyIsUnder = false;
            private bool canSmash = true;
            private GameObject flyObject;
            private SpriteRenderer sprite;
            [SerializeField]
            private GameObject flySmashed;
            [SerializeField]
            private GameObject buttonA;

            // Start is called before the first frame update
            void Start()
            {
                sprite = GetComponent<SpriteRenderer>();
            }

            // Update is called once per frame
            void Update()
            {
                if (flyIsDead)
                {
                    buttonA.SetActive(false);
                }

                //if the fly is under the swatter press a button and slam
                if (Input.GetButtonDown("A_Button") && canSmash)
                {
                    StartCoroutine(SwatterAnimation());
                   //SoundManagerMouche.Instance.sfxSound[3].Play();
                    if (flyIsUnder || flyIsDead)
                    {
                        //sound when swatter hit the fly
                        SoundManagerMouche.Instance.sfxSound[1].Play();
                        if (!flyIsDead)
                        {
                            Instantiate(flySmashed, flyObject.transform);
                            flyIsDead = true; //success condition
                            flyObject.GetComponent<SpriteRenderer>().enabled = false;
                        }
                    }
                    else
                    {
                        //sound when swatter hit the jam
                        SoundManagerMouche.Instance.sfxSound[2].Play();
                        sprite.sortingOrder = 2; //the fly sprite is above the swatter 
                        canSmash = false; //if the player don't time the smash right he can't smash again
                    }

                }
            }

            void OnTriggerEnter2D(Collider2D col) //the fly is under the swatter
            {
                if (canSmash)
                {
                    buttonA.SetActive(true);
                }
                flyIsUnder = true;
                flyObject = col.gameObject;
            }

            void OnTriggerExit2D(Collider2D collision) //the fly is no longer under the swatter
            {
                buttonA.SetActive(false);
                flyIsUnder = false;
            }

            IEnumerator SwatterAnimation()
            {
                transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f);

                if (flyIsUnder)
                {
                    yield return new WaitForSeconds(0.1f);
                    transform.DOScale(new Vector3(1, 1, 1), 0.1f);
                }
                else yield return null;
            }
        }
    }
}

