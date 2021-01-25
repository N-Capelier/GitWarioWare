using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioLLL
{
    namespace Basho
    {
        /// <summary>
        /// Louis Vitrant
        /// </summary>
        public class GraphManager : TimedBehaviour
        {
            public GameObject backgroundLevel1;
            public GameObject backgroundLevel2;
            public GameObject backgroundLevel3;
            public GameObject bossLevel1;
            public GameObject bossLevel2;
            public GameObject bossLevel3;
            [HideInInspector] public SpriteRenderer currentBoss;
            public GameObject bossLevel1Defeated;
            public GameObject bossLevel2Defeated;
            public GameObject bossLevel3Defeated;
            public GameObject bossLevel1Hand;
            public GameObject bossLevel2Hand;
            public GameObject bossLevel3Hand;
            public ParticleSystem confusedEnemy;
            public EnemyLifeSystem enemyLifeSys;
            [HideInInspector] public bool endOfTheGame;
            [HideInInspector] public float bossLife;
            public override void Start()
            {
                bossLife = enemyLifeSys.currentEnemyLife;
                base.Start();
                endOfTheGame = false;

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        backgroundLevel1.SetActive(true);
                        bossLevel1.SetActive(true);
                        currentBoss = bossLevel1.GetComponent<SpriteRenderer>();
                        break;
                    case Difficulty.MEDIUM:
                        backgroundLevel2.SetActive(true);
                        bossLevel2.SetActive(true);
                        currentBoss = bossLevel2.GetComponent<SpriteRenderer>();
                        break;
                    case Difficulty.HARD:
                        backgroundLevel3.SetActive(true);
                        bossLevel3.SetActive(true);
                        currentBoss = bossLevel3.GetComponent<SpriteRenderer>();
                        break;
                }
            }
            public override void TimedUpdate()
            {
                base.TimedUpdate();
                bossLife = enemyLifeSys.currentEnemyLife;

                if (bossLife <= 0)
                {
                    endOfTheGame = true;
                    confusedEnemy.Play();
                    switch (currentDifficulty)
                    {
                        case Difficulty.EASY:
                            bossLevel1.SetActive(false);
                            bossLevel1Defeated.SetActive(true);
                            break;
                        case Difficulty.MEDIUM:
                            bossLevel2.SetActive(false);
                            bossLevel2Defeated.SetActive(true);
                            break;
                        case Difficulty.HARD:
                            bossLevel3.SetActive(false);
                            bossLevel3Defeated.SetActive(true);
                            break;
                    }
                }
                else if (bossLife > 0)
                {
                    if(Tick > 7)
                    {
                        endOfTheGame = true;
                        switch (currentDifficulty)
                        {
                            case Difficulty.EASY:
                                bossLevel1.SetActive(false);
                                bossLevel1Hand.SetActive(true);
                                break;
                            case Difficulty.MEDIUM:
                                bossLevel2.SetActive(false);
                                bossLevel2Hand.SetActive(true);
                                break;
                            case Difficulty.HARD:
                                bossLevel3.SetActive(false);
                                bossLevel3Hand.SetActive(true);
                                break;
                        }
                    }
                }
            }
        }
    }
}
