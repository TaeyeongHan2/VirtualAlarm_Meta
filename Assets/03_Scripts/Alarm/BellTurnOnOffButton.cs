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
    public string bellOnText = "BELL ON";
    public string bellOffText = "BELL OFF";
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
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}