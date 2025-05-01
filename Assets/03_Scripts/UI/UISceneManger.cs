using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UISceneManger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void ChangeSceneToAR()
    {
        SceneManager.LoadScene("ARTestScene");
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
