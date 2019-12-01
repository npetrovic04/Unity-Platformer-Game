using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AchievementCtrl : MonoBehaviour
{
    public static AchievementCtrl instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        //PlayGamesPlatform.Activate();
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool succes) =>
            {

            });
        }
    }

	// Use this for initialization
	void Start () {
        PlayGamesPlatform.Activate();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Login()
    {

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                PlayGamesPlatform.Instance.ShowAchievementsUI();
            }
            else
            {
                Debug.Log("User could't authenticate");
            }
        });
    }

    public void ShowAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            Login();
        }
    }

    public void CheckAchievements()
    {
        if (GameCtrl.instance.data.score > 100)
        {
            Social.ReportProgress(GPGSIds.achievement_fifth_place, 100f, (bool success) =>
            {

            });
        }
        if (GameCtrl.instance.data.score > 200)
        {
            Social.ReportProgress(GPGSIds.achievement_fourth_place, 100f, (bool success) =>
            {

            });
        }
        if (GameCtrl.instance.data.score > 300)
        {
            Social.ReportProgress(GPGSIds.achievement_third_place, 100f, (bool success) =>
            {

            });
        }
        if (GameCtrl.instance.data.score > 400)
        {
            Social.ReportProgress(GPGSIds.achievement_second_place, 100f, (bool success) =>
            {

            });
        }
        if (GameCtrl.instance.data.score > 500)
        {
            Social.ReportProgress(GPGSIds.achievement_first_place, 100f, (bool success) =>
            {

            });
        }

    }
}
