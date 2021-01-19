using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Rewards;

[CustomEditor(typeof(RewardDebug))]
public class RewardsDebugEditor : Editor
{
    private bool[] fodloutArray;
    private RewardDebug rewardDebug;

    #region eachRewards
    private BeatcoinReward beatcoinReward;
    private RationReward rationReward;
    private HealReward healReward;
    private HarpoonReward harpoonReward;
    private CannonballReward cannonballReward;
    private HardTimeReward hardTimeReward;
    private AC130Reward AC130Reward;
    private SturdyReward sturdyReward;
    #endregion
    private void OnEnable()
    {
        rewardDebug = target as RewardDebug;
        fodloutArray = new bool[rewardDebug.rewardsList.Count];
    }
    public override void OnInspectorGUI()
    {

        for (int i = 0; i < rewardDebug.rewardsList.Count; i++)
        {
            EditorGUILayout.LabelField(rewardDebug.rewardsList[i].rewardName, EditorStyles.boldLabel);
            rewardDebug.rewardsList[i].price = EditorGUILayout.IntSlider("     Price", rewardDebug.rewardsList[i].price, 0, 1000);
            rewardDebug.rewardsList[i].rarity = (RewardRarity)EditorGUILayout.EnumPopup("     Rarity", rewardDebug.rewardsList[i].rarity);
            rewardDebug.rewardsList[i].dropRateWeight = EditorGUILayout.IntSlider("     Price", rewardDebug.rewardsList[i].price, 0, 100);
            switch (rewardDebug.rewardsList[i].rewardName)
            {
                case "Sac de Beatcoins":
                    beatcoinReward = rewardDebug.rewardsList[i] as BeatcoinReward;
                    beatcoinReward.beatcoinAmount = EditorGUILayout.IntField("     Beatcoin amount", beatcoinReward.beatcoinAmount);
                    break;
                case "Ration":
                    rationReward = rewardDebug.rewardsList[i] as RationReward;
                    rationReward.foodAmount = EditorGUILayout.IntField("     Food Amount", rationReward.foodAmount);
                    break;
                case "Planche de bois":
                    healReward = rewardDebug.rewardsList[i] as HealReward;
                    healReward.healAmount = EditorGUILayout.IntField("     Heal amount", healReward.healAmount);
                    break;
                case "La galère":
                    hardTimeReward = rewardDebug.rewardsList[i] as HardTimeReward;
                    hardTimeReward.beatcoinAmount = EditorGUILayout.IntField("     Beatcoin amount", hardTimeReward.beatcoinAmount);
                    hardTimeReward.foodAmount = EditorGUILayout.IntField("     Food Amount", hardTimeReward.foodAmount);
                    break;
                case "Harpon":
                    harpoonReward = rewardDebug.rewardsList[i] as HarpoonReward;
                    harpoonReward.bonusBarrels = EditorGUILayout.IntField("     bonus barrels", harpoonReward.bonusBarrels);
                    break;

                case "Boulet de cannon":
                    cannonballReward = rewardDebug.rewardsList[i] as CannonballReward;
                    cannonballReward.cannonDamage = EditorGUILayout.IntField("      Canon damage", cannonballReward.cannonDamage);
                    break;
                case "AC-130":
                    AC130Reward = rewardDebug.rewardsList[i] as AC130Reward;
                    AC130Reward.damages = EditorGUILayout.IntField("     damage", AC130Reward.damages);
                    break;
                case "Rattrapage":
                    sturdyReward = rewardDebug.rewardsList[i] as SturdyReward;
                    sturdyReward.healAmmount = EditorGUILayout.IntField("     heal amount", sturdyReward.healAmmount);
                    break;
                case "Pack de rations":
                    rationReward = rewardDebug.rewardsList[i] as RationReward;
                    rationReward.foodAmount = EditorGUILayout.IntField("     Food Amount", rationReward.foodAmount);
                    break;
                case "Kaisse en kit":
                    healReward = rewardDebug.rewardsList[i] as HealReward;
                    healReward.healAmount = EditorGUILayout.IntField("     Heal amount", healReward.healAmount);
                    break;
                case "Black Captain-Card":
                    beatcoinReward = rewardDebug.rewardsList[i] as BeatcoinReward;
                    beatcoinReward.beatcoinAmount = EditorGUILayout.IntField("     Beatcoin amount", beatcoinReward.beatcoinAmount);
                    break;
                default:
                    break;

            }
        }


        Repaint();
        EditorUtility.SetDirty(rewardDebug);
        serializedObject.ApplyModifiedProperties();
    }
}
