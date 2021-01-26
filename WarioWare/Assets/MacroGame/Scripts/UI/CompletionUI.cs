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
    public GameObject rewardContainer;
    public GameObject completionContainer;
    public GameObject completionText;
    public Image chestImage;
    public ParticleSystem chestParticle;
    public GameObject moralBar;
    public Image moralFillBar;
    public TextMeshProUGUI rewardDescription;
    public TextMeshProUGUI goldText;
    public Image rewardImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartCompletion()
    {
        StartCoroutine(CompletionRoutine());
    }

    void ShowMoral()
    {
        moralBar.SetActive(true);
        StartCoroutine(FillMoral());
    }

    void ShowRewards()
    {
        rewardContainer.SetActive(true);
        if(PlayerMovement.Instance.playerIsland.reward != null)
        {
            rewardImage.sprite = PlayerMovement.Instance.playerIsland.reward.sprite;
            rewardDescription.text = PlayerMovement.Instance.playerIsland.reward.GetDescription();
        }
    }

    private IEnumerator ShowCompletion()
    {
        completionContainer.SetActive(true);
        //chestImage.sprite = il me faut les chests;
        yield return new WaitForSeconds(1f);
        chestImage.gameObject.SetActive(false);
        chestParticle.Play();
        yield return new WaitForSeconds(0.5f);
        completionText.SetActive(true);

    }

    private IEnumerator FillMoral()
    {
        int moralToAdd = Manager.Instance.moralCost;
        moralFillBar.fillAmount = (float)PlayerManager.Instance.moral / 100;

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
        rewardCanvas.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ShowMoral();
        yield return new WaitForSeconds(2);
        ShowRewards();
        yield return new WaitForSeconds(2);
        StartCoroutine(ShowCompletion());
        yield return new WaitForSeconds(5);
        ResetAllItems();
    }

    private void ResetAllItems()
    {
        rewardContainer.SetActive(false);
        moralBar.SetActive(false);
        completionContainer.SetActive(false);
        rewardCanvas.SetActive(false);
        completionText.SetActive(false);
        chestImage.gameObject.SetActive(true);
    }

}
