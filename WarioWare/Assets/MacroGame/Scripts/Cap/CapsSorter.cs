using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Trisibo;
[CreateAssetMenu(fileName = "New Sorter", menuName = "CapSorter", order = 50)]
[System.Serializable]
public class CapsSorter : ScriptableObject
{
    [SerializeField] public List<IDCard> idCards;
    [SerializeField] public List<IDCard> idCardsNotPlayed;
    [SerializeField] public List<IDCard> iDCardsPlayed;

}

