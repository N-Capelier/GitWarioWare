using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Islands
{
    public enum IslandAnchorPoint
    {
        North,
        North_East,
        East,
        South_East,
        South,
        South_West,
        West,
        North_West
    }

    [CreateAssetMenu(fileName = "New Island Sprite", menuName = "Island Sprite", order = 50)]
    public class IslandSprite : ScriptableObject
    {
        public Sprite sprite;
        public IslandAnchorPoint anchorPoint;
        public Material borderMaterial;
    }
}