using UnityEngine;
using UnityEngine.SceneManagement;

public class GoAlarm : MonoBehaviour
{
    public void GoToAlarm()
    {
        SceneManager.LoadScene("UIScene");
    }
}
