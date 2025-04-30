using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ARUIManager : MonoBehaviour
{
    public GameObject scrollviewPanel;
    
    //public PlayerInput playerInput;

    //public ScrollRect scrollRect;

    public void Start()
    {
        //scrollRect.verticalNormalizedPosition = 0f;
    }

    public void ScrollViewOpenButton()
    {
        scrollviewPanel.SetActive(true);
        
        //playerInput.SwitchCurrentActionMap("UI");
    }

    public void ScrollViewCloseButton()
    {
        scrollviewPanel.SetActive(false);
        
        //playerInput.SwitchCurrentActionMap("TouchControl");
    }

    public void OnGoToAlarmScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OnGoToCalenderScene()
    {
        SceneManager.LoadScene(2);
    }
}
