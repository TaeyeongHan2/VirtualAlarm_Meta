using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

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
    //일찍 일어난 경우: 연보라
    public Color earlyColor;
    //3분이내에 일어난 경우: 연두
    public Color onTimeColor;
    //늦잠잔 경우: 연주황
    public Color lateColor;
    
    // 알람 설정 시간 표시용
    public TextMeshProUGUI settedTimeText;
    // 실제 기상 시간 표시용
    public TextMeshProUGUI wakeupTimeText;   


    private Dictionary<int, DateButton> dateButtons = new();

    public void Awake()
    {
        popupPanel.SetActive(false);
    }
    
    
    public void OnDayButtonClicked(DateButton clickedDate)
    {
        //이전에 선택된 버튼 색 초기화
        if (selectedDate != null)
        {
            selectedDate.SetCircleColor(new Color(1, 1, 1, 0));
        }
        //새로 클릭한 버튼 색 변경
        selectedDate = clickedDate;
        selectedDate.SetCircleColor(GetColorForDay(selectedDate.GetDay()));

        //버튼에 저장된 날짜 정보를 가져옴
        int clickedDay = selectedDate.GetDay();
        //팝업 열기
        if (selectedDate != null)
        {
            OpenPopup(clickedDay);
            
        }
        
    }

    private Color GetColorForDay(int day)
    {
        //설정한 알람 시각과 일어난 시각을 비교해서 색상을 리턴하는 로직 필요
        if (day == 0)
        {
            return earlyColor;
        }
        return onTimeColor;
    }
    
    //팝업을 열음
    public void OpenPopup(int day)
    {
        popupPanel.SetActive(true);
        popupDateText.text = $"2025.05.{selectedDate.GetDay():00}";
    }
    
    //팝업을 닫음
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
    
    //팝업 내부에 해당 날짜의 알람 설정 시각과
    //사용자가 AR버튼을 누른(일어난 시각)을 표시해줌
    public void GetWakeupTime(int day)
    {
        
    }
    //AR 버튼이 눌리면 사용자의 일어난 시각을 불러와서 wakeuptime에 표시
    public void GetSettedTime(int day)
    {
        
    }
    
}
