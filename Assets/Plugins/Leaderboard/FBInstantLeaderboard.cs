using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class FBInstantLeaderboard : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SetScoreOnLeaderboard(string leaderboardName, int score, string extraData);

    [DllImport("__Internal")]
    private static extern void GetLeaderboard(string leaderboardName);

    [DllImport("__Internal")]
    private static extern void GetContextID(string name);

    public static void SetScore(string leaderboardName , int score , string extraData)
    {
        SetScoreOnLeaderboard(leaderboardName, score, extraData);
    }

    public static void GetLeaderboardData(string leaderboardName)
    {
        GetLeaderboard(leaderboardName);
    }
    
    public static void GetLeaderboardID(string name)
    {
        GetContextID(name);
    }
}
