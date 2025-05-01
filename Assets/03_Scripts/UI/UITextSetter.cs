using System;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UITextSetter : MonoBehaviour
{
    public static UITextSetter instance{get; private set;}
    public UIReference uiReference; // 캐싱
    public AlramDataGenerator alramDataGenerator;
    public TimeUtile timeUtile;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        uiReference = UIReference.instance; // 싱글톤 인스턴스를 캐싱
    }



    public void SetClockTimeSetButtonText()
    {
        string tt = AMPMScrollView.Instance.AMorPM.TrimEnd();
        string hh = HoursScrollView.Instance.hours.TrimEnd();
        string mm = MINScrollView.Instance.minute.TrimEnd();
        uiReference.ClockTimeSetButtonText.text = $"{tt} {hh} : {mm}";;
    }
    
    

    public void SetButtonText(TextMeshProUGUI button, string newText)
    {
        button.text = newText;
    }
    
    void Update()
    {
        
    }
}
