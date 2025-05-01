using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class CalenderManager : MonoBehaviour
{
    public GameObject dateButtonPrefab;
    public GameObject emptyButtonPrefab;
    public Transform dateGrid;
    
    //현재 선택된 날짜
    private DateButton selectedDate;
    //PopupPanel오브젝트
    public GameObject popupPanel;
    //popupPanel 내의 날짜 표시용 텍스트
    public TextMeshProUGUI popupDateText;
    
    // 알람 설정 시간 표시용
    public TextMeshProUGUI settedTimeText;
    // 실제 기상 시간 표시용
    public TextMeshProUGUI wakeupTimeText;   

    void Start()
    {
        GenerateCalendar();
    }

    public void Awake()
    {
        popupPanel.SetActive(false);
    }
    
    public void GenerateCalendar()
    {
        int totalDays = 31;

        for (int i = 1; i <= totalDays; i++)
        {
            GameObject obj = Instantiate(dateButtonPrefab, dateGrid);
            DateButton btn = obj.GetComponent<DateButton>();
            
            btn.dateText = obj.GetComponentInChildren<TextMeshProUGUI>();
            btn.calenderManager = this;
            btn.Init(i, this);
        }
    }
    
    public void OnDayButtonClicked(DateButton clickedDate)
    {
        selectedDate = clickedDate;
        int clickedDay = selectedDate.GetDay();

        OpenPopup(clickedDay);
    }
    
    //팝업을 열음
    public void OpenPopup(int day)
    {
        int year = 2025;
        int month = 5;
        Debug.Log($"[CalenderManager] OpenPopup 호출됨 {day}일");
        popupPanel.SetActive(true);
       
        popupDateText.text = $"{year}.{month:00}.{day:00}";
        
        settedTimeText.text = $"설정한 알람 시간: {TimeData.GetSettedTime(year, month, day)}";
        wakeupTimeText.text = $"일어난 시간: {TimeData.GetWakeupTime(year, month, day)}";
    }
    
    //팝업을 닫음
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
