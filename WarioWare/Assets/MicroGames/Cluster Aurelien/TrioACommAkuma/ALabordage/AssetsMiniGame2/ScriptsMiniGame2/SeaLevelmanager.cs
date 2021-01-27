using UnityEngine;

namespace ACommeAkuma
{
    namespace ALAbordage
    {
        /// <summary>
        /// Simon PICARDAT
        /// </summary>

        public class SeaLevelmanager : TimedBehaviour
        {
            public GameObject easyMode;
            public GameObject mediumMode;
            public GameObject hardMode;

            public GameObject currentDifficultyMode;
            public int difficultyNum;

            public Animator waves;
            public Animator BuoLeft;
            public Animator BuoRight;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        easyMode.SetActive(true);
                        currentDifficultyMode = easyMode; 
                        difficultyNum = 1;
                        break;
                    case Difficulty.MEDIUM:
                        mediumMode.SetActive(true);

                        currentDifficultyMode = mediumMode;
                        difficultyNum = 2;
                        break;
                    case Difficulty.HARD:
                        hardMode.SetActive(true);
                        currentDifficultyMode = hardMode;
                        difficultyNum = 3;
                        break;
                }

                waves = currentDifficultyMode.transform.GetChild(0).GetComponent<Animator>();
                waves.speed = waves.speed * ((bpm / 60)/1.5f);
                BuoLeft = currentDifficultyMode.transform.GetChild(1).GetComponent<Animator>();
                BuoLeft.speed = BuoLeft.speed * ((bpm / 60)/1.5f);
                BuoRight = currentDifficultyMode.transform.GetChild(2).GetComponent<Animator>();
                BuoRight.speed = BuoRight.speed * ((bpm / 60)/1.5f);

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }
        }
    }
}