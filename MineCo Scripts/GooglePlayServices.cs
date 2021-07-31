using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayServices : MonoBehaviour
{
    public PopupText popupText;
    public Text accountText;
    // Start is called before the first frame update
    void Start()
    {
        PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames();
        PlayGamesPlatform.InitializeInstance(builder.Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();


        if (!Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
                if (result == SignInStatus.Success)
                {
                    if (accountText != null) accountText.text = "Logged in as:\n" + Social.localUser.userName;
                }
                else if (result == SignInStatus.Failed) Debug.Log("Sign-in Failed");
                else Debug.Log("Something Else");
            });
        }
        else if (Social.localUser.authenticated)
        {
            if (accountText != null) accountText.text = "Logged in as:\n" + Social.localUser.userName;
        }
    }

    //Google Play Sign-in
    public void GoogleSignIn()
    {
        if (Social.localUser.authenticated)
        {
            popupText.SetText("Already signed in");
            popupText.ShowPopup();
        }
        else
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
            {
                if (result == SignInStatus.Success)
                {
                    if (accountText != null) accountText.text = "Logged in as:\n" + Social.localUser.userName;
                }
                else if (result == SignInStatus.Failed) Debug.Log("Sign-in Failed");
                else Debug.Log("Something Else");
            });
        }
    }

    public void GoogleSignOut()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
            if (accountText != null) accountText.text = "Not Logged In";
            popupText.SetText("Sign-out Successful");
            popupText.ShowPopup();
        }
    }
}
