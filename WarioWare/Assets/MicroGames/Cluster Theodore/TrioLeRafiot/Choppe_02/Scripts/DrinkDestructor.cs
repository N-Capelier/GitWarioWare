﻿using UnityEngine;

namespace LeRafiot
{
    namespace Choppe
    {
        public class DrinkDestructor : MonoBehaviour
        {
            public CatchSystem catchScript;

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.CompareTag("Enemy2") && !catchScript.catchedBadDrink)
                {
                    catchScript.drinkExit = true;
                    catchScript.canCatch = false;
                    DrinkManager.Instance.canSpawn = false;
                    Destroy(collision.gameObject);
                    //Manager.Instance.Result(false);
                    //SoundManagerChoppe.Instance.sfxSound[1].Play();
                }
                else
                {
                    Destroy(collision.gameObject);
                }
            }

        }
    }   
}
