using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Testing;


namespace ExampleScene
{
    public class Spawner : MonoBehaviour
    {
        //will do the cooldwon
        private float timer;
        //global bpm at start
        private float bpm;
        // 60/ bpm to ahve a timer in seconds
        private float spawnCooldwon;
        //references
        public PlayerBehavior player;
        public GameObject ennemy;
        public Vector3 rightPosition;
        public Vector3 leftPosition;
        // previous ennemy was left or right
        private bool left;
        // end the game when reach 8
        private int cpt;
        //stop spawns on end game
        private bool canSpawn = true;

        private bool isHard;

        [Header("UI")]
        //win panel
        public GameObject panel;
        public TextMeshProUGUI resultText;
        public TextMeshProUGUI bpmText;
        public Slider timerUI;
        public TextMeshProUGUI ticNumber;
        
        private void Start()
        {
            bpm = Manager.Instance.bpm;
            bpmText.text = "bpm: " + bpm.ToString();
            spawnCooldwon = 60 / bpm;
            if (Manager.Instance.currentDifficulty == Manager.difficulty.HARD)
                isHard = true;
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            timer += Time.deltaTime;
            timerUI.value = timer / spawnCooldwon;
            if(timer>= spawnCooldwon && canSpawn)
            {
                timer = 0;
                if (!isHard)
                    NormalSpawn();
                else
                   StartCoroutine( HardSpawn());

                cpt++;
                ticNumber.text = cpt.ToString();
                if(cpt == 8)
                {
                   Result(true);
                }
            }
        }
        private void NormalSpawn()
        {
            if (left)
                Instantiate(ennemy, leftPosition, Quaternion.identity, transform);
            else
                Instantiate(ennemy, rightPosition, Quaternion.identity, transform);
            left = !left;
        }
         private IEnumerator HardSpawn()
        {
            NormalSpawn();
            if (Random.Range(0, 2) == 1)
            {
                yield return new WaitForSeconds(spawnCooldwon / 2);
                NormalSpawn();
            }

        }
        public void Result(bool win)
        {
            canSpawn = false;
            Manager.Instance.Result(true);
        }
    }

}
