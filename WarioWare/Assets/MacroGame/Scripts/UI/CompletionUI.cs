using Caps;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompletionUI : MonoBehaviour
{
    
    public GameObject rewardCanvas;
    public GameObject aButton;
    
    [Header("Moral")]
    public GameObject moralBar;
    public Image moralFillBar;
    public TextMeshProUGUI goldText;
    public Image craneImage;

    [Header("Crane Sprites")] 
    public Sprite crane_1;
    public Sprite crane_2;
    public Sprite crane_3;
    public Sprite crane_4;

    [Header("Chest Sprites")]
    public Sprite bronzeChest;
    public Sprite silverChest;
    public Sprite goldChest;
    public Sprite noChest;

    [Header("Reward")]
    public GameObject rewardContainer;
    public TextMeshProUGUI rewardDescription;
    public Image rewardImage;

    [Header("Completion Reward")]
    public GameObject completionContainer;
    public GameObject completionText;
    public Image chestImage;
    public ParticleSystem chestParticle;

    private float _completion;
    public static bool completionIsDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartCompletion(float completion)
    {
        completionIsDone = false;
        _completion = completion;
        StartCoroutine(CompletionRoutine());
    }

    void ShowMoral()
    {
        moralBar.SetActive(true);
        StartCoroutine(FillMoral());


        if (PlayerManager.Instance.moral < 25)
        {
            craneImage.sprite = crane_1;
            goldText.text = "+ 0 Beatcoins...";
        }
        else if (PlayerManager.Instance.moral >= 25 && PlayerManager.Instance.moral < 50)
        {
            craneImage.sprite = crane_2;
            goldText.text = "+ 5 Beatcoins !";
        }
        else if (PlayerManager.Instance.moral >= 50 && PlayerManager.Instance.moral < 75)
        {
            craneImage.sprite = crane_3;
            goldText.text = "+ 10 Beatcoins !";
        }
        else if (PlayerManager.Instance.moral > 75)
        {
            craneImage.sprite = crane_4;
            goldText.text = "+ 15 Beatcoins !";
        }
    }

    void ShowRewards()
    {
        rewardContainer.SetActive(true);
        if(PlayerMovement.Instance.playerIsland.reward != null)
        {
            rewardImage.sprite = PlayerMovement.Instance.playerIsland.reward.sprite;
            rewardDescription.text = PlayerMovement.Instance.playerIsland.reward.rewardName;
        }
    }

    private IEnumerator ShowCompletion()
    {
        completionContainer.SetActive(true);
        chestImage.gameObject.SetActive(true);
        chestImage.sprite = noChest;

        if (_completion >= 0.5f)
        {
            chestImage.sprite = bronzeChest;
            if(_completion >= 0.75f && _completion < 1f)
            {
                chestImage.sprite = silverChest;
            }
            else if(_completion >= 1f)
            {
                chestImage.sprite = goldChest;
            }
            yield return new WaitForSeconds(1f);
            chestImage.gameObject.SetActive(false);
            chestParticle.Play();
            yield return new WaitForSeconds(0.5f);
            completionText.SetActive(true);   
           
        }
        completionIsDone = true;
        aButton.SetActive(true);
    }

    private IEnumerator FillMoral()
    {
        int moralToAdd = Manager.Instance.moralCost;
        moralFillBar.fillAmount = (float)(PlayerManager.Instance.moral + 10) / 100f;

        float fraction = (float)moralToAdd / 100f;

        for (float i = 0; i < 1; i += 0.01f)
        {
           if(PlayerManager.Instance.moral + moralToAdd < 100)
                moralFillBar.fillAmount += (fraction/100);

            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator CompletionRoutine()
    {
        ResetAllItems();
        rewardCanvas.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ShowMoral();
        yield return new WaitForSeconds(2);
        ShowRewards();
        yield return new WaitForSeconds(2);
        StartCoroutine(ShowCompletion());
        yield return new WaitUntil(() => Input.GetButtonDown("A_Button"));
        Manager.Instance.CloseReward();
        ResetAllItems();
        UI.UICameraController.canSelect = true;
    }


    
    private void ResetAllItems()
    {
        rewardContainer.SetActive(false);
        moralBar.SetActive(false);
        completionContainer.SetActive(false);
        rewardCanvas.SetActive(false);
        completionText.SetActive(false);
        chestImage.gameObject.SetActive(false);
        aButton.SetActive(false);
    }

}
