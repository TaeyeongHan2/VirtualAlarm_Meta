using TMPro;
using UnityEngine;
[DefaultExecutionOrder(-900)]
public class UIReference : MonoBehaviour
{
    public static UIReference instance { get; private set; }
    
    [Header("UI Pages")]
    public GameObject viewTitlePrefab;
    public GameObject viewHomePrefab;
    public GameObject viewAlarmSettingPrefab;
    public GameObject viewTimeSetPrefab;
    public GameObject viewTimeRingingPrefab;

    [Header("HOME - VIEW")] 
    public GameObject HomeAlarmButtonPrefab;
    public GameObject HomeAlarmListRoot;

    [Header("Alarm Setting - VIEW")] 
    public GameObject ClockTimeSetButton;
    public TextMeshProUGUI ClockTimeSetButtonText;

    public GameObject MondayButton;
    public TextMeshProUGUI MondayButtonText;
    public GameObject TuesdayButton;
    public TextMeshProUGUI TuesdayButtonText;
    public GameObject WednesdayButton;
    public TextMeshProUGUI WednesdayButtonText;
    public GameObject ThursdayButton;
    public TextMeshProUGUI ThursdayButtonText;
    public GameObject FridayButton;
    public TextMeshProUGUI FridayButtonText;
    public GameObject SaturdayButton;
    public TextMeshProUGUI SaturdayButtonText;
    public GameObject SundayButton;
    public TextMeshProUGUI SundayButtonText;

    public GameObject BellTurnOnOffButton;
    public TextMeshProUGUI BellTurnOffButtonText;
    public GameObject AutoQuitAlarmButton;
    public TextMeshProUGUI AutoQuitAlarmButtonText;
    public GameObject AutoQuitTimerButton;
    public TextMeshProUGUI AutoQuitTimerButtonText;
    public GameObject OKButton;
    public GameObject CancelButton;
    [Header("Ringing - VIEW")] 
    public GameObject currentTimeButton;
    public TextMeshProUGUI currentTimeButtonText;
    public GameObject ARButton;
    
    [Header("Sounds")]
    public AudioSource alarmMusic;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
