using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class BossLifeManager : Singleton<BossLifeManager>
    {
        public BossUI bossUI;
        public static int currentLife = 100;
        public static int maxLife = 100;
        private void Start()
        {
            CreateSingleton();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                TakeDamage(10);
        }
        /// <summary>
        /// Apply damage to boss, if the boss life is equal 0, return true, else return false
        /// </summary>
        /// <param name="damageValue"></param>
        /// <returns></returns>
        public bool TakeDamage (int damageValue)
        {
            currentLife -= Mathf.Clamp(damageValue, 0, currentLife);

            bossUI.UpdateHp(maxLife,currentLife);

            if (currentLife == 0)
                return true;
            else
                return false;

        }

    }

}
