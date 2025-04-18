using System;
using System.Collections.Generic;
using System.Text;
using _03_Scripts.Alarm;
using Unity.VisualScripting;
using UnityEngine;

public class TimeUtile : MonoBehaviour
{
    public static TimeUtile instance { get; private set; }

    void Awake()
    {
        instance = this;
    }
/*
    public string GetCurrent24Time()
    {
        var currentTime = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        Debug.Log(currentTime); // 예시) Friday, 18 April 2025 04:37:53
        var currentTimeTokens = currentTime.Split(' ');
        Debug.Log(currentTimeTokens[0]);
        Debug.Log(currentTimeTokens[1]);
        Debug.Log(currentTimeTokens[2]);
        Debug.Log(currentTimeTokens[3]);
        Debug.Log(currentTimeTokens[4]);
        var currentDayOfWeek = currentTimeTokens[0].Replace(",", "");
        Debug.Log(currentDayOfWeek);
        var current24TimeTokens = currentTimeTokens[4].Split(':');
        var current24Hour = current24TimeTokens[0];
        Debug.Log(current24Hour);
        var current24Minute = current24TimeTokens[1];
        Debug.Log(current24Minute);
        var  current24Second = current24TimeTokens[2];
        Debug.Log(current24Second);
        
        
        StringBuilder sb24Time = new StringBuilder();
        sb24Time.Append(current24Hour);
        sb24Time.Append(current24Minute);
        var alarm24Time = sb24Time.ToString();
        return alarm24Time;
    }
    // 현 시간 딕셔너리로 반환
    public static Dictionary<string, int> GetCurrentTimeDict()
    {
        var currentTime = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        Debug.Log(currentTime); // 예시) Friday, 18 April 2025 04:37:53
        var currentTimeTokens = currentTime.Split(' ');
        Debug.Log(currentTimeTokens[0]);
        Debug.Log(currentTimeTokens[1]);
        Debug.Log(currentTimeTokens[2]);
        Debug.Log(currentTimeTokens[3]);
        Debug.Log(currentTimeTokens[4]);
        var currentDayOfWeek = currentTimeTokens[0].Replace(",", "");
        Debug.Log(currentDayOfWeek);
        var current24TimeTokens = currentTimeTokens[4].Split(':');
        var current24Hour = current24TimeTokens[0];
        Debug.Log(current24Hour);
        var current24Minute = current24TimeTokens[1];
        Debug.Log(current24Minute);
        var  current24Second = current24TimeTokens[2];
        Debug.Log(current24Second);
        
        Dictionary<string, int> current24TimeInfo = new Dictionary<string, int>();
        current24TimeInfo["Hour"] =  int.Parse(current24Hour);
        current24TimeInfo["Minute"] = int.Parse(current24Minute);
        //current24TimeInfo["Second"] = int.Parse(current24Second);
        
        Debug.Log(current24TimeInfo["Hour"]);
        Debug.Log(current24TimeInfo["Minute"]);
        //Debug.Log(current24TimeInfo["Second"]);

        return current24TimeInfo;

    }*/
}
