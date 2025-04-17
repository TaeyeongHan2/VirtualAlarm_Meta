using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigator : MonoBehaviour
{
    public GameObject[] pagePrefabsArray;
    public GameObject currentPage;
    
    
    private void Awake()
    {
        pagePrefabsArray = new GameObject[5];
        pagePrefabsArray[0] = UIReference.instance.viewTitlePrefab;
        pagePrefabsArray[1] = UIReference.instance.viewHomePrefab;
        pagePrefabsArray[2] = UIReference.instance.viewAlarmSettingPrefab;
        pagePrefabsArray[3] = UIReference.instance.viewTimeSetPrefab;
        pagePrefabsArray[4] = UIReference.instance.viewTimeRingingPrefab;

        if (currentPage != null)
        {
            currentPage = pagePrefabsArray[0];
        }
        currentPage.SetActive(true);

    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        ChangePage(1);
        


    }

    private void ChangePage(int pageIndex)
    {
        var lastPage = currentPage;
        lastPage.SetActive(false);
        
        currentPage = pagePrefabsArray[pageIndex];
        currentPage.SetActive(true);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
