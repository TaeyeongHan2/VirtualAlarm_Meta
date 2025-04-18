using System.Collections.Generic;
using _03_Scripts.Alarm;
using UnityEngine;


public class DBAlarm : MonoBehaviour
{
    public static DBAlarm instance { get; private set; }
    //  /알람 버튼 프리팹 /알람 정보/ 맵핑용 
    public Dictionary<GameObject,AlarmBase> alarmDataDict = new Dictionary<GameObject, AlarmBase>();
    
    private void Awake()
    {
        instance = this;
    }
    
}