using System;
using System.Collections.Generic;
using System.Text;
using _03_Scripts.Alarm;
using UnityEngine;

public class AlramDataGenerator : MonoBehaviour
{
    public static AlramDataGenerator instance{get; private set;}
    public UIReference uiReference;

    public string alram24Time;

    private void Awake()
    {
        instance = this; // 싱글톤용
        uiReference = UIReference.instance;
    }
    void Start()
    {
        
    }

    public AlarmBase CreateAlarmData()
    {
        AlarmBase setting = new AlarmBase();
        setting.alarm24Hour = int.Parse(DateTime.Now.ToString("HH"));
        setting.alarm24Minute = int.Parse(DateTime.Now.ToString("mm"));

        StringBuilder sb24Time = new StringBuilder();
        sb24Time.Append(setting.alarm24Hour);
        sb24Time.Append(setting.alarm24Minute);
        setting.alarm24Time = sb24Time.ToString();
        
        setting.alarm12tt = DateTime.Now.ToString("tt");
        setting.alarm12HH = DateTime.Now.ToString("hh");
        setting.alarm12mm = DateTime.Now.ToString("mm");
        setting.alarm12time = uiReference.ClockTimeSetButtonText.text;

        setting.isBellOn = BellTurnOnOffButton.Instance.isActive;
        setting.isAUTOQuitOn = AutoQuitAlarmButton.Instance.isActive;
        setting.AUTOQuitMinutes = AutoQuitTimerButton.Instance.minutes;
            
        setting.alarmRepeatDays = new Dictionary<string, bool>();
        setting.alarmRepeatDays["월"] = MondayButton.Instance.isActive;
        setting.alarmRepeatDays["화"] = TuesdayButton.Instance.isActive;
        setting.alarmRepeatDays["수"] = WednesdayButton.Instance.isActive;
        setting.alarmRepeatDays["목"] = ThursdayButton.Instance.isActive;
        setting.alarmRepeatDays["금"] = FridayButton.Instance.isActive;
        setting.alarmRepeatDays["토"] = SaturdayButton.Instance.isActive;
        setting.alarmRepeatDays["일"] = SundayButton.Instance.isActive;

        return setting;
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
