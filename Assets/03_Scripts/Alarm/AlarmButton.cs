using System;
using _03_Scripts.Alarm;
using TMPro;
using UnityEngine;

public class AlarmButton : MonoBehaviour
{
    public TMP_Text textUI;

    private void Awake()
    {
        textUI = GetComponentInChildren<TMP_Text>();
    }

    public void SetData(AlarmBase alarmBase)
    {
        textUI.text = $"{alarmBase.alarm12time}";
    }
}
