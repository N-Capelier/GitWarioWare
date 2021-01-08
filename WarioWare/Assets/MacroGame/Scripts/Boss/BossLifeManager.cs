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
        public  int maxLife = 100;
        private void Awake()
        {
            CreateSingleton();
            currentLife = maxLife;
        }


        private void Update()
        {
            /* uncoment this to test the life ui
            if (Input.GetKeyDown(KeyCode.Space))
                TakeDamage(10);*/
        }
        /// <summary>
        /// Apply damage to boss, if the boss life is equal 0, return true, else return false
        /// </summary>
        /// <param name="damageValue"></param>
        /// <returns></returns>
        public bool TakeDamage (int damageValue, int initialLife = 100, bool isBoss = false)
        {
            currentLife -= Mathf.Clamp(damageValue, 0, currentLife);

            if(!isBoss)
            bossUI.UpdateHp(maxLife,currentLife);
            else
            {
                lifeRect.transform.DOScaleX(0.2f * currentLife / initialLife, 60 / (float)Manager.Instance.bpm);
            }

            if (currentLife == 0)
                return true;
            else
                return false;

        }

    }

}
