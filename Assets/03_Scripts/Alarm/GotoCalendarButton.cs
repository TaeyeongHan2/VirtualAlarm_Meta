using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GotoCalendarButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ChangeSceneToCalendar()
    {
        SceneManager.LoadScene("CalendarScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
