using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class CalenderManager : MonoBehaviour
{
    public GameObject dateButtonPrefab;
    public GameObject emptyButtonPrefab;
    public Transform dateGrid;
    
    public GameObject popupPanel;
    public Color earlyColor;
    public Color onTimeColor;
    public Color lateColor;

    private Dictionary<int, DateButton> dateButtons = new();

    private void Start()
    {
        // 1. 앞에 빈칸 4개 (일, 월, 화, 수)
        for (int i = 0; i < 4; i++)
        {
            Instantiate(emptyButtonPrefab, dateGrid);
        }
        
        //DateButton들을 전부 등록
        for (int i = 1; i <= 31; i++)
        {
            GameObject dateButton = Instantiate(dateButtonPrefab, dateGrid);
            DateButton button = dateButton.GetComponent<DateButton>();
            button.Init(i, this);
        }
    }

    public void OnDateSelected(int day)
    {
        //1, 기존 선택된 것을 초기화
        foreach (var button in dateButtons.Values)
        {
            button.SetCircleColor(new Color(1, 1, 1,0));
        }
        //2. 해당 날짜 버튼 활성화
        DateButton selectedButton = dateButtons[day];
        selectedButton.SetCircleColor(GetWakeupColor(day));
        
        //3. 팝업 띄우기
        popupPanel.SetActive(true);
        
        //4. Wakeup 시간, Setted 시간 표시
        //여기서는 리스트에서 날짜별 Wakeup 시간 꺼내오기

        
    }
    
    //설정한 알람 시간과 일어난 시간을 비교해서 색을 결정
    private Color GetWakeupColor(int day)
    {
        double diffMinutes = GetWakeUpDiffMinutes(day);
        if(diffMinutes <= -3) return earlyColor;
        //if(Math) return onTimeColor;
        return lateColor;
    }

    //설정된 알람 시간과 일어난 시간 정보를 가져와서 시간차를 계산하는 함수
    private double GetWakeUpDiffMinutes(int day)
    {
        return 2;
    }
}
