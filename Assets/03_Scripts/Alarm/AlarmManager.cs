using System;
using System.Collections;
using _03_Scripts.Alarm;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    private Coroutine alarmCheckCoroutine; // 추후 코루틴을 종료해야할 때를 대비해서 IEnumerator 캐싱

    public static AlarmBase currentAlarmData;
    public static int currentTotalMin;
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

    public void CheckAndRunAlarm(DateTime now)
    {
        var now12 = now.ToString("tt hh:mm");
        var alarmDataList = UIManager.Instance.alarmDataList;
        var alarmsButtonsList = UIManager.Instance.homePageAlarmsButtons;
        for ( int i = 0; i < alarmDataList.Count; i++)
        {
            if (alarmDataList[i].alarm12time == now12)
            {
                Debug.Log("벨이 울립니다");
                currentAlarmData = alarmDataList[i];
                var currentHours = DateTime.Now.Hour;
                var currentMinutes = DateTime.Now.Minute;
                currentTotalMin = currentHours * 60 + currentMinutes;
                var isAutoQuit = currentAlarmData.isAUTOQuitOn;
                var AUTOQuitMIN = currentAlarmData.AUTOQuitMinutes;
                var isBellOn = currentAlarmData.isBellOn;
                SFXManager.Instance.PlaySound(UIReference.instance.alarmMusic, isAutoQuit, AUTOQuitMIN*60f);
                UINavigator.instance.ChangePage(4);
                UIReference.instance.currentTimeButtonText.text = now12;
                // TODO: currentTimeButtonText가 실시간으로 시간 변하게 설정
                
                // 알람 삭제 코드
                //UIManager.Instance.alarmDataList.RemoveAt(i);
                //UIManager.Instance.homePageAlarmsButtons.RemoveAt(i);
                //Destroy(_);


            }
            
        }
    }
    
    private IEnumerator RunAtEvery5Minutes()
    {
        Application.runInBackground = true; // 백그라운드에서도 실행하게 앱 설정
        
        DateTime currentTime = DateTime.Now; 
        //int minutesToWait = 5 - (currentTime.Minute % 5);
        int secondsToWait = 60 - (currentTime.Second);

        //float initialWaitTime = (minutesToWait * 60) + secondsToWait - 60f;
        float initialWaitTime = secondsToWait;

    
        Debug.Log($"코루틴의 최초 대기시간 : {initialWaitTime}");
        
        yield return new WaitForSecondsRealtime(initialWaitTime);
        
        while (true)
        {
            // todo : 실행할 알람 있는지 확인 하는 매소드 여기서 실행
            DateTime now = DateTime.Now;
            CheckAndRunAlarm(now);
            
            yield return new WaitForSecondsRealtime(60f);
        }
    }
}
