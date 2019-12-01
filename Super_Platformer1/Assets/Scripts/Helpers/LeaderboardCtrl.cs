using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LeaderboardCtrl : MonoBehaviour
{

    /*public static LeaderboardCtrl instance;

    //private const string LeaderboardID = "CgkIk76Mj7AGEAIQAA";

    void Awake()
    {
        MakeSingleton();

        PlayGamesPlatform.Activate();
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool succes) =>
            {

            });
        }
    }

    void MakeSingleton()
    {
        //if(instance != null)
        //{
        //    Destroy(this);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        if (instance == null)
            instance = this;
    }

    public void OpenLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderboardID);
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderboardID);
                }
                else
                {
                    Debug.Log("User could't authenticate");
                }
            });
        }
    }

    public void PostScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, LeaderboardID, (bool success) =>
            {

            });
        }
    }*/

    // -------------------------------------------------------------------------------------------------------------------

    /* public static LeaderboardCtrl instance;

     private const string LeaderboardID = "CGkIk76Mj7AGEAIQAA";

     void Awake()
     {
         PlayGamesPlatform.Activate();
         if (!Social.localUser.authenticated)
         {
             Social.localUser.Authenticate((bool succes) =>
             {

             });
         }
         if (instance == null)
         {
             instance = this;
         }
     }

     void Start()
     {
         PlayGamesPlatform.Activate();
         Login();
     }

     public void Login()
     {
         Social.localUser.Authenticate((bool success) =>
         {
             if (success)
             {
                 PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderboardID);
             }
             else
             {
                 Debug.Log("User could't authenticate");
             }
         });
     }

     public void AddScoreToLeaderboard()
     {
         if (Social.localUser.authenticated)
         {
             Social.ReportScore(GameCtrl.instance.data.score, LeaderboardID, (bool success) =>
         {

         });
         }
     }

     public void ShowLeaderboard()
     {
         // Social.ShowLeaderboardUI();

         if (Social.localUser.authenticated)
         {
             PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderboardID);
             ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(LeaderboardID);
         }
         else
         {
             Login();
         }
     }*/
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static LeaderboardCtrl instance;

    private const string LeaderboardID = "CgkIk76Mj7AGEAIQAA";

    void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        PlayGamesPlatform.Activate();
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //if (instance == null)
        //    instance = this;
    }

    public void OpenLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderboardID);
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderboardID);
                }
                else
                {
                    Debug.Log("User could't authenticate");
                }
            });
        }
    }

    public void LogInOrLogOutGoogleleaderboard()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                
            });
        }
    }
    public void ReportScore(int score)
    {
        Social.ReportScore(score, LeaderboardID, (bool success) =>
            {

            });
    }

}
