using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Confirmation : MonoBehaviour
{
    public Button yesBtn;
    public Button noBtn;
    public TextMeshProUGUI smallText;
    public CurrentSelectedSlot css; //FireButton
    public PrestigeSystem prestige; //Prestige Reset
    public Settings settings;   //Reset Game
    public HiringTimer hiringTimer; //Gem Timer
    public OpenUIButton openUIButton;
    public void ShowConfirmationWindow()
    {
        gameObject.SetActive(true);
    }
    public void OnDisable()
    {
        yesBtn.onClick.RemoveAllListeners();
        noBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(openUIButton.OpenCloseUIButton);
        noBtn.onClick.AddListener(openUIButton.OpenCloseUIButton);
        ResetText();
    }

    public void SetListeners(string functionName)
    {
        switch (functionName)
        {
            case "FireConfirmationFunction":
                yesBtn.onClick.AddListener(css.FireConfirmationFunction);
                break;
            case "PrestigeReset":
                yesBtn.onClick.AddListener(prestige.PrestigeReset);
                break;
            case "ResetGame":
                yesBtn.onClick.AddListener(settings.ResetGame);
                break;
            case "GemTimerFunction":
                yesBtn.onClick.AddListener(hiringTimer.GemTimerFunction);
                break;
            default:
                break;
        }
    }

    public void SetCSS(CurrentSelectedSlot c)
    {
        css = c;
    }

    public void SetText(string s)
    {
        smallText.text = s;
    }

    public void ResetText()
    {
        smallText.text = "Are you sure?";
    }
}
