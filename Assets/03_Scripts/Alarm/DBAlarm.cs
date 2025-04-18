using System.Collections.Generic;
using _03_Scripts.Alarm;
using UnityEngine;


public class DBAlarm : MonoBehaviour
{
    public static DBAlarm instance { get; private set; }
    
    public Dictionary<GameObject,AlarmBase> alarmDataDict = new Dictionary<GameObject, AlarmBase>();
    
    private void Awake()
    {
        instance = this;
    }
    
}