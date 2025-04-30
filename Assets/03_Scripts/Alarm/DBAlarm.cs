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
    }

    private void Start()
    {
        alarmDataList = UIManager.Instance.alarmDataList;
    }
}