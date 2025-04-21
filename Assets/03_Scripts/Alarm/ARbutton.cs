using UnityEngine;
using UnityEngine.SceneManagement;

public class ARbutton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("ARTestScene");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
