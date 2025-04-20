using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _03_Scripts.Alarm;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlarmButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TMP_Text[] textUIs;
    public TMP_Text timeTextUI;
    
    public TMP_Text[] daysTextUI;
    public TMP_Text mondayTextUI;
    public TMP_Text tuesdayTextUI;
    public TMP_Text wednesdayTextUI;
    public TMP_Text thursdayTextUI;
    public TMP_Text fridayTextUI;
    public TMP_Text saturdayTextUI;
    public TMP_Text sundayTextUI;
    
    public Dictionary<string, TMP_Text> daysText = new Dictionary<string, TMP_Text>();
    
    public Image buttonImage;
    private String _textBeforeDraged;
    public float dragCheckSensitivity = 100f;
    public String UITextOnDraging = "Swape to Delete"; 
    private void Awake()
    {
        textUIs = GetComponentsInChildren<TMP_Text>();
        timeTextUI = textUIs[0];
        mondayTextUI = textUIs[1];
        tuesdayTextUI = textUIs[2];
        wednesdayTextUI = textUIs[3];
        thursdayTextUI = textUIs[4];
        fridayTextUI = textUIs[5];
        saturdayTextUI = textUIs[6];
        sundayTextUI = textUIs[7];
        
        daysTextUI = new TMP_Text[7];
        daysTextUI[0] = mondayTextUI;
        daysTextUI[1] = tuesdayTextUI;
        daysTextUI[2] = wednesdayTextUI;
        daysTextUI[3] = thursdayTextUI;
        daysTextUI[4] = fridayTextUI;
        daysTextUI[5] = saturdayTextUI;
        daysTextUI[6] = sundayTextUI;
        
        daysText["월"] = mondayTextUI;
        daysText["화"] = tuesdayTextUI;
        daysText["수"] = wednesdayTextUI;
        daysText["목"] = thursdayTextUI;
        daysText["금"] = fridayTextUI;
        daysText["토"] = saturdayTextUI;
        daysText["일"] = sundayTextUI;
        
        
        buttonImage = GetComponentInChildren<Image>();
    }
    
    

    public void SetData(AlarmBase alarmBase)
    {
        StringBuilder reapeatDaysSB = new StringBuilder();
        var trueKeys = alarmBase.alarmRepeatDays.Where(x => x.Value == true).Select(x => x.Key);
        foreach (var key in trueKeys)
        {
            var activeDay = daysText[key];
            activeDay.color = Color.black;
        }

        timeTextUI.text = $"{alarmBase.alarm12time}\n";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        _textBeforeDraged = timeTextUI.text;
        timeTextUI.text = UITextOnDraging;
        
        foreach (var v in daysTextUI)
            v.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        //var originalPosition = eventData.position;

        if (eventData.delta.sqrMagnitude > dragCheckSensitivity)
        {
            Destroy(gameObject);
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        if (this != null)
        {
            timeTextUI.text = _textBeforeDraged;
            foreach (var v in daysTextUI)
                v.gameObject.SetActive(false);
        }

    }
}
