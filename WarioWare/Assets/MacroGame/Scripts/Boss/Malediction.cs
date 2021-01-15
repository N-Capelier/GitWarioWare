using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewMalediction", menuName ="Malediction/NormalMalediction")]
public class Malediction : ScriptableObject
{
    [HideInInspector] public int timer =4 ;
    public string maledictionName;
    public GameObject prefabManager;
    private GameObject instantiatedPrefab;
    public virtual void StartMalediction()
    {
        Reset();
        instantiatedPrefab = Instantiate(prefabManager, Vector3.zero, Quaternion.identity);
    }

    private void Reset()
    {

    }


    public virtual void StopMalediction()
    {
        Destroy(instantiatedPrefab);
    }
}
