using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTrackingManager : MonoBehaviour
{
    private float delayBeforeHideModel = 5f; // 5초동안 마커 안보이면 매소드 실행을 위한 변수
    private Coroutine hideModelCoroutine; 
    private Dictionary<string, Coroutine> hideModelCoroutines = new Dictionary<string, Coroutine>();

    private static readonly int CanInteract = Animator.StringToHash("CanInteract");
    private static readonly int AlarmState = Animator.StringToHash("AlarmState");
    
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject characterWithStage;
    [SerializeField] private string markerName;
    [SerializeField] private AlarmManager alarmManager;

    [field: Header("Model 정렬")] 
    public float ModelYRotationOffset { get; } = 180f; // 모델 180도 돌리게 설정
    private float modelYoffset = 0f; // 테스트를 위해 10 뛰움
    
    [SerializeField] private Animator modelAnimator;
    [SerializeField] private ARTrackedImage _currImage;
    private string wakeUpStatus;
    

    private void OnEnable()
    {
        trackedImageManager.trackablesChanged.AddListener(OnChanged);
    }

    private void OnDisable()
    {
        trackedImageManager.trackablesChanged.RemoveListener(OnChanged);
    }

    private void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        // 마커가 새로 인식(추가)될 때
        foreach (var trackedImage in eventArgs.added)
        {
            if (trackedImage.referenceImage.name == markerName)
            {
                if (hideModelCoroutine != null)
                {
                    StopCoroutine(hideModelCoroutine);
                    hideModelCoroutine = null;
                }
                
                characterWithStage.transform.SetParent(trackedImage.transform);
                characterWithStage.SetActive(true);
                _currImage = trackedImage;
                
                PlaceSetAnimation(trackedImage);
            }
        }
        
        foreach (var pair in eventArgs.removed)
        {
            var removedARMarker = pair.Value;
            if (removedARMarker.referenceImage.name == markerName)
            { 
                hideModelCoroutine = StartCoroutine(HideCharacterModel());
            }
        }
    }

    private void PlaceSetAnimation(ARTrackedImage trackedImage)
    {
        characterWithStage.transform.position = trackedImage.transform.position +
                                         trackedImage.transform.up * modelYoffset;
        characterWithStage.transform.rotation = trackedImage.transform.rotation 
                                         * Quaternion.Euler(0f, ModelYRotationOffset, 0f);
        
        // 알람 상태 판정
        // 마커를 처음 인식한 순간 -> 현재 시간과 알람 시간 비교
        DateTime now = DateTime.Now;
        var currentHours = now.Hour;
        var currentMinutes = now.Minute;
        var currentTotalMin = currentHours * 60 + currentMinutes;
                    
        // UI manager에서 현재 설정된 알람 리스트를 불러옴
        var alarmList = DBAlarm.Instance.alarmDataList;
        if (alarmList == null || alarmList.Count == 0)
        {
            Debug.Log("[AR] 알람 데이터 없음");
            return;
        }
        
        var settedAlarmTime = DBAlarm.Instance.enabledAlarmTimeAsMinutes;
        var standard = settedAlarmTime + 3;
                    
        // 현재 시간과 알람 시간 차이 비교하여 사용자의 일어남 상태 확인하여 상태에 저장
        if (currentTotalMin > standard )
        {
            wakeUpStatus = "late";
        }
        else if(settedAlarmTime > currentTotalMin)
        {
            wakeUpStatus = "early";
        }
        else if (currentTotalMin <= standard && settedAlarmTime < currentTotalMin)
        {
            wakeUpStatus = "onTime";
        }
        else
        {
            Debug.Log("error");
        }

        if (DBAlarm.Instance.wakeUpStatus != null)
        {
            wakeUpStatus = DBAlarm.Instance.wakeUpStatus;
        }
        // Animator Controller change
        if (modelAnimator != null)
        {
            int alarmState = 0; // 기본값: early
            switch (wakeUpStatus)
            {
                case "early":
                    alarmState = 0;
                    break;
                case "onTime":
                    alarmState = 1;
                    break;
                case "late":
                    alarmState = 2;
                    break;
            }
            modelAnimator.SetInteger(AlarmState, alarmState);
            modelAnimator.SetBool(CanInteract, false);
            
            
            // 알람 애니메이션 길이만큼 대기 후 상호작용 해제
            float alarmAnimLength = 5f;
            StartCoroutine(EnableInteractionAfterDelay(alarmAnimLength));
           
            int year = now.Year;
            int month = now.Month;
            int day = now.Day;

            string wakeupTime = now.ToString("HH:mm");
            string settedTime = $"{AlarmManager.currentHour:D2}:{AlarmManager.currentMinute:D2}";

            TimeData.SaveDayRecord(year, month, day, settedTime, wakeupTime);

            Debug.Log($"[AR] PlayerPrefs 저장 완료: {year}.{month}.{day} Set:{settedTime}, Wake:{wakeupTime}");
        }
    }

    private IEnumerator EnableInteractionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Sub State Machine Changed?!?!?!?!?");
        modelAnimator.SetBool(CanInteract, true);
    }
    // Reset 버튼이 현재 마커의 rotaion을 사용할 수 있게 public 메서드로 제공
    public ARTrackedImage GetCurrentlyTrackingImage()
    {
        return _currImage;
    }
    
    private IEnumerator HideCharacterModel()
    {
        yield return new WaitForSeconds(delayBeforeHideModel);
        
        characterWithStage.transform.SetParent(null);
        characterWithStage.SetActive(false);
        _currImage = null;
    }
}
