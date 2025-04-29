using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARImageTrackingManager : MonoBehaviour
{
    private static readonly int CanInteract = Animator.StringToHash("CanInteract");
    private static readonly int AlarmState = Animator.StringToHash("AlarmState");
    
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject modelPrefab;
    [SerializeField] private string markerName;
    [SerializeField] private AlarmManager alarmManager;

    [field: Header("Model 정렬")] 
    public float ModelYRotationOffset { get; } = 180f; // 모델 180도 돌리게 설정
    private float modelYoffset = 100f; // 테스트를 위해 100 뛰움
    
    [SerializeField] private Animator modelAnimator;
    [SerializeField] private ARTrackedImage _currImage;
    private string wakeUpStatus = "early";    // 초기값은 early로 설정

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
                modelPrefab.transform.SetParent(trackedImage.transform);
                modelPrefab.SetActive(true);
                _currImage = trackedImage;
                
                PlaceSetAnimation(trackedImage);
            }
        }
        
        // 마커가 업데이트(트래킹 상태 변화)될 때
        foreach (var pair in eventArgs.removed)
        {
            if (pair.Value.referenceImage.name == markerName)
            {
                modelPrefab.transform.SetParent(null);
                modelPrefab.SetActive(false);
                _currImage = null;
                
                PlaceSetAnimation(pair.Value);
            }
        }
    }

    private void PlaceSetAnimation(ARTrackedImage trackedImage)
    {
        modelPrefab.transform.position = trackedImage.transform.position +
                                         trackedImage.transform.up * modelYoffset;
        modelPrefab.transform.rotation = trackedImage.transform.rotation 
                                         * Quaternion.Euler(0f, ModelYRotationOffset, 0f);
        
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
            int alarmState = 0; // 기본값: OnTime
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
