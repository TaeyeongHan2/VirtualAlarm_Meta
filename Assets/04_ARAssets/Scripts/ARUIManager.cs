using UnityEngine;
using UnityEngine.SceneManagement;

public class ARUIManager : MonoBehaviour
{
    public GameObject scrollviewPanel;

    public void ScrollViewOpenButton()
    {
        scrollviewPanel.SetActive(true);
    }

    public void ScrollViewCloseButton()
    {
        scrollviewPanel.SetActive(false);
    }

    public void OnGoToAlarmScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OnGoToCalenderScene()
    {
        //SceneManager.LoadScene(2);
    }
}
