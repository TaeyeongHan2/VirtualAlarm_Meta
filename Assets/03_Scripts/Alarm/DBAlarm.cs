using System.Collections.Generic;
using _03_Scripts.Alarm;
using UnityEngine;

public class DBAlarm : MonoBehaviour
{
    public static DBAlarm Instance { get; private set; }
    public List<AlarmBase> alarmDataList;
    public AlarmBase currentAlarmData;
    public int enabledAlarmTimeAsMinutes;
    public string wakeUpStatus;
    
    public Color pastelPink;


    public void InitWakeUpStatus()
    {
        wakeUpStatus = null;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        pastelPink = new Color(1.0f, 0.7122642f, 0.7565312f);
    }

    private void Start()
    {
        alarmDataList = UIManager.Instance.alarmDataList;
    }
}