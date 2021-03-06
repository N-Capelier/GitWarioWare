﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trisibo;

[CreateAssetMenu(fileName = "New ID", menuName = "IDCard", order = 50)]
[System.Serializable]
public class IDCard : ScriptableObject
{
    
    public Cluster cluster;
    [SerializeField] public string trio;

    [SerializeField] public bool asSecondSprite;
    [SerializeField] public Sprite secondSprite;
    public int indexEnum;
    public string verbe = string.Empty;
    [SerializeField]  public SceneField microGameScene;

    [SerializeField] public Sprite inputs;

    // will be used later on the macro;
    [SerializeField] public Difficulty currentDifficulty;
    public int winningStreak = 0;
    public float losingStreak = 0;
    [SerializeField]  public int idWeight = 0;
    public bool hasBarrel;

    public IDCard()
    {

    }
}


#region enum
public enum Cluster { Theodore, Aurelien, Thibault};
public enum TrioTheodore { Brigantin, SpanishInquisition, TrapioWare, LeRafiot };
public enum TrioAurelien { ACommeAkuma, RadioRTL, DragonsPépères};
public enum TrioThibault { LLL, Soupe, Fleebos, SAS};
public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD
}
public enum BPM
{
    Slow = 60,
    Medium = 80,
    Fast = 100,
    SuperFast = 120
}
#endregion

