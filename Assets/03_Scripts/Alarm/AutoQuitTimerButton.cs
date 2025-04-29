using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoQuitTimerButton : MonoBehaviour
{
    public static AutoQuitTimerButton Instance { get; private set; }
    public int minutes;
    public Image image;
    public TMP_Text text;

    private void Awake()
    {
        Instance = this;
        image = gameObject.GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
        Init();
    }

    private void Init()
    {
        minutes = 1;
        text.text = $"{minutes.ToString()}분";
    }
    

    public void ChageButtonState()
    {
        if (minutes < 9)
        {
            minutes++;
        }
        else
        {
            minutes = 1;
        }
        text.text = $"{minutes.ToString()}분";
    }
}