using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UINavigator : MonoBehaviour
{
    public static UINavigator instance{get; private set;}
    
    public GameObject[] pagePrefabsArray;
    public GameObject currentPage;
    public int currentPageIndex;

    public List<GameObject> homePageAlarmsButtons = new List<GameObject>();
    
    public UIReference uiReference; // 캐싱
    
    private void Awake()
    {
        instance = this; // 싱글톤용
        
        uiReference = UIReference.instance;
        pagePrefabsArray = new GameObject[5];
        pagePrefabsArray[0] = uiReference.viewTitlePrefab;
        pagePrefabsArray[1] = uiReference.viewHomePrefab;
        pagePrefabsArray[2] = uiReference.viewAlarmSettingPrefab;
        pagePrefabsArray[3] = uiReference.viewTimeSetPrefab;
        pagePrefabsArray[4] = uiReference.viewTimeRingingPrefab;

        if (currentPage != null)
        {
            currentPage = pagePrefabsArray[0];
        }
        currentPage.SetActive(true);

    }

    // 새 알람 생성 매소드
    public void InitNewAlarm()
    {
        var newAlarmButton =Instantiate(uiReference.HomeAlarmButtonPrefab, uiReference.HomeAlarmListRoot.transform);
        // todo : 실제 알람 데이터 생성
        AlramDataGenerator.instance.CreateTempAlarmData();
        //DBAlarm.instance.D
        
        homePageAlarmsButtons.Add(newAlarmButton);
    }

    public void DeleteAlarm(GameObject alarm)
    {
        // todo :실제 알람 데이터 삭제 부분
        
        
    }
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        ChangePage(1);

    }

    public void ChangePage(int pageIndex)
    {
        var lastPage = currentPage;
        lastPage.SetActive(false);
        
        currentPage = pagePrefabsArray[pageIndex];
        currentPage.SetActive(true);
        
        // 현재 페이지 인덱스 저장
        currentPageIndex = pageIndex;
        
        
    }

 
    
    
    void Update()
    {
        
    }
}
