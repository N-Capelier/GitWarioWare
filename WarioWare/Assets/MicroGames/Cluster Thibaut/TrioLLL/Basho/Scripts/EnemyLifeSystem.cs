using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;
using UnityEngine.UI;

namespace TrioLLL
{
    namespace Basho
    {
        /// <summary>
        /// Louis Vitrant
        /// </summary>
        public class EnemyLifeSystem : TimedBehaviour
        {
            [HideInInspector]public float enemyLife;
            [HideInInspector] public float currentEnemyLife;
            public bool victory = false;
            private AudioSource source;
            public AudioClip victoryClip;
            public AudioClip failClip;
            public Image healthBar;
            public bool gameFinished;
            public GraphManager graph;
            public override void Start()
            {
                source = GetComponent<AudioSource>();
                gameFinished = false;
                base.Start();

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        enemyLife = 25;
                        break;
                    case Difficulty.MEDIUM:
                        enemyLife = 35;
                        break;
                    case Difficulty.HARD:
                        enemyLife = 35;
                        break;
                }

                currentEnemyLife = enemyLife;
                graph.bossLife = currentEnemyLife;
            }

            public void Damage()
            {
                currentEnemyLife--;
                if (currentEnemyLife <= 0)
                {
                    victory = true;
                }
            }

            public void HealthRegain()
            {
                currentEnemyLife =+ 0.1f;
            }

            public override void TimedUpdate()
            {
                base.TimedUpdate();
                if (Tick == 7.2)
                {
                    gameFinished = true;
                }
                if (Tick == 8)
                {
                    Manager.Instance.Result(victory);
                }
            }

            bool endSoundPlayed = false;

            public void Update()
            {
                healthBar.fillAmount = currentEnemyLife / enemyLife;
                if (gameFinished && !endSoundPlayed)
                {
                    endSoundPlayed = true;
                    if (victory)
                    {
                        source.PlayOneShot(victoryClip);
                    }
                    else
                    {
                        source.PlayOneShot(failClip);
                    }
                }
            }
        }
    }
}
