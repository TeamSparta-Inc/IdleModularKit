using System;
using UnityEngine;

public class OfflineRewardManager : MonoBehaviour
{
    private TimeSpan totalAccumulatedTime;
    private const double MAX_ACCUMULATION_TIME_IN_MINUTES = 24 * 60;  // 24 hours in minutes

    void Start()
    {
        LoadData();

        if (!DidUserClaimReward())
        {
            DateTime lastLogIn = ES3.Load<DateTime>("LAST_LOGIN");
            TimeSpan ts = DateTime.Now - lastLogIn;

            totalAccumulatedTime += ts;

            LimitAccumulationTime();

            Debug.Log(GetCurrentRewardTimeAsString());
            Debug.Log((int)ts.TotalMinutes + "g");
        }
        else
        {
            Debug.Log("WELCOME");
            Debug.Log("0g");
        }
    }

    private void LoadData()
    {
        if (ES3.KeyExists("LAST_LOGIN"))
            totalAccumulatedTime = ES3.KeyExists("TOTAL_ACCUMULATED_TIME") ? ES3.Load<TimeSpan>("TOTAL_ACCUMULATED_TIME") : new TimeSpan();
        else
            totalAccumulatedTime = new TimeSpan();
    }

    private void LimitAccumulationTime()
    {
        if (totalAccumulatedTime.TotalMinutes > MAX_ACCUMULATION_TIME_IN_MINUTES)
            totalAccumulatedTime = TimeSpan.FromMinutes(MAX_ACCUMULATION_TIME_IN_MINUTES);
    }

    public string GetCurrentRewardTimeAsString()
    {
        return $"{totalAccumulatedTime.Days} Days {totalAccumulatedTime.Hours} Hours {totalAccumulatedTime.Minutes} Minutes {totalAccumulatedTime.Seconds} Seconds";
    }

    public TimeSpan GetCurrentRewardTime() => totalAccumulatedTime;

    private bool DidUserClaimReward()
    {
        return ES3.KeyExists("CLAIMED_REWARD") && ES3.Load<bool>("CLAIMED_REWARD");
    }

    public void ClaimReward()
    {
        ES3.Save<bool>("CLAIMED_REWARD", true);
        totalAccumulatedTime = new TimeSpan();
    }

    private void OnApplicationQuit()
    {
        ES3.Save<DateTime>("LAST_LOGIN", DateTime.Now);
        ES3.Save<TimeSpan>("TOTAL_ACCUMULATED_TIME", totalAccumulatedTime);
        if (DidUserClaimReward())
            ES3.Save<bool>("CLAIMED_REWARD", false);
    }
}