using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Caps;
namespace Boss
{
    public class BossLifeManager : Singleton<BossLifeManager>
    {
        public Transform lifeRect;
        public BossUI bossUI;
        public static int currentLife = 100;
        public  int maxLife = 150;
        public BossTransitionUI bossTranstion;
        private void Awake()
        {
            CreateSingleton();
            //set up value from debug
            maxLife = (int)DebugToolManager.Instance.ChangeVariableValue("maxLife");
            currentLife = maxLife;
        }

        public void InitialLife()
        {
            bossTranstion.InitiateBossLife(maxLife,currentLife);
        }
        public void InitialLife(int life)
        {
            bossTranstion.InitiateBossLife(life, life);
        }

        /// <summary>
        /// Apply damage to boss, if the boss life is equal 0, return true, else return false
        /// </summary>
        /// <param name="damageValue"></param>
        /// <returns></returns>
        public bool TakeDamage (int damageValue, int initialLife = 150, bool isBoss = false)
        {
            currentLife -= Mathf.Clamp(damageValue, 0, currentLife);

            if (!isBoss)
            {
                StartCoroutine(bossTranstion.BossTakeDamage(initialLife, currentLife));
            }                
            else
            {
                StartCoroutine(bossTranstion.BossTakeDamage(initialLife,currentLife));
            }

            if (currentLife == 0)
                return true;
            else
                return false;

        }

    }

}
