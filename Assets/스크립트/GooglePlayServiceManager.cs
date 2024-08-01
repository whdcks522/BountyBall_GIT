using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayServiceManager : MonoBehaviour
{
    static private GooglePlayServiceManager instance;
    public static GooglePlayServiceManager Instance {
        get {
            if(instance == null)
            {
                instance = FindObjectOfType<GooglePlayServiceManager>();
                if (instance == null) {
                    instance = new GameObject("Google Play Service")
                        .AddComponent<GooglePlayServiceManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
            .Builder().EnableSavedGames().Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = false;

        PlayGamesPlatform.Activate();
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) => { if (!success) { Debug.Log("Login Fail"); } });
    }

    public bool isAuthenticated {//로그인이 됫나 확인
        get {
            return Social.localUser.authenticated;
        }
    }

    public void Completeshock_and_awe() {
        if (!isAuthenticated) {
            Login();
            return;
        }
        Social.ReportProgress
            (GPGSIds.achievement_shock_and_awe, 100.0, (bool success) => 
            { if (!success) { Debug.Log("Report Fail"); } });
    }

    public void ShowAchievementUI() {
        if (!isAuthenticated)
        {
            Login();
            return;
        }
        Social.ShowAchievementsUI();
    }
}
