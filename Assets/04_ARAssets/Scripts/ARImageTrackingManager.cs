using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARImageTrackingManager : MonoBehaviour
{
    private static readonly int CanInteract = Animator.StringToHash("CanInteract");
    private static readonly int AlarmState = Animator.StringToHash("AlarmState");

    [SerializeField]
    private ARTrackedImageManager trackedImageManager;
    [SerializeField] 
    private GameObject modelPrefab;
    [SerializeField] 
    private string markerName;
    [SerializeField]
    private AlarmManager alarmManager;
    
    [Header("Model 정렬")]
    public float modelYRotationOffset = 180f;
    [SerializeField] 
    private float modelYoffset = 0f;
    
    [SerializeField]
    private Animator modelAnimator;
    
    private ARTrackedImage _currImage = null;
    
    
    // 초기값은 정시에 일어난 걸로 설정
    public string wakeUpStatus = "onTime";

    private void OnEnable()
    {
        trackedImageManager.trackablesChanged.AddListener(OnChanged);
    }

    private void OnDisable()
    {
        trackedImageManager.trackablesChanged.RemoveListener(OnChanged);
    }

    private void OnChanged(UnityEngine.XR.ARFoundation.ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        // 마커가 새로 인식(추가)될 때
        foreach (var added in eventArgs.added)
        {
            if (added.referenceImage.name == markerName)
            {
                PlaceSetAnimation(added);
            }
        }
        // 마커가 업데이트(트래킹 상태 변화)될 때
        foreach (var updated in eventArgs.updated)
        {
            if (updated.referenceImage.name == markerName &&
                updated.trackingState == TrackingState.Tracking &&
                !modelPrefab.activeSelf)
            {
                PlaceSetAnimation(updated);
            }
        }
        // removed는 XRTrackedImage 타입이므로 referenceImage, trackabledID 사용 불가!
        // 대신 현재 trackables에서 마커가 사라졌는지 직접 확인:
        // 마커가 사라질 때
        bool markerStillTracked = false;
        foreach (var trackedImage in trackedImageManager.trackables)
        {
            // referenceImage가 null일 수 있으므로, TrackabledID로 비교
            if (trackedImage.referenceImage.name == markerName &&
                trackedImage.trackingState == TrackingState.Tracking)
            {
                markerStillTracked = true;
                break;
            }
        }

        if (!markerStillTracked)
        {
            modelPrefab.SetActive(false);
            _currImage = null;
        }
    }

    private void PlaceSetAnimation(ARTrackedImage trackedImage)
    {
        modelPrefab.SetActive(true);
        modelPrefab.transform.position = trackedImage.transform.position +
                                         trackedImage.transform.up * modelYoffset;
        modelPrefab.transform.rotation = trackedImage.transform.rotation 
                                         * Quaternion.Euler(0f, modelYRotationOffset, 0f);
        
        // 알람 상태 판정
        // 마커를 처음 인식한 순간 -> 현재 시간과 알람 시간 비교
        DateTime now = DateTime.Now;
        var currentHours = now.Hour;
        var currentMinutes = now.Minute;
        var currentTotalMin = currentHours * 60 + currentMinutes;
                    
        // UI manager에서 현재 설정된 알람 리스트를 불러옴
        var alarmList = UIManager.Instance.alarmDataList;
        if (alarmList == null || alarmList.Count == 0)
        {
            Debug.Log("[AR] 알람 데이터 없음");
            return;
        }
                    
        // 마지막으로 설정된 알람을 기준으로 DateTime 객체 생성
        var settedAlarmTime = AlarmManager.currentTotalMin;
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
        
        // Animator Controller change
        if (modelAnimator != null)
        {
            int alarmState = 1; // 기본값: OnTime
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
        }

        _currImage = trackedImage;
    }

    private System.Collections.IEnumerator EnableInteractionAfterDelay(float delay)
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
}
