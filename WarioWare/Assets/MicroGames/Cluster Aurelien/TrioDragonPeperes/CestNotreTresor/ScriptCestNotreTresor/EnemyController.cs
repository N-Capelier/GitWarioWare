using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragons_Peperes
{
    namespace CestNotreTresor
    {
        /// <summary>
        /// Mael Ricou
        /// </summary>

        public class EnemyController : MonoBehaviour
        {
            public float enemySpeed = 5.5f;
            [SerializeField] float enemyXSpeed = 4;

            public Transform target;
            public float bound_Y;

            EnemyManager minigameManager;
            AudioManager audioManager;
            DockScrolling dockScrolling;
            PlayerController playerController;

            public GameObject looseScreen;

            [SerializeField] bool isEnemy2;


            public GameObject particule;

            private void Start()
            {
                target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                minigameManager = FindObjectOfType<EnemyManager>();
                particule = GameObject.Find("Particle");

                audioManager = FindObjectOfType<AudioManager>();
                dockScrolling = FindObjectOfType<DockScrolling>();
                playerController = FindObjectOfType<PlayerController>();

                if (minigameManager.bpm == 60)
                {
                    enemySpeed = enemySpeed * 1.1f;
                }

                if (minigameManager.bpm == 80)
                {
                    enemySpeed = enemySpeed * 1.2f;
                }

                if (minigameManager.bpm == 100)
                {
                    enemySpeed = enemySpeed * 1.3f;
                }

                if(minigameManager.bpm == 120)
                {
                    enemySpeed = enemySpeed * 1.5f;
                }
            }


            private void Update()
            {
                if (!isEnemy2)
                {
                    MoveEnemy();
                }

                if (isEnemy2)
                {
                    MoveEnemy2();
                }
            }

            void MoveEnemy()
            {
                //se dirige vers l'axe X du Joueur
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), enemyXSpeed * Time.deltaTime);

                //se déplace sur l'axe Y en négatif
                Vector2 temp = transform.position;
                temp.y -= enemySpeed * Time.deltaTime;
                transform.position = temp;

                //si l'ennemi dépasse un certain axe, se fait détruire
                if (temp.y < bound_Y)
                    Destroy(gameObject, 1);
            }

            public void MoveEnemy2()
            {
                //se déplace sur l'axe Y en négatif
                Vector2 temp = transform.position;
                temp.y -= enemySpeed * Time.deltaTime;
                transform.position = temp;

                //si l'ennemi dépasse un certain axe, se fait détruire
                if (temp.y < bound_Y)
                    Destroy(gameObject, 1);
            }

            private void OnTriggerEnter2D(Collider2D other)
            {
                if (other.name == "Player" && !minigameManager.gameFinished)
                {
                    EndGame();

                    if(minigameManager.playerLost != true)
                    {
                        audioManager.PlayCatched();
                    }
                    
                    minigameManager.playerLost = true;
                }
            }

            public void EndGame()
            {
                minigameManager.gameFinished = true;
                particule.SetActive(false);
                audioManager.StopMusic();
                audioManager.StopRunning();
                dockScrolling.scrollSpeed = 0f;
                enemySpeed = 0f;
                playerController.speed = 0f;
            }
        }

    }
}
