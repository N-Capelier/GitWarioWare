using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewMalediction", menuName ="Malediction")]
public class Malediction : ScriptableObject
{
    [HideInInspector] public int timer =4 ;
    public string maledictionName;
    public GameObject prefabManager;
    private GameObject instantiatedPrefab;
    public void StartMalediction()
    {
        Reset();
        instantiatedPrefab = Instantiate(prefabManager, Vector3.zero, Quaternion.identity);
    }

    private void Reset()
    {

    }


    public void StopMalediction()
    {
        Destroy(instantiatedPrefab);
    }
}
