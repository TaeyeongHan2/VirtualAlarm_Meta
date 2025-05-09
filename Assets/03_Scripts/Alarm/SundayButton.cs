using UnityEngine;
using UnityEngine.UI;

public class SundayButton : MonoBehaviour
{
    public static SundayButton Instance { get; private set; }
    public bool isActive;
    public Image image;
    public Color currentColor;

    void Awake()
    {
        Instance = this;
        image = gameObject.GetComponent<Image>();
        Init();
    }

    public void Init()
    {
        isActive = true;
        image.color = DBAlarm.Instance.pastelPink;
        currentColor = image.color;
    }
    

    public void ChageButtonState()
    {
        if (currentColor == DBAlarm.Instance.pastelPink)
        {
            image.color = Color.gray;
            isActive = false;
            currentColor = image.color;
        }
        else if (currentColor == Color.gray)
        {
            image.color = DBAlarm.Instance.pastelPink;
            isActive = true;
            currentColor = image.color;
        }
    }
}
