using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTrackingManager : MonoBehaviour
{
    private float delayBeforeHideModel = 0.5f; // 5초동안 마커 안보이면 매소드 실행을 위한 변수
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
    
    public static int currentHour;
    public static int currentMinute;
    
    //[Header("Voice")]
    //public AudioSource audioSource;
    //public AudioClip guideAudioClip;
    //private bool guideAudioPlayingStart = false;

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
        Debug.Log("[AR] trackableschanged event 발생");
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
                characterWithStage.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
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
                //                PlaceSetAnimation(trackedImage);

            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            Debug.Log($"[AR] {markerName} trackingState: {trackedImage.trackingState}");
            if (trackedImage.referenceImage.name == markerName)
            {
                if (trackedImage.trackingState != TrackingState.Tracking)
                {
                    if (hideModelCoroutine == null)
                    {
                        hideModelCoroutine = StartCoroutine(HideCharacterModel());
                    }
                }
                else
                {
                    if(hideModelCoroutine != null)
                    {
                        StopCoroutine(hideModelCoroutine);
                        hideModelCoroutine = null;
                    }

                    if (!characterWithStage.activeSelf)
                    {
                        characterWithStage.SetActive(true);
                        characterWithStage.transform.SetParent(trackedImage.transform);
                        characterWithStage.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                        _currImage = trackedImage;
                        Debug.Log("[AR] Tracking 복구 -> Model 다시 표시");
                        PlaceSetAnimation(trackedImage);
                    }
                    
                }
            }
        }
    }

    private void PlaceSetAnimation(ARTrackedImage trackedImage)
    {
        characterWithStage.transform.position = trackedImage.transform.position +
                                         trackedImage.transform.up * modelYoffset;
        //characterWithStage.transform.rotation = trackedImage.transform.rotation * Quaternion.Euler(0f, ModelYRotationOffset, 0f);
        //characterWithStage.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        
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
        else if(currentTotalMin >= settedAlarmTime && currentTotalMin <= standard)
        {
            wakeUpStatus = "onTime";
        }
        else if (currentTotalMin < settedAlarmTime)
        {
            wakeUpStatus = "early";
        }
        else
        {
            // 기본값
            wakeUpStatus = "early";
            //Debug.Log("error");
        }
        /*
        if (DBAlarm.Instance.wakeUpStatus != null)
        {
            wakeUpStatus = DBAlarm.Instance.wakeUpStatus;
        }
       */

        if (string.IsNullOrEmpty(DBAlarm.Instance.wakeUpStatus))
        {
            DBAlarm.Instance.wakeUpStatus = wakeUpStatus;
        }
        // Animator Controller change
        if (modelAnimator != null)
        {
            // 기본값: early
            int alarmState = 0; 
            switch (wakeUpStatus)
            {
                case "early":
                    alarmState = 0;
                    Debug.Log("[AR] 애니메이션 상태 : early");
                    break;
                case "onTime":
                    alarmState = 1;
                    Debug.Log("[AR] 애니메이션 상태 : onTime");
                    break;
                case "late":
                    alarmState = 2;
                    Debug.Log("[AR] 애니메이션 상태 : late");
                    break;
            }
            modelAnimator.SetInteger(AlarmState, alarmState);
            modelAnimator.SetBool(CanInteract, false);


            //guideAudioPlayingStart = true;
            // 알람 애니메이션 길이만큼 대기 후 상호작용 해제
            StartCoroutine(SetInteractionAfterAnimation());
        }
        // PlayerPrefs 저장
        string settedTimeString = $"{settedAlarmTime / 60:00}:{settedAlarmTime % 60:00}";
        string wakeupTimeString = $"{currentHours:00}:{currentMinutes:00}";

        int year = now.Year;
        int month = now.Month;
        int day = now.Day;

        TimeData.SaveDayRecord(year, month, day, settedTimeString, wakeupTimeString);
        
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
        //StopAllCoroutines();
        yield return new WaitForSeconds(delayBeforeHideModel);
        
        characterWithStage.transform.SetParent(null);
        characterWithStage.SetActive(false);
        _currImage = null;
        hideModelCoroutine = null;

        //guideAudioPlayingStart = false;
    }

    private IEnumerator SetInteractionAfterAnimation()
    {
        yield return null;
        
        AnimatorStateInfo currentState = modelAnimator.GetCurrentAnimatorStateInfo(0);
        float waitTime = currentState.length;

        while (modelAnimator.IsInTransition(0))
        {
            yield return null;
        }
        
        float remainingTime= (1.0f - currentState.normalizedTime) * waitTime;
        yield return new WaitForSeconds(remainingTime);
        /*
        while (currentState.normalizedTime < 1.0f || modelAnimator.IsInTransition(0))
        {
            yield return null;
            currentState = modelAnimator.GetCurrentAnimatorStateInfo(0);
        }
        
        //float length = currentState.length;
        //Debug.Log($"[AR] 현재 Animation 길이 : {length}");
        //yield return new WaitForSeconds(length);

        if (audioSource != null && guideAudioClip != null)
        {
            audioSource.PlayOneShot(guideAudioClip);
            Debug.Log("Guide Audio Clip Play");
        }
        */
        modelAnimator.SetBool(CanInteract, true);
        Debug.Log("[AR] CanInteract = true");
    }
}
