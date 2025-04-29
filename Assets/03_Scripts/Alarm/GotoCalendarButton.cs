using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ARbutton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("CalendarScene");
        
    }
    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("context.phase == InputActionPhase.Started");
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("context.phase == InputActionPhase.Performed");
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            Debug.Log("context.phase == InputActionPhase.Canceled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
