using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoQuitTimerButton : MonoBehaviour
{
    public static AutoQuitTimerButton Instance { get; private set; }
    public int minutes;
    public Image image;
    public TMP_Text text;

    void Awake()
    {
        Instance = this;
        image = gameObject.GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
        Init();
    }

    public void Init()
    {
        minutes = 1;
        text.text = $"{minutes.ToString()} MIN";
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
        text.text = $"{minutes.ToString()} MIN";
        
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