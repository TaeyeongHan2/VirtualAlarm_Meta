using UnityEngine;
using UnityEngine.UI;

public class ThursdayButton : MonoBehaviour
{
    public static ThursdayButton Instance { get; private set; }
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
        image.color = Color.yellow;
        currentColor = image.color;
    }
    

    public void ChageButtonState()
    {
        if (currentColor == Color.yellow)
        {
            image.color = Color.gray;
            isActive = false;
            currentColor = image.color;
        }
        else if (currentColor == Color.gray)
        {
            image.color = Color.yellow;
            isActive = true;
            currentColor = image.color;
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