using System;
using UnityEngine;

public class AlrarmSettingContext : MonoBehaviour
{
    private void OnEnable()
    {
        AlramDataGenerator.instance.CreateAlarmData();
        //UIReference.instance.ClockTimeSetButtonText.text = DateTime.Now.ToString("tt hh:mm");
        //UIReference.instance.ClockTimeSetButtonText.text = AlramDataGenerator.instance.tempAlarmSetting.alarm12time;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
