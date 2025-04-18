using System;
using System.Collections.Generic;
using System.Text;
using _03_Scripts.Alarm;
using UnityEngine;

public class AlramDataGenerator : MonoBehaviour
{
    public static AlramDataGenerator instance{get; private set;}

    public AlarmBase tempAlarmSetting = new AlarmBase();

    public string alram24Time;

    private void Awake()
    {
        instance = this; // 싱글톤용
    }
    void Start()
    {
        
    }

    public void CreateTempAlarmData()
    {
        
        tempAlarmSetting.alarm24Hour = int.Parse(DateTime.Now.ToString("HH"));
        tempAlarmSetting.alarm24Minute = int.Parse(DateTime.Now.ToString("mm"));

        StringBuilder sb24Time = new StringBuilder();
        sb24Time.Append(tempAlarmSetting.alarm24Hour);
        sb24Time.Append(tempAlarmSetting.alarm24Minute);
        tempAlarmSetting.alarm24Time = sb24Time.ToString();
        
        tempAlarmSetting.alarm12tt = DateTime.Now.ToString("tt");
        tempAlarmSetting.alarm12HH = DateTime.Now.ToString("HH");
        tempAlarmSetting.alarm12mm = DateTime.Now.ToString("mm");
        tempAlarmSetting.alarm12time = DateTime.Now.ToString("tt HH:mm");

        tempAlarmSetting.isBellOn = true;
        tempAlarmSetting.isAUTOQuitOn = false;
        
        tempAlarmSetting.alarmRepeatDays = new Dictionary<string, bool>();
        tempAlarmSetting.alarmRepeatDays["Monday"] = false;
        tempAlarmSetting.alarmRepeatDays["Tuesday"] = false;
        tempAlarmSetting.alarmRepeatDays["Wednesday"] = false;
        tempAlarmSetting.alarmRepeatDays["Thursday"] = false;
        tempAlarmSetting.alarmRepeatDays["Friday"] = false;
        tempAlarmSetting.alarmRepeatDays["Saturday"] = false;
        tempAlarmSetting.alarmRepeatDays["Sunday"] = false;

    }

    public void saveAlarmData(AlarmBase alarm)
    {
        
    }

    public void SwitchBellorVIBE()
    {
        
    }

    void Update()
    {
        
    }
}
