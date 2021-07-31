using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupText : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public Image BG;
    public Image rewardImageOnly;
    public GameObject image2;   //image over rewardImageOnly.
    public Sounds sounds; //0-coin, 1-gem, 2-shiny, 3-prestige
    public AudioSource audioSource;
    public float timer;
    public bool showPopup;
    public float upTime;

    public void Start()
    {
        gameObject.SetActive(false);
        if (BG != null)
        {
            BG.enabled = true;
        }
    }
    public void OnDisable()
    {
        upTime = 2f;
        if (rewardImageOnly != null)
        {
            if (rewardImageOnly.sprite != null)
            {
                image2.gameObject.SetActive(false);
                rewardImageOnly.sprite = null;
                rewardImageOnly.gameObject.SetActive(false);
            }
        }
        if (audioSource != null)
        {
            audioSource.clip = null;
        }
    }
    public void Update()
    {
        if (showPopup)
        {
            timer += Time.deltaTime;
            if (timer >= upTime)
            {
                showPopup = false;
                timer = 0;
                gameObject.SetActive(false);
            }
        }
    }

    public void ShowPopup()
    {
        timer = 0;
        if (rewardImageOnly != null)
        {
            if (rewardImageOnly.sprite != null)
            {
                image2.gameObject.SetActive(true);
                rewardImageOnly.gameObject.SetActive(true);
            }
        }
        showPopup = true;
        gameObject.SetActive(true);
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
                audioSource.Play();
        }
    }

    public void HidePopup()
    {
        gameObject.SetActive(false);
    }

    public void SetText(string s)
    {
        popupText.text = s;
    }

    public void SetCoinReward(string s)
    {
        audioSource.clip = sounds.sfxList[0];
        popupText.text = s;
    }

    public void SetGemReward(string s)
    {
        audioSource.clip = sounds.sfxList[1];
        popupText.text = s;
    }

    public void SetImageReward(string s, Sprite image)
    {
        upTime = 3f;
        popupText.text = s;
        audioSource.clip = sounds.sfxList[2];
        rewardImageOnly.sprite = image;
    }

    public void SetPrestigeText(string s)
    {
        upTime = 5f;
        popupText.text = s;
        audioSource.clip = sounds.sfxList[3];
    }
}
