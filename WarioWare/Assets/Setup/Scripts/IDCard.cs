using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Trisibo;
[CreateAssetMenu(fileName = "New ID", menuName = "IDCard", order = 50)]
[System.Serializable]
public class IDCard : ScriptableObject
{
    public string idName ;
    public Cluster cluster;
    public string trio;
  [SerializeField]  public SceneField microGameScene;
    
}


public enum Cluster { Theodore, Aurelien, Thibault};
public enum TrioTheodore { Brigantin, SpanishInquisition, TrapioWare, LeRafiot };
public enum TrioAurelien { ACommeAkuma, RadioRTL, DragonsPépères};
public enum TrioThibault { LLL, Soupe, Fleebos, SAS};