using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;

public class ARModelTouchInteraction : MonoBehaviour
{
    [SerializeField] 
    private Camera arCamera;
    [SerializeField] 
    private Animator modelAnimator;
    [SerializeField]
    private ARTrackedImageManager arTrackedImageManager;
    [SerializeField] 
    private string markerName;
    
    private ARTrackedImage _arTrackedImage;
    void Update()
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
        
        // 멀티 터치 땐 return -> 애니메이션 처리 x
        int activeTouchCount = 0;
        foreach (var t in Touchscreen.current.touches)
        {
            if (t.press.isPressed)
            {
                activeTouchCount++;
            }
        }

        if (activeTouchCount >= 2)
        {
            return;
        }
        
        // 2. 한 손가락 탭 입력 감지
        foreach (var t in Touchscreen.current.touches)
        {
            if (t.press.wasPressedThisFrame)
            {
                Vector2 touchPosition = t.position.ReadValue();
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
                        if (hit.collider.CompareTag("Model"))
                        {
                            foundHit = hit;
                            hitTag = "Model";
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
                        //modelAnimator.SetTrigger("PlayHeadAnimation");
                        Debug.Log("Head touch animation start");
                    }
                    else if (hitTag == "Hand")
                    {
                        Debug.Log("Hand touch animation start");
                    }
                    else if (hitTag == "Model")
                    {
                        
                        Debug.Log("Model touch animation start");
                    }
                }
                else
                {
                    Debug.Log("No Animation");
                }
            }
        }
    }
}
