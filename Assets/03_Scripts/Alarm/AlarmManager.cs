using System;
using System.Collections;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    private Coroutine alarmCheckCoroutine; // 추후 코루틴을 종료해야할 때를 대비해서 IEnumerator 캐싱

    void Awake()
    {
        // todo : 코루틴의 최초 실행 시점을 알람이 생성될 때로 옮겨 성능 향상 시도하기
        alarmCheckCoroutine = StartCoroutine(RunAtEvery5Minutes());
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    private IEnumerator RunAtEvery5Minutes()
    {
        Application.runInBackground = true; // 백그라운드에서도 실행하게 앱 설정
        
        DateTime currentTime = DateTime.Now; 
        int minutesToWait = 5 - (currentTime.Minute % 5);
        int secondsToWait = 60 - (currentTime.Second);

        float initialWaitTime = (minutesToWait * 60) + secondsToWait - 60f;
    
        Debug.Log($"코루틴의 최초 대기시간 : {initialWaitTime}");
        
        yield return new WaitForSecondsRealtime(initialWaitTime);
        
        while (true)
        {
            // todo : 실행할 알람 있는지 확인 하는 매소드 여기서 실행

            Debug.Log(DateTime.Now); // 디버깅용
            
            yield return new WaitForSecondsRealtime(300f);
        }
    }
}
