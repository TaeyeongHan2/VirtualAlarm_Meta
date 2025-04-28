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

    // private void Start()
    // {
    //     // 1. 앞에 빈칸 4개 (일, 월, 화, 수)
    //     for (int i = 0; i < 4; i++)
    //     {
    //         Instantiate(emptyButtonPrefab, dateGrid);
    //     }
    //     
    //     //DateButton들을 전부 등록
    //     for (int i = 1; i <= 31; i++)
    //     {
    //         GameObject dateButton = Instantiate(dateButtonPrefab, dateGrid);
    //         DateButton button = dateButton.GetComponent<DateButton>();
    //         button.Init(i, this);
    //     }
    // }

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

        //팝업 열기
        popupPanel.SetActive(true);
        popupDateText.text = $"2025.05.{selectedDate.GetDay(): 00}";
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
    
    // public void OnDateSelected(int day)
    // {
    //     //1, 기존 선택된 것을 초기화
    //     foreach (var button in dateButtons.Values)
    //     {
    //         button.SetCircleColor(new Color(1, 1, 1,0));
    //     }
    //     //2. 해당 날짜 버튼 활성화
    //     DateButton selectedButton = dateButtons[day];
    //     selectedButton.SetCircleColor(GetWakeupColor(day));
    //     
    //     //3. 팝업 띄우기
    //     popupPanel.SetActive(true);
    //     
    //     //4. Wakeup 시간, Setted 시간 표시
    //     //여기서는 리스트에서 날짜별 Wakeup 시간 꺼내오기
    //
    //     
    // }
    //
    // //설정한 알람 시간과 일어난 시간을 비교해서 색을 결정
    // private Color GetWakeupColor(int day)
    // {
    //     double diffMinutes = GetWakeUpDiffMinutes(day);
    //     if(diffMinutes <= -3) return earlyColor;
    //     //if(Math) return onTimeColor;
    //     return lateColor;
    // }
    //
    // //설정된 알람 시간과 일어난 시간 정보를 가져와서 시간차를 계산하는 함수
    // private double GetWakeUpDiffMinutes(int day)
    // {
    //     return 2;
    // }
}
