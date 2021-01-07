using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Boss
{
    public class BossUI : MonoBehaviour
    {
        public RectTransform bossHpBackground;
        public RectTransform bossCurrentHpParent;

        public void UpdateHp(float maxHp, float currentHp)
        {           
            bossCurrentHpParent.DOScaleX(bossCurrentHpParent.transform.localScale.x * currentHp / maxHp,0.5f);
        }


    }
}
