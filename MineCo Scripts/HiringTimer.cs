using UnityEngine;
using UnityEngine.UI;

public class HiringTimer : MonoBehaviour
{
    public IdleGame idleGame;
    public PopulateHiringMiners popHiringMiners;
    public Text timerText;
    public int minutes;
    public int seconds;
    public double currentTime;
    public bool isNowHiring;
    public Button gemBtn;
    public Confirmation confirmation;
    public PopupText popupText;

    public void Update()
    {
        if (!isNowHiring)
        {
            if (currentTime >= 0)
            {
                currentTime -= Time.deltaTime;
                minutes = Mathf.FloorToInt((int)currentTime / 60);
                seconds = Mathf.FloorToInt((int)currentTime % 60);
                if (seconds < 10)
                {
                    timerText.text = minutes.ToString() + ":0" + seconds.ToString();
                }
                else timerText.text = minutes.ToString() + ":" + seconds.ToString();
            }
            else if (currentTime <= 0)
            {
                if (confirmation.gameObject.activeSelf) confirmation.openUIButton.OpenCloseUIButton();
                currentTime = 0;
                isNowHiring = true;
                popHiringMiners.Populate();
                ShowNowHiring();
            }
        }
    }

    public void Awake()
    {
        currentTime = idleGame.hiringBoardTimer;
    }

    public void ShowNowHiring()
    {
        gemBtn.gameObject.SetActive(false);
        timerText.text = "Now Hiring";
        popHiringMiners.rerollBtn.interactable = true;
        popHiringMiners.hiringboard.GetComponent<Button>().interactable = true;
    }

    public void ResetTimer()
    {
        currentTime = idleGame.hiringBoardTimer;
        isNowHiring = false;
        popHiringMiners.hiringboard.GetComponent<Button>().interactable = false;
        gemBtn.gameObject.SetActive(true);
    }

    public void GemTimerButton()
    {
        if (idleGame.gems >= 1)
        {
            confirmation.SetListeners("GemTimerFunction");  //Set YesBtn onClick with FireFunction
            confirmation.SetText("Spend 1 <sprite=5>\nto finish search?");
            confirmation.ShowConfirmationWindow();  //Show window.
        }
    }

    public void GemTimerFunction()
    {
        if (idleGame.gems >= 1)
        {
            idleGame.gems -= 1;
            currentTime = 0;
            idleGame.UpdateCurrencyText();
        }
        else
        {
            popupText.SetText("Not enough gems.");
            popupText.ShowPopup();
        }
    }
    
}
