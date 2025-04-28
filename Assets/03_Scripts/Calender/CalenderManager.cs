using System.Collections.Generic;
using TMPro;
using Unity.Mathematics.Geometry;
using UnityEngine;

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
        popupDateText.text = $"2025.05.{selectedDate.GetDay(): 00}";
    }
    
    //팝업을 닫음
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
    
}
