using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Debuging Values", menuName = "Debuging Values", order = 50)]
[System.Serializable]
public class DebugingTool : ScriptableObject
{


    [SerializeField] public List<string> names = new List<string>();
    [SerializeField] public List<int> values = new List<int>();
    public DebugingTool()
        {
        //CapsManager
            names.Add("numberBeforSpeedUp"); values.Add(2);
            names.Add("idWeighToAdd"); values.Add(2);
            names.Add("idInitialWeight"); values.Add(10);
            names.Add("damageOnMiniGameLose"); values.Add(10);
            names.Add("barrelProbability"); values.Add(30);
            names.Add("maxBarrelRessource"); values.Add(10);
            names.Add("minBarrelRessource"); values.Add(8);
            names.Add("lifeWeight"); values.Add(1);
            names.Add("goldWeight"); values.Add(5);
            names.Add("foodWeight"); values.Add(3);
        //PlayerManager
            names.Add("playerHP"); values.Add(100);
            names.Add("beatcoins"); values.Add(200);
            names.Add("food"); values.Add(50);
            names.Add("maxFood"); values.Add(100);
        //PlayerMovement
            names.Add("foodPrice"); values.Add(10);
            names.Add("damageWhenNoFood"); values.Add(10);
        //IslandCreator
            names.Add("commonRewardRateWeight"); values.Add(50);
            names.Add("rareRewardRateWeight"); values.Add(35);
            names.Add("epicRewardRateWeight"); values.Add(15);
            names.Add("comonRewardRandomness"); values.Add(20);
            names.Add("rareRewardRandomness"); values.Add(20);
        //Island
            names.Add("anchorRange"); values.Add(64);
        //BossManager
            names.Add("damageToPlayer"); values.Add(10);
            names.Add("damageToBoss"); values.Add(10);
            names.Add("differentMiniGameNumber"); values.Add(5);
            names.Add("maxLife"); values.Add(100);
        }
}


