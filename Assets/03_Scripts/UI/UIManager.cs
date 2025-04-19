using System.Collections.Generic;
using _03_Scripts.Alarm;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get;private set; }
    public UIReference uiReference; // 캐싱
    public List<GameObject> homePageAlarmsButtons = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        uiReference = UIReference.instance; // 싱글톤 인스턴스를 캐싱
    }
    
    // 새 알람 생성 매소드
    public void InitNewAlarm()
    {
        var newAlarmButton = Instantiate(uiReference.HomeAlarmButtonPrefab, uiReference.HomeAlarmListRoot.transform);
        // todo : 실제 알람 데이터 생성
        AlarmBase alarmBase = AlramDataGenerator.instance.CreateAlarmData();
        AlarmButton alarmButton = newAlarmButton.GetComponent<AlarmButton>();
        alarmButton.SetData(alarmBase);
        
        homePageAlarmsButtons.Add(newAlarmButton);
    }

    public void DeleteAlarm(GameObject alarm)
    {
        // todo :실제 알람 데이터 삭제 부분
        
        
    }

    void Update()
    {
        
    }
}
