using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Present : MonoBehaviour
{
    public IdleGame idleGame;
    public Vector3 botPos;
    public Vector3 topPos;
    public GameObject presentParent;
    public bool showPresent;
    public float presentTimer;
    public float presentCD, presentRangeCD, tempPresentCD;
    public float randomX;

    public GameObject rewardPopUp;
    public TextMeshProUGUI rewardText;
    public double rewardCoins, rewardGems;
    public bool showAd;
    public PopupText popupText;
    public Button adBtn;

    public void Update()
    {
        if (!showPresent && !rewardPopUp.activeSelf)
        {
            presentTimer += Time.deltaTime;
            if (presentTimer >= presentCD)
            {
                showPresent = true;
                SetupReward();
                if (idleGame.adMob.rewardedAd.IsLoaded())
                {
                    adBtn.interactable = true;
                }
                else
                {

                    if (idleGame.ads)
                        idleGame.adMob.RequestRewardedAd();
                    adBtn.interactable = false;
                }
                presentCD = UnityEngine.Random.Range(tempPresentCD - presentRangeCD, tempPresentCD + presentRangeCD);
                ResetPresentTimer();
                presentParent.SetActive(true);
                PresentMovement();
            }
        }
    }

    public void PresentMovement()
    {
        iTween.MoveTo(presentParent, iTween.Hash("position", topPos, "islocal", true, "time", 30f, "oncomplete", "ResetPresentTimer", "oncompletetarget", this.gameObject, "easeType", iTween.EaseType.linear));
    }

    public void PresentButton()
    {
        iTween.Stop(presentParent);
        ResetPresentTimer();
        rewardPopUp.SetActive(true);
    }

    public void ResetPresentTimer()
    {
        randomX = UnityEngine.Random.Range(-70, 71);
        botPos = new Vector3(randomX, 0, 0);
        topPos = new Vector3(randomX, 700, 0);
        showPresent = false;
        presentTimer = 0;
        presentParent.SetActive(false);
        presentParent.transform.localPosition = botPos;
    }

    public void AcceptButton()
    {
        if (rewardGems == 0)
        {
            idleGame.coins += rewardCoins;
            rewardPopUp.SetActive(false);
            popupText.SetCoinReward("+" + idleGame.CoinConversionTwoDecimal(rewardCoins) + " <sprite=4>");
            popupText.ShowPopup();
        }
        else
        {
            idleGame.gems += rewardGems;
            rewardPopUp.SetActive(false);
            popupText.SetGemReward("+" + rewardGems + " <sprite=5>");
            popupText.ShowPopup();
        }
        idleGame.UpdateCurrencyText();
        idleGame.Save();
    }

    public void AdRewardButton()
    {
        if (idleGame.ads)
        {
            showAd = true;
            idleGame.adMob.ShowRewardedAd();
        }
        else
        {
            DoubleReward();
        }
    }

    public void SetupReward()
    {
        if (UnityEngine.Random.value > 0.30)
        {
            rewardCoins = idleGame.RoundCostConversion(100 + (idleGame.totalCoins * 0.08));
            rewardText.text = "+" + idleGame.CoinConversionTwoDecimal(rewardCoins) + " <sprite=4>";
            rewardGems = 0;
        }
        else
        {
            rewardGems = UnityEngine.Random.Range(1, 6);
            rewardText.text = "+" + rewardGems + " <sprite=5>";
            rewardCoins = 0;
        }
    }

    public void DoubleReward()
    {
        if (rewardGems == 0)
        {
            rewardCoins *= 2;
            idleGame.coins += rewardCoins;
            rewardPopUp.SetActive(false);

            popupText.SetCoinReward("+" + idleGame.CoinConversionTwoDecimal(rewardCoins) + " <sprite=4>");
            popupText.ShowPopup();
        }
        else
        {
            rewardGems *= 2;
            idleGame.gems += rewardGems;
            rewardPopUp.SetActive(false);

            popupText.SetGemReward("+" + rewardGems + " <sprite=5>");
            popupText.ShowPopup();
        }
        idleGame.UpdateCurrencyText();
        idleGame.Save();
    }

}
