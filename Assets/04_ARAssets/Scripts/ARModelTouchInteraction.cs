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
        touchActions = new TouchControl();
        
    }
    private void OnEnable()
    {
        touchActions.Enable();
        touchActions.Touch.Tap.performed += OnTap;
    }

    private void OnDisable()
    {
        touchActions.Touch.Tap.performed -= OnTap;
        touchActions.Disable();
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        // 1. 마커가 tracking 중일 때만 동작
        _arTrackedImage = null;
        foreach (var img in arTrackedImageManager.trackables)
        {
            if (img.referenceImage.name == markerName &&
                img.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                _arTrackedImage = img;
                break;
            }
        }
        if (_arTrackedImage == null)
        {
            return;
        }
        
        // 터치 위치 얻기
        Vector2 touchPosition = touchActions.Touch.TouchPosition.ReadValue<Vector2>();
        
        // 더블탭 감지
        float now = Time.time;
        if (tapCount == 1 && now - lastTapTime < doubleTapMaxDelay)
        {
            if (modelAnimator != null && modelAnimator.GetBool(CanInteract))
            {
                Debug.Log("Double Tap : Dance Animation Start");
                modelAnimator.SetTrigger(Dance);
            }
            else
            {
                Debug.Log("Animator is NULL");
            }
            tapCount = 0;
            return;
        }
        else
        {
            tapCount = 1;
            lastTapTime = now;
        }
        
        // 단일 탭일 때만 파츠 판정
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        RaycastHit? foundHit = null;
        string hitTag = "";

        // 1. Hand, 2. Head, 3. Model
        foreach (var hit in hits)
        {
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
            Debug.Log("No Animation");
        }
    }
}
