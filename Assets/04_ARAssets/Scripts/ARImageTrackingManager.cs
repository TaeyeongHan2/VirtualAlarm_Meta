using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARImageTrackingManager : MonoBehaviour
{
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
    //[SerializeField] private float baseMarkerSize = 0.14f;
    //[SerializeField] private float minScale = 0.1f;
    //[SerializeField] private float maxScale = 0.3f;

    //public float moveRadius = 0.04f;
    //public float moveSpeed = 0.25f;
    //private float timer = 0f;

    private ARTrackedImage _currImage = null;
    private bool wasTracking = false;
    
    // 캐릭터 프리팹
    //public GameObject earlyWakeShinanoPrefab;
    //public GameObject afterAlarmShinanoPrefab;
    //public GameObject wakeUpLateShinanoPrefab;
    
    // Animator Controller
    [SerializeField]
    private Animator modelAnimator;
    [SerializeField]
    private RuntimeAnimatorController earlyAnimatorController;
    [SerializeField]
    private RuntimeAnimatorController onTimeAnimatorController;
    [SerializeField]
    private RuntimeAnimatorController lateAnimatorController;
    
    // 현재 생성된 캐릭터 프리팹
    //private GameObject spawnedPrefab;
    
    // 초기값은 정시에 일어난 걸로 설정
    public string wakeUpStatus = "onTime";
    void Update()
    {
        bool found = false;
        
        foreach (var trackedImage in trackedImageManager.trackables)
        {
            //Debug.Log($"[AR] name : {trackedImage.referenceImage.name}, state : {trackedImage.trackingState}");
            if (trackedImage.referenceImage.name == markerName &&
                trackedImage.trackingState == TrackingState.Tracking)
            {
                found = true;

                if (!wasTracking)
                {
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
                    else if (currentTotalMin <= standard)
                    {
                        wakeUpStatus = "onTime";
                    }
                    else
                    {
                        wakeUpStatus = "early";
                    }
                    
                    // DateTime alarmTime = new DateTime(now.Year, now.Month, now.Day,
                    //     latestAlarm.alarm24Hour, latestAlarm.alarm24Minute, 0);
                    
                    // 현재 시간과 알람 시간 차이 비교하여 사용자의 일어남 상태 확인하여 상태에 저장
                    // TimeSpan diff = now - alarmTime;
                    // if (now < alarmTime)
                    // {
                    //     wakeUpStatus = "early";
                    // }
                    // else if (diff.TotalMinutes <= 3)
                    // {
                    //     wakeUpStatus = "onTime";
                    // }
                    // else
                    // {
                    //     wakeUpStatus = "late";
                    // }
                    
                    // Animator Controller change
                    if (modelAnimator != null)
                    {
                        switch (wakeUpStatus)
                        {
                            case "early":
                                modelAnimator.runtimeAnimatorController = earlyAnimatorController;
                                break;
                            case "onTime":
                                modelAnimator.runtimeAnimatorController = onTimeAnimatorController;
                                break;
                            case "late":
                                modelAnimator.runtimeAnimatorController = lateAnimatorController;
                                break;
                        }
                    }
                    
                    modelPrefab.SetActive(true);
                    
                    modelPrefab.transform.position = trackedImage.transform.position;
                    
                    modelPrefab.transform.rotation = trackedImage.transform.rotation 
                                                     * Quaternion.Euler(0f, modelYRotationOffset, 0f);
                   /* 
                    // model scale 
                    float scale = trackedImage.size.y / baseMarkerSize;
                    scale = Mathf.Clamp(scale, minScale, maxScale);
                    modelPrefab.transform.localScale = Vector3.one * scale;
                    */
                }
                _currImage = trackedImage;
                break;
                /*
                // 원형 경로를 따로 자동 이동
                timer += Time.deltaTime * moveSpeed;
                float angle = timer * Mathf.PI * 2f;
                Vector3 markerCenter = trackedImage.transform.position;
                Vector3 markerRight = trackedImage.transform.right;
                Vector3 markerForward = trackedImage.transform.forward;

                Vector3 offset = markerRight * Mathf.Cos(angle) * moveRadius +
                                 markerForward * Mathf.Sin(angle) * moveRadius;
                modelPrefab.transform.position = markerCenter + offset;
                //modelPrefab.transform.position = trackedImage.transform.position;
                modelPrefab.transform.rotation = trackedImage.transform.rotation;

               */
            }
        }
        // 만약 found가 false면 마커를 못 본 상태 -> setActive(false)
        if (!found)
        {
            if (modelPrefab.activeSelf)
            {
                //Debug.Log("[AR] Model Off");
                modelPrefab.SetActive(false);
                _currImage = null;
            }
        }
        wasTracking = found;
    }
    // Reset 버튼이 현재 마커의 rotaion을 사용할 수 있게 public 메서드로 제공
    public ARTrackedImage GetCurrentlyTrackingImage()
    {
        return _currImage;
    }
}
