using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caps
{
    public class Tester : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Manager.Instance.CapAttribution();
            Manager.Instance.ResetIDCards();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var _random = Random.Range(0, Manager.Instance.islandList.Length);
                StartCoroutine(Manager.Instance.StartCap(Manager.Instance.islandList[_random].capList[Random.Range(0, Manager.Instance.islandList[_random].capList.Count)]));
            }
        }
    }

}
