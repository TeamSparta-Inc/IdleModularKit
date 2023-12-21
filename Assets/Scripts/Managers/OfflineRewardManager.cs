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

    // 데이터 로드 메서드
    private void LoadData()
    {
        if (ES3.KeyExists("LAST_LOGIN"))
            totalAccumulatedTime = ES3.KeyExists("TOTAL_ACCUMULATED_TIME") ? ES3.Load<TimeSpan>("TOTAL_ACCUMULATED_TIME") : new TimeSpan();
        else
            totalAccumulatedTime = new TimeSpan();
    }

    // 누적 시간의 한계를 설정하는 메서드
    private void LimitAccumulationTime()
    {
        if (totalAccumulatedTime.TotalMinutes > MAX_ACCUMULATION_TIME_IN_MINUTES)
            totalAccumulatedTime = TimeSpan.FromMinutes(MAX_ACCUMULATION_TIME_IN_MINUTES);
    }

    // 현재 누적 보상 시간을 문자열로 반환하는 메서드
    public string GetCurrentRewardTimeAsString()
    {
        return $"{totalAccumulatedTime.Days} Days {totalAccumulatedTime.Hours} Hours {totalAccumulatedTime.Minutes} Minutes {totalAccumulatedTime.Seconds} Seconds";
    }

    // 현재 누적 보상 시간을 TimeSpan으로 반환하는 메서드
    public TimeSpan GetCurrentRewardTime() => totalAccumulatedTime;

    // 사용자가 보상을 청구했는지 확인하는 메서드
    private bool DidUserClaimReward()
    {
        return ES3.KeyExists("CLAIMED_REWARD") && ES3.Load<bool>("CLAIMED_REWARD");
    }

    // 보상을 청구하는 메서드
    public void ClaimReward()
    {
        ES3.Save<bool>("CLAIMED_REWARD", true);
        totalAccumulatedTime = new TimeSpan();
    }

    // 애플리케이션이 종료될 때 호출되는 메서드
    private void OnApplicationQuit()
    {
        ES3.Save<DateTime>("LAST_LOGIN", DateTime.Now);
        ES3.Save<TimeSpan>("TOTAL_ACCUMULATED_TIME", totalAccumulatedTime);
        if (DidUserClaimReward())
            ES3.Save<bool>("CLAIMED_REWARD", false);
    }
}