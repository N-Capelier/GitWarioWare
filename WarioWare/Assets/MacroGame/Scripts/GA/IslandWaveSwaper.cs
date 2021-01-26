using UnityEngine;
using UnityEngine.UI;

public class IslandWaveSwaper : MonoBehaviour
{
    public Image island;
    public Sprite[] islands;
    public Sprite[] islandDFs;

    MaterialPropertyBlock mbp;
    [HideInInspector]
    public MaterialPropertyBlock Mbp
    {
        get 
        { 
            if (mbp == null)
            {
                mbp = new MaterialPropertyBlock();
            }
            return mbp; 
        }
    }

    private void Awake()
    {
        //Mbp = island.material;
    }

    private void OnEnable()
    {
        for (int i = 0; i < islandDFs.Length; i++)
        {
            if(island.sprite == islands[i])
            {
                Mbp.SetTexture("_IslandDistField", islandDFs[i].texture);
                island.GetComponent<MeshRenderer>().SetPropertyBlock(Mbp);
            }
        }
    }
}
