using System;
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

    public void SetClockTimeSetButtonText()
    {
        UIReference.instance.ClockTimeSetButtonText.text = DateTime.Now.ToString("tt hh:mm");
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
