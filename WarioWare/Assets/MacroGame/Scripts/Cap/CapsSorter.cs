using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Trisibo;
[CreateAssetMenu(fileName = "New Sorter", menuName = "CapSorter", order = 50)]
[System.Serializable]
public class CapsSorter : ScriptableObject
{

    [SerializeField] public bool chalInput;
    [SerializeField] public List<IDCard> idCards;

   [SerializeField] public List<IDCardList> sortedIdCards = new List<IDCardList>((int)ChallengeHaptique.A10);
}

[System.Serializable]
public class IDCardList
{
    public List<IDCard> IDCards = new List<IDCard>();
    public int weight;
}