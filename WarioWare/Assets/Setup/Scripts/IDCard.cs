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
public enum TrioTheodore { A1,B1,C1,D1};
public enum TrioAurelien { A2,B2,C2,D2};
public enum TrioThibault { A3,B3,C3,D3};