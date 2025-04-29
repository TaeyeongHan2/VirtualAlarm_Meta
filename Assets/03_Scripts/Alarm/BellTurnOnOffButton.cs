using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BellTurnOnOffButton : MonoBehaviour
{
    public static BellTurnOnOffButton Instance { get; private set; }
    public bool isActive;
    public Image image;
    public Color currentColor;
    public TMP_Text text;
    public string bellOnText = "벨소리";
    public string bellOffText = "진동";
    void Awake()
    {
        Instance = this;
        image = gameObject.GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
        Init();
    }

    public void Init()
    {
        isActive = true;
        image.color = Color.white;
        currentColor = image.color;
        text.text = bellOnText;
    }
    

    public void ChageButtonState()
    {
        if (currentColor == Color.white)
        {
            image.color = Color.gray;
            isActive = false;
            currentColor = image.color;
            text.text = bellOffText;
        }
        else if (currentColor == Color.gray)
        {
            image.color = Color.white;
            isActive = true;
            currentColor = image.color;
            text.text = bellOnText;
        }
    }
}