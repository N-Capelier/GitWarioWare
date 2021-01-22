using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioBrigantin
{
    namespace CouteauxTires
    {
        public class ACouteauxTiré_Manager : TimedBehaviour
        {
            #region Variables
            static public ACouteauxTiré_Manager instance;
            public ACouteauTires_SoundManager soundManager;

            [Header("Enemy Setup Fields")]

            int numberOfEnemies; //Serialize field when testing
            int ammo;
            int misses = 0;
            [HideInInspector] public List<Enemy> enemiesAlive = new List<Enemy>();
            [HideInInspector] public List<Enemy> enemiesKilled = new List<Enemy>();
            bool resultSent = false;
            [SerializeField] AmmoCounter timeTick;
            [SerializeField] AmmoCounter ammoCount;

            public GameObject baseEnemy;
            public GameObject superEnemy;
            [SerializeField] GameObject[] spawnSets;
            public GameObject spawnSetAnchor;
            GameObject chosenSpawnSet;
            EnemySpawnerMag chosenSpawner;

            bool doSuperEnemySpawning;
            [Range(0, 100)]
            [SerializeField] int superEnemyChance = 4; //Probability of Super Enemy, 0 means will be sure to not spawn

            [Header("CrossHair BPM balance")]
            [Range(5f, 15f)]
            [SerializeField] float slowSpeed = 6.4f;
            [Range(5f, 15f)]
            [SerializeField] float medSpeed = 8f;
            [Range(5f, 15f)]
            [SerializeField] float fastSpeed = 9.7f;
            [Range(5f, 15f)]
            [SerializeField] float superSpeed = 12.2f;

            [Header("For End Screen")]
            [SerializeField] GameObject gameScene;
            [SerializeField] GameObject winScene;
            [SerializeField] GameObject loseScene;
            bool onEndScreen = false;
            bool winCon;
            #endregion

            private void Awake()
            {
                if (instance == null)
                    instance = this;
                else
                    Destroy(gameObject);
            }

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                
                //speed and music
                switch (bpm)
                {
                    case 60:
                        CrosshairController.instance.movementSpeed = slowSpeed;
                        soundManager.Play("CouteauxTires_60BPM");
                        break;

                    case 80:
                        CrosshairController.instance.movementSpeed = medSpeed;
                        soundManager.Play("CouteauxTires_80BPM");
                        break;

                    case 100:
                        CrosshairController.instance.movementSpeed = fastSpeed;
                        soundManager.Play("CouteauxTires_100BPM");
                        break;

                    case 120:
                        CrosshairController.instance.movementSpeed = superSpeed; 
                        soundManager.Play("CouteauxTires_120BPM");
                        break;

                    default:
                        break;
                }
                Debug.Log("crh Speed: " + CrosshairController.instance.movementSpeed);
                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        numberOfEnemies = 3;
                        //ammo = numberOfEnemies;
                        break;

                    case Difficulty.MEDIUM:
                        numberOfEnemies = 4;
                        doSuperEnemySpawning = DecideSuperEnemySpawn();

                        //if (doSuperEnemySpawning)
                        //    ammo = numberOfEnemies + 1;
                        //else
                        //    ammo = numberOfEnemies;

                        break;

                    case Difficulty.HARD:
                        numberOfEnemies = 5;
                        doSuperEnemySpawning = DecideSuperEnemySpawn();

                        //if (doSuperEnemySpawning)
                        //    ammo = numberOfEnemies + 1;
                        //else
                        //    ammo = numberOfEnemies;

                        break;
                }
                
                ammo = numberOfEnemies + 1;
                if (doSuperEnemySpawning)
                    ammo++;

                ammoCount.InitAmmoCounter(ammo);
                InstantiateSpawner(spawnSets[(int)currentDifficulty]);
                timeTick.InitAmmoCounter(8);
                Debug.Log("Ammo left: " + ammo);
                Debug.Log(Tick);
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
                //if (ammo == 0 && enemiesKilled.Count == numberOfEnemies)
                //{
                //    Manager.Instance.Result(true);
                //}
                if (/*ammo == 0 && */!onEndScreen)
                {
                    if (misses >= 2)
                    {
                        gameScene.SetActive(false);
                        ammoCount.gameObject.SetActive(false);
                        loseScene.SetActive(true);
                        if (doSuperEnemySpawning)
                            soundManager.Play("SuperEnemySnicker");
                        soundManager.Play("PistolHammer");

                        winCon = false;
                        onEndScreen = true;
                    }
                    else if (enemiesKilled.Count == numberOfEnemies)
                    {
                        gameScene.SetActive(false);
                        ammoCount.gameObject.SetActive(false);
                        winScene.SetActive(true);
                        soundManager.Play("KnifeHit");
                        if (doSuperEnemySpawning)
                            soundManager.Play("SuperEnemyDeath");
                        soundManager.Play("EnemyDeath");

                        winCon = true;
                        onEndScreen = true;
                    }
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                Debug.Log("Ennemies locked: " + enemiesKilled.Count);
                Debug.Log(Tick);

                if (resultSent)
                    return;

                timeTick.DiscountKnife(8 - Tick);

                if (Tick == 8)
                {
                    if (!onEndScreen)
                        Manager.Instance.Result(false);
                    else
                        Manager.Instance.Result(winCon);
                    
                    resultSent = true;
                }
            }

            #region AmmoInteraction
            public void MinusAmmo()
            {
                ammo--;
                ammoCount.DiscountKnife(ammo);
                Debug.Log("Ammo left: " + ammo);
            }
            public void PlusMiss()
            {
                misses++;
                Debug.Log("Missed: " + misses);
            }

            public bool GetAmmoZero()
            {
                if (ammo == 0)
                    return true;
                else
                    return false;
            }
            #endregion

            void InstantiateSpawner(GameObject _spawnSet)
            {
                chosenSpawnSet = Instantiate(_spawnSet, spawnSetAnchor.transform.position, Quaternion.identity);
                chosenSpawnSet.transform.SetParent(spawnSetAnchor.transform);
                chosenSpawner = chosenSpawnSet.GetComponent<EnemySpawnerMag>();
                chosenSpawner.GetEnemyNumber(numberOfEnemies, doSuperEnemySpawning);
                chosenSpawner.SpawnBehavior();
                Debug.Log(chosenSpawnSet.name + " was instatiated");
            }

            bool DecideSuperEnemySpawn()
            {
                int superSpawnChance = Random.Range(1, 101);
                if(superSpawnChance <= superEnemyChance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}