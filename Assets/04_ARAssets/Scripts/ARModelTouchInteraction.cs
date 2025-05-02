using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;

public class ARModelTouchInteraction : MonoBehaviour
{
    private static readonly int Wave = Animator.StringToHash("Wave");
    private static readonly int Nod = Animator.StringToHash("Nod");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Dance = Animator.StringToHash("Dance");
    private static readonly int CanInteract = Animator.StringToHash("CanInteract");

    [SerializeField] 
    private Camera arCamera;
    [SerializeField] 
    private Animator modelAnimator;
    [SerializeField]
    private ARTrackedImageManager arTrackedImageManager;
    [SerializeField] 
    private string markerName;
    
    private ARTrackedImage _arTrackedImage;
    
    // InputActions 에셋에서 생성된 클래스
    private TouchControl touchActions;
    
    // 더블 탭 감지 변수
    private float lastTapTime = -1f;
    private const float doubleTapMaxDelay = 0.3f;
    private int tapCount = 0;
    
    private void Awake()
    {
        // TouchControl이 null이 아닌지 확인
        try 
        {
            touchActions = new TouchControl();
            Debug.Log("TouchControl initialized successfully");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to initialize TouchControl: {e.Message}");
        }
    }
    
    private void OnEnable()
    {
        if (touchActions != null)
        {
            touchActions.Enable();
            touchActions.Touch.Tap.performed += OnTap;
            Debug.Log("Touch actions enabled");
        }
        else
        {
            Debug.LogError("TouchActions is null in OnEnable");
        }
        
        // AR 추적 이미지 관리자 확인
        if (arTrackedImageManager == null)
        {
            Debug.LogError("ARTrackedImageManager is null");
            arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        }
    }

    private void OnDisable()
    {
        if (touchActions != null)
        {
            touchActions.Touch.Tap.performed -= OnTap;
            touchActions.Disable();
        }
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        // Debug 출력 추가
        Debug.Log("Tap detected");
        
        // 기기 확인
        #if UNITY_IOS
        Debug.Log("Running on iOS");
        #elif UNITY_ANDROID
        Debug.Log("Running on Android");
        #endif
        
        // arTrackedImageManager가 null인지 확인
        if (arTrackedImageManager == null)
        {
            Debug.LogError("ARTrackedImageManager is null");
            return;
        }
        
        // 1. 마커가 tracking 중일 때만 동작
        _arTrackedImage = null;
        
        try
        {
            foreach (var img in arTrackedImageManager.trackables)
            {
                if (img != null && img.referenceImage != null && 
                    img.referenceImage.name == markerName &&
                    img.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                {
                    _arTrackedImage = img;
                    Debug.Log($"Found tracked image: {markerName}");
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error checking trackables: {e.Message}");
            return;
        }
        
        if (_arTrackedImage == null)
        {
            Debug.Log("No tracked image found matching criteria");
            return;
        }
        
        // 터치 위치 얻기 - 플랫폼별 처리
        Vector2 touchPosition;
        
        #if UNITY_IOS
        // iOS에서는 직접 터치 위치를 얻는 대체 방법 사용
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            Debug.Log($"iOS touch position: {touchPosition}");
        }
        else
        {
            Debug.Log("No touches detected on iOS");
            return;
        }
        #else
        // 다른 플랫폼에서는 원래 방식 사용
        touchPosition = touchActions.Touch.TouchPosition.ReadValue<Vector2>();
        Debug.Log($"Touch position: {touchPosition}");
        #endif
        
        // 더블탭 감지
        float now = Time.time;
        if (tapCount == 1 && now - lastTapTime < doubleTapMaxDelay)
        {
            Debug.Log("Double tap detected");
            if (modelAnimator != null && modelAnimator.GetBool(CanInteract))
            {
                Debug.Log("Double Tap : Dance Animation Start");
                modelAnimator.SetTrigger(Dance);
            }
            else
            {
                Debug.Log($"Animator issue: null={modelAnimator==null}, CanInteract={modelAnimator?.GetBool(CanInteract)}");
            }
            tapCount = 0;
            return;
        }
        else
        {
            tapCount = 1;
            lastTapTime = now;
        }
        
        // 카메라 확인
        if (arCamera == null)
        {
            Debug.LogError("AR Camera is null");
            arCamera = Camera.main;
            if (arCamera == null)
            {
                Debug.LogError("Failed to find main camera");
                return;
            }
        }
        
        // 단일 탭일 때만 파츠 판정
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        Debug.Log($"Casting ray from: {ray.origin}, direction: {ray.direction}");
        
        RaycastHit[] hits = Physics.RaycastAll(ray);
        Debug.Log($"Raycast returned {hits.Length} hits");

        RaycastHit? foundHit = null;
        string hitTag = "";

        // 1. Hand, 2. Head, 3. Model
        foreach (var hit in hits)
        {
            Debug.Log($"Hit object: {hit.collider.gameObject.name}, tag: {hit.collider.tag}");
            if (hit.collider.CompareTag("Hand"))
            {
                foundHit = hit;
                hitTag = "Hand";
                break;
            }
        }

        if (!foundHit.HasValue)
        {
            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Head"))
                {
                    foundHit = hit;
                    hitTag = "Head";
                    break;
                }
            }
        }

        if (!foundHit.HasValue)
        {
            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Foot"))
                {
                    foundHit = hit;
                    hitTag = "Foot";
                    break;
                }
            }
        }
        
        // 분기 처리
        if (foundHit.HasValue)
        {
            Debug.Log($"[Tap] 우선 수위 hit part: {hitTag}, Object : {foundHit.Value.collider.gameObject.name}");
            
            // Animator 유효성 검사
            if (modelAnimator == null)
            {
                Debug.LogError("Animator is null");
                return;
            }
            
            if (hitTag == "Head")
            {
                modelAnimator.SetTrigger(Nod);
                Debug.Log("Head touch animation start");
            }
            else if (hitTag == "Hand")
            {
                modelAnimator.SetTrigger(Wave);
                Debug.Log("Hand touch animation start");
            }
            else if (hitTag == "Foot")
            {
                modelAnimator.SetTrigger(Jump);
                Debug.Log("Model touch animation start");
            }
        }
        else
        {
            Debug.Log("No hit objects found with relevant tags");
        }
    }
}