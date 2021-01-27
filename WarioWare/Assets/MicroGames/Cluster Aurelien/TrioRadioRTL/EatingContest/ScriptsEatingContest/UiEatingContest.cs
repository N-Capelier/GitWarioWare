using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Caps;

namespace RadioRTL
{
    namespace EatingContest
    {
        public class UiEatingContest : TimedBehaviour
        {
            public PlayerController playerController;
            public int chomp;
            public int numberOfChomps;
            public int totalPlates;
            [Header("UI")]
            
            
            public TextMeshProUGUI bpmText;
            public Slider timerUI;
            public TextMeshProUGUI tickNumber;
            public GameObject input;
            public override void Start()
            {
                base.Start(); //Do not erase this line!
                bpmText.text = "bpm: " + bpm.ToString();
                totalPlates = playerController.numberOfPlates;
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
                chomp = playerController.chomp;
                numberOfChomps = playerController.numberOfChomps;
                timerUI.value = ((float)playerController.numberOfPlates/(float)totalPlates);
                
                
                
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 1)
                    input.SetActive(false);
                if(Tick <= 8)
                    tickNumber.text = Tick.ToString();
            }
        }
    }
}