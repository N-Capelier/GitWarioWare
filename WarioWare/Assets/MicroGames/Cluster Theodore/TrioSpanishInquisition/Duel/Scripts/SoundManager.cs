﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpanishInquisition
{
    namespace Duel
    {
        public class SoundManager : MonoBehaviour
        {

            private static SoundManager _instance;
            public static SoundManager instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<SoundManager>();
                    }
                    return _instance;
                }
            }

            private AudioSource[] gameSounds;

            [SerializeField]
            private AudioSource parry;
            [SerializeField]
            private AudioSource victorySound;
            [SerializeField]
            private AudioSource defeatSound;
            [SerializeField]
            private AudioSource enemyAttack;
            [SerializeField]
            private AudioSource prepareAttack;

            [SerializeField]
            private AudioSource musicSlow;
            [SerializeField]
            private AudioSource musicMedium;
            [SerializeField]
            private AudioSource musicFast;
            [SerializeField]
            private AudioSource musicSuperFast;

            [SerializeField]
            private AudioSource backgroundNoise;


            // Start is called before the first frame update
            void Start()
            {
                gameSounds = GetComponents<AudioSource>();

                parry = gameSounds[0];
                victorySound = gameSounds[1];
                defeatSound = gameSounds[2];
                enemyAttack = gameSounds[3];
                prepareAttack = gameSounds[4];
                musicSlow = gameSounds[5];
                musicMedium = gameSounds[6];
                musicFast = gameSounds[7];
                musicSuperFast = gameSounds[8];
                backgroundNoise = gameSounds[9];
            }

            public void PlayParry()
            {
                parry.Play();
            }

            public void PlayAttack()
            {
                enemyAttack.Play();
            }

            public void PlayVictory()
            {
                victorySound.Play();
            }

            public void PlayDefeat()
            {
                defeatSound.Play();
            }

            public void PlayPrepareAttack()
            {
                prepareAttack.Play();
            }

            public void PlayDuelMusicSlow()
            {
                musicSlow.Play();
            }

            public void PlayDuelMusicMedium()
            {
                musicMedium.Play();
            }

            public void PlayDuelMusicFast()
            {
                musicFast.Play();
            }

            public void PlayDuelMusicSuperFast()
            {
                musicSuperFast.Play();
            }

            public void PlayBackgroundNoise()
            {
                backgroundNoise.Play();
            }
        }
    }
}
