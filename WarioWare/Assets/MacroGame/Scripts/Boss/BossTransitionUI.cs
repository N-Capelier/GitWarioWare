using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Boss
{

    public class BossTransitionUI : MonoBehaviour
    {

        public Image lifeBar;
        public Image[] cran;
        public Transform startTransform;
        public Transform endTransform;
        public void InitiateBossLife(float initialLife, float currentLife)
        {
            float currentLifePurcentage = currentLife / initialLife;
            lifeBar.fillAmount = currentLifePurcentage;
            float distance = ( endTransform.position.x - startTransform.position.x) / currentLifePurcentage;
            for (int i = 0; i < cran.Length; i++)
            {
                cran[i].transform.position = new Vector3(startTransform.position.x + distance * (i + 1) / cran.Length, startTransform.position.y, startTransform.position.z);
            }
        }

        public IEnumerator BossTakeDamage(float initialLife, float currentLife)
        {
            float currentLifePurcentage = currentLife / initialLife;

           while(lifeBar.fillAmount> currentLifePurcentage) 
            { 
                lifeBar.fillAmount -= 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            lifeBar.fillAmount = currentLifePurcentage;
        }

    }
}
