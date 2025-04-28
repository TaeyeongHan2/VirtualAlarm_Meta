using TMPro;
using UnityEngine;
using UnityEngine.UI;

//날짜를 눌렀을 때 해당 날짜의 정보를 불러옴
public class DateButton : MonoBehaviour
{
    
    public Image circleIcon;
    public TextMeshProUGUI dateText;
    public CalenderManager calenderManager;
    
    //해당하는 날짜
    [HideInInspector]
    public int dayNumber;

    public void Init(int day, CalenderManager manager)
    {
        dayNumber = day;
        calenderManager = manager;
        dateText.text = day.ToString();
        //초기의 날짜 버튼 아이콘은 투명색임
        circleIcon.color = new Color(1, 1, 1, 0);
    }

    public void OnClick()
    {
        Debug.Log($"날짜 버튼 클릭됨: {dayNumber}일");
        calenderManager.OnDayButtonClicked(this);
    }

    public void SetCircleColor(Color color)
    {
        circleIcon.color = color;
    }

    public int GetDay()
    {
        return dayNumber;
    }
}
