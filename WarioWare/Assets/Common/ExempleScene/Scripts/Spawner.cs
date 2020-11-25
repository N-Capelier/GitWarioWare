﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


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

        [Header("UI")]
        //win panel
        public GameObject panel;
        public TextMeshProUGUI resultText;
        public TextMeshProUGUI bpmText;
        public Slider timerUI;
        public TextMeshProUGUI ticNumber;
        private void Start()
        {
            bpm = player.bpm;
            bpmText.text = "bpm: " + bpm.ToString();
            spawnCooldwon = 60 / bpm;
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            timer += Time.deltaTime;
            timerUI.value = timer / spawnCooldwon;
            if(timer>= spawnCooldwon && canSpawn)
            {
                timer = 0;
                if (left)
                    Instantiate(ennemy, leftPosition, Quaternion.identity, transform);
                else
                    Instantiate(ennemy, rightPosition, Quaternion.identity, transform);
                left = !left;
                cpt++;
                ticNumber.text = cpt.ToString();
                if(cpt == 8)
                {
                    Result(true);
                }
            }
        }

        /// <summary>
        /// debug the result of the game on a panel
        /// </summary>
        /// <param name="win"> if true the game is win , if fals the game is lose</param>
        public void Result(bool win)
        {
            canSpawn = false;
            foreach (Ennemy ennemy in GetComponentsInChildren<Ennemy>())
            {
               Destroy( ennemy.gameObject);
            }
            panel.SetActive(true);
            if (win)
                resultText.text = "Victory";
            else
                resultText.text = "Lose";
           

        }
        
    }

}
