using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 /// <summary>
 /// Omogucava funkcionalnosti socijalnim dugmicima
 /// </summary>
public class SettingsCtrl : MonoBehaviour
{
    public string privacyURL, tutorialURL, googlePlusURL, ratingURL;


    public void FacebookLike()
    {
        Application.OpenURL(privacyURL);
    }

    public void TwitterFollow()
    {
        Application.OpenURL(tutorialURL);
    }

    public void GooglePlus()
    {
        Application.OpenURL(googlePlusURL);
    }

    public void Rating()
    {
        Application.OpenURL(ratingURL);
    }

    public void OpenLeaderboards()
    {
        LeaderboardCtrl.instance.OpenLeaderboard();
    }

    public void OpenAchievments()
    {
        AchievementCtrl.instance.ShowAchievements();
    }
}
