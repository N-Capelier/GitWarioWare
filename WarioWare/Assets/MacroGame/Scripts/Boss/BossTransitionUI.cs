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

        [Header("Appearance")]
        public SpriteRenderer bossRenderer;
        public Sprite miniBoss;
        public Sprite boss;
        public Image lifeBarRenderer;
        public Sprite miniBosslifeBar;
        public Sprite bossLifeBar;
        public Sprite miniBossCran;
        public Sprite bossCran;
        public Image backgroundBarRenderer;
        public Sprite miniBossBackground;
        public Sprite bossBackground;
        public GameObject keystoneImage;
        public GameObject keystoneText;
        public void InitiateBossLife(float initialLife, float currentLife, bool isMiniBoss)
        {
            if (isMiniBoss)
            {
                bossRenderer.sprite = miniBoss;
                lifeBarRenderer.sprite = miniBosslifeBar;
                foreach (var cransprite in cran)
                {
                    cransprite.sprite = miniBossCran;
                }
                backgroundBarRenderer.sprite = miniBossBackground;
                keystoneImage.SetActive(false);
                keystoneText.SetActive(false);
            }
            else
            {
                bossRenderer.sprite = boss;
                lifeBarRenderer.sprite = bossLifeBar;
                foreach (var cransprite in cran)
                {
                    cransprite.sprite = bossCran;
                }
                backgroundBarRenderer.sprite = bossBackground;
                keystoneImage.SetActive(true);
                keystoneText.SetActive(true);
            }
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
