using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using FBInstant;

public class Test : MonoBehaviour
{

    public UnityEngine.UI.Text coin;

    public void fortyEight(string id)
    {
        FBInstantIAP.PurchaseProduct(id, AwardFortyEight);
    }

    private void AwardFortyEight()
    {
        Debug.Log("awarding 48000 coins");
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 48000);
        coin.text = PlayerPrefs.GetInt("Coins").ToString();
    }


    public void SeventyTwo(string id)
    {
        FBInstantIAP.PurchaseProduct(id, AwardSeventyTwo);
    }

    private void AwardSeventyTwo()
    {
        Debug.Log("awarding 7200 coins");
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 7200);
        coin.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void ThirtyFour(string id)
    {
        FBInstantIAP.PurchaseProduct(id, AwardThirtyFour);
    }

    private void AwardThirtyFour()
    {
        Debug.Log("awarding 3400 coins");
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 3400);
        coin.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void leaderboardTest()
    {
        FBInstantLeaderboard.SetScore("Tournament", 20, "");
    }

    public void GetLeaderboard(string name)
    {
        FBInstantLeaderboard.GetLeaderboardData(name);
    }

    public void GetContextID(string name)
    {
        FBInstantLeaderboard.GetLeaderboardID(name);
    }
}


