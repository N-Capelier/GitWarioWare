﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Caps;
using UnityEngine.UI;

namespace Boss
{
    public class BossLifeManager : Singleton<BossLifeManager>
    {
        public BossUI bossUI;
        public static int currentLife = 100;
        public  int maxLife = 150;
        public BossTransitionUI bossTranstion;
        public int miniBossLife = 0;
        public Image bossHPFillbar;
        private void Awake()
        {
            CreateSingleton();
            //set up value from debug
            maxLife = (int)DebugToolManager.Instance.ChangeVariableValue("maxLife");
            currentLife = maxLife;
        }

        public void InitialLife()
        {
            bossTranstion.InitiateBossLife(maxLife,currentLife, false);
        }
        public void InitialLife(int life)
        {
            miniBossLife = life;
            bossTranstion.InitiateBossLife(life, life, true);
        }

        /// <summary>
        /// Apply damage to boss, if the boss life is equal 0, return true, else return false
        /// </summary>
        /// <param name="damageValue"></param>
        /// <returns></returns>
        public bool TakeDamage (int damageValue, int initialLife = 150, bool isBoss = false, bool isMiniBoss = false)
        {
            bossHPFillbar.fillAmount = currentLife / initialLife;
            if (!isBoss)
            {

                StartCoroutine(bossTranstion.BossTakeDamage(initialLife, currentLife));
            }                
            else
            {
                if (isMiniBoss)
                {
                    miniBossLife -= Mathf.Clamp(damageValue, 0, miniBossLife);
                    StartCoroutine(bossTranstion.BossTakeDamage(initialLife, miniBossLife));                    
                }
                else
                {
                    currentLife -= Mathf.Clamp(damageValue, 0, currentLife);
                    StartCoroutine(bossTranstion.BossTakeDamage(initialLife, currentLife));
                }
                
            }

            if (currentLife == 0 || miniBossLife == 0)
                return true;
            else
                return false;

        }

    }

}
