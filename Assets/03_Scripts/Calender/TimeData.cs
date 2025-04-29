using UnityEngine;

//사용자가 설정한 알람 시각과 AR버튼을 누른시각(기상시각) 정보를 저장할 PlayerPrefabs
public class TimeData : MonoBehaviour
{
    public static void SaveDayRecord(int year, int month, int day, string settedTime, string wakeupTime)
    {
        string prefix = $"{year}_{month:00}_{day:00}_";
        PlayerPrefs.SetString(prefix + "setted", settedTime);
        PlayerPrefs.SetString(prefix + "wakeup", wakeupTime);
        PlayerPrefs.Save();
    }

    public static string GetSettedTime(int year, int month, int day)
    {
        string key = $"{year}_{month:00}_{day:00}_setted";
        return PlayerPrefs.GetString(key, "-");
    }

    public static string GetWakeupTime(int year, int month, int day)
    {
        string key = $"{year}_{month:00}_{day:00}_wakeup";
        return PlayerPrefs.GetString(key, "-");
    }

    public static void ClearAllRecords() // 필요 시 전체 삭제용
    {
        PlayerPrefs.DeleteAll();
    }
}
