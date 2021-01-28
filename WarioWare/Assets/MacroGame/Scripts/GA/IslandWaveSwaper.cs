using UnityEngine;
using UnityEngine.UI;

public class IslandWaveSwaper : MonoBehaviour
{
    public Image island_Borders;
    public Texture island_DF_Texture;

    private void ChangeMaterialTexture()
    {
        island_Borders.material.SetTexture("_MainTex", island_Borders.mainTexture);
    }
}
