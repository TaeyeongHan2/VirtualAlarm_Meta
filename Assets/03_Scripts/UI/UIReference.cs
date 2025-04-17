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

    private void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
