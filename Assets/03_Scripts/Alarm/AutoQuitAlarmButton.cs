using UnityEngine;
using UnityEngine.UI;

public class AutoQuitAlarmButton : MonoBehaviour
{
    public static AutoQuitAlarmButton Instance { get; private set; }
    public bool isActive;
    public Image image;
    public Color currentColor;
    void Awake()
    {
        Instance = this;
        image = gameObject.GetComponent<Image>();
    }

    void Start()
    {
        Init();
        
    }

    public void Init()
    {
        isActive = true;
        image.color = Color.white;
        currentColor = image.color;
        AutoQuitTimerButton.Instance.image.color = Color.white;
    }
    

    public void ChageButtonState()
    {
        if (currentColor == Color.white)
        {
            image.color = Color.gray;
            isActive = false;
            currentColor = image.color;
            AutoQuitTimerButton.Instance.image.color = Color.gray;
        }
        else if (currentColor == Color.gray)
        {
            image.color = Color.white;
            isActive = true;
            currentColor = image.color;
            AutoQuitTimerButton.Instance.image.color = Color.white;
        }
    }
}