using System.Collections.Generic;
using _03_Scripts.Alarm;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get;private set; }
    public UIReference uiReference; // 캐싱
    public List<GameObject> homePageAlarmsButtons = new List<GameObject>();
    public List<AlarmBase> alarmDataList = new List<AlarmBase>();


    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        uiReference = UIReference.instance; // 싱글톤 인스턴스를 캐싱
    }
    
    public void InitNewAlarm()
    {
        var newAlarmButton = Instantiate(uiReference.HomeAlarmButtonPrefab, uiReference.HomeAlarmListRoot.transform);
        // todo : 실제 알람 데이터 생성
        AlarmBase alarmData = AlramDataGenerator.instance.CreateAlarmData();
        AlarmButton alarmButton = newAlarmButton.GetComponent<AlarmButton>();
        alarmButton.SetData(alarmData);
        
        homePageAlarmsButtons.Add(newAlarmButton);
        alarmDataList.Add(alarmData);
    }

    public void DeleteAlarm(GameObject alarmObject, AlarmBase alarmData)
    {
        if (homePageAlarmsButtons.Contains(alarmObject))
        {
            homePageAlarmsButtons.Remove(alarmObject);
        }
    
        if (alarmDataList.Contains(alarmData))
        {
            alarmDataList.Remove(alarmData);
        }
    
        Debug.Log($"알람 수: {homePageAlarmsButtons.Count}, 데이터 수 : {alarmDataList.Count}");
    }

    void Update()
    {
        
    }
}
