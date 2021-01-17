using Caps;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{ 
    public class ZoneText : MonoBehaviour
    {
        public GameObject panel;
        public TextMeshProUGUI textContainer;
        public string text;
        public float lifeTime;
    
        // Start is called before the first frame update
        IEnumerator Start()
        {
            panel.SetActive(true);
            Manager.Instance.eventSystem.enabled = false;
            textContainer.text = text;
            yield return new WaitForSeconds(lifeTime);
            panel.SetActive(false);
            Manager.Instance.eventSystem.enabled = true;
        }


    }
}
