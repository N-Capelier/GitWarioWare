using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmbrunsMalediction : MonoBehaviour
{
    private RectTransform canvasTransform;
    public GameObject wave;
    public Image waveImage;

    public Color waveColor;

    public float waveDuration;
    public float timeWaveToFade;
    public float waveFadeDuration;
    public bool loop = false;

    // Start is called before the first frame update
    void Start()
    {
        waveColor.a = 1;
        waveImage.color = waveColor;
        canvasTransform = GetComponent<RectTransform>();
        wave.transform.position = new Vector3(canvasTransform.rect.width/2, -canvasTransform.rect.height/2,0);
        StartCoroutine(Wave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Wave()
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            wave.transform.position += new Vector3(0, canvasTransform.rect.height / 100, 0);
            yield return new WaitForSeconds(waveDuration / 100);
        }
        yield return new WaitForSeconds(timeWaveToFade);
        StartCoroutine(Fade());
    }
    private IEnumerator Fade()
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            waveColor.a -= 0.01f;
            waveImage.color = waveColor;
            yield return new WaitForSeconds(waveDuration / 100);
        }

        if (loop)
            Start();
    }
}
