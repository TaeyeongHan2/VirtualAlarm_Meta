using System;
using _03_Scripts.Alarm;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlarmButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TMP_Text textUI;
    public Image buttonImage;
    private String _textBeforeDraged;
    public float dragCheckSensitivity = 100f;
    public String UITextOnDraging = "Swape to Delete"; 
    private void Awake()
    {
        textUI = GetComponentInChildren<TMP_Text>();
        buttonImage = GetComponentInChildren<Image>();
    }

    public void SetData(AlarmBase alarmBase)
    {
        textUI.text = $"{alarmBase.alarm12time}";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        _textBeforeDraged = textUI.text;
        textUI.text = UITextOnDraging;
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
            this.textUI.text = _textBeforeDraged;
    }
}
