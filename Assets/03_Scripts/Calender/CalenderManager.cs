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


    //private Dictionary<int, DateButton> dateButtons = new();

    public void Awake()
    {
        popupPanel.SetActive(false);
    }
    
    
    public void OnDayButtonClicked(DateButton clickedDate)
    {
        //버튼에 저장된 날짜 정보를 가져옴
        int clickedDay = selectedDate.GetDay();
        //팝업 열기
        if (selectedDate != null)
        {
            OpenPopup(clickedDay);
            popupDateText.text = clickedDay.ToString();
        }
    }
    
    //팝업을 열음
    public void OpenPopup(int day)
    {
        int year = 2025;
        int month = 5;
        popupPanel.SetActive(true);
       
        popupDateText.text = $"{year}.{month:00}.{day:00}";
        
        settedTimeText.text = $"Setted Time: {TimeData.GetSettedTime(year, month, day)}";
        wakeupTimeText.text = $"WakeUp Time: {TimeData.GetWakeupTime(year, month, day)}";
    }
    
    //팝업을 닫음
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
    
    //팝업 내부에 해당 날짜의 알람 설정 시각과
    //사용자가 AR버튼을 누른(일어난 시각)을 표시해줌
    public void GetSettedTime(int day)
    {
        //현재 설정된 알람 리스트 불러오기
        var alarmList = UIManager.Instance.alarmDataList;
        if (alarmList == null || alarmList.Count == 0)
        {
            Debug.Log("설정한 알람이 없음");
            return;
        }
        
    }
    public void GetWakeupTime(int day)
    {
        DateTime now = DateTime.Now;
        
    }
    
}
