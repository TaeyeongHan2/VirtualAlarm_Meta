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
        dateText.text  = dayNumber.ToString();
        
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log($"날짜 버튼 클릭됨: {dayNumber}일");
        calenderManager.OnDayButtonClicked(this); 
        Debug.Log("팝업이 열렸습니다. ");
    }

    public int GetDay()
    {
        return dayNumber;
    }
}
