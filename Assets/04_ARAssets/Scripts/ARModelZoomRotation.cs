using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ARModelZoomRotation : MonoBehaviour
{
    [Header("Model Object")] 
    public Transform headTransform;
    public Transform modelTransform;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private ARImageTrackingManager arImageTrackingManager;
    
    [Header("Zoom/Rotation Settings")]
    public float zoomSpeed = 0.002f;
    public float minHeadScale = 0.5f;
    public float maxHeadScale = 1.5f;
    public float rotationSpeed = 0.5f;

    private bool isPinching = false;
    private float prevTouchDistance = 0f;
    private Vector2 prevTouchVector;
    private bool isRotating = false;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    

    private void Start()
    {
        if (headTransform != null)
        {
            originalScale = headTransform.localScale;
        }

        if (modelTransform != null)
        {
            originalRotation = modelTransform.rotation;
        }
    }
    void Update()
    {
        // UI 위라면 Ar 터치 입력 모두 무시
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //  멀티터치(핀치/회전)
        int activeTouchCount = 0;
        Vector2[] positions = new Vector2[2];
        int idx = 0;
        foreach (var t in Touchscreen.current.touches)
        {
            if (t.press.isPressed)
            {
                // 두 손가락까지만 기록
                if (idx < 2)
                {
                    positions[idx++] = t.position.ReadValue();
                }
                activeTouchCount++;
            }
        }

        if (activeTouchCount < 2)
        {
            isPinching = false;
            isRotating = false;
            return;
        }
        Vector2 p0 = positions[0];
        Vector2 p1 = positions[1];
        
        //Debug.Log($"Zoom touch pos0: {p0}, pos1: {p1}");
        // 머리 부분 위에 두 손가락 중 하나라도 올려져 있으면 -> 얼굴 줌
        if (IsTouchOnHead(p0) || IsTouchOnHead(p1))
        {
            // 얼굴 확대 축소
            if (!isPinching)
            {
                isPinching = true;
                prevTouchDistance = Vector2.Distance(p0, p1);
                // 회전 모드 해제
                isRotating = false;
                return;
            }
            float curTouchDistance = Vector2.Distance(p0, p1);
            float deltaMagnitude = curTouchDistance - prevTouchDistance;
            float scaleFactor = 1f + deltaMagnitude * zoomSpeed;
            
            Vector3 newScale = headTransform.localScale * scaleFactor;
            newScale.x = Mathf.Clamp(newScale.x, minHeadScale, maxHeadScale);
            newScale.y = Mathf.Clamp(newScale.y, minHeadScale, maxHeadScale);
            newScale.z = Mathf.Clamp(newScale.z, minHeadScale, maxHeadScale);
            headTransform.localScale = newScale;
            
            prevTouchDistance = curTouchDistance;
        }
        else
        {
            // model 전체 회전
            if (!isRotating)
            {
                isRotating = true;
                prevTouchVector = p1 - p0;
                // 줌 모드 해제
                isPinching = false;
                //return;
            }
            Vector2 curTouchVector = p1 - p0;
            float prevAngle = Mathf.Atan2(prevTouchVector.y, prevTouchVector.x) * Mathf.Rad2Deg;
            float curAngle = Mathf.Atan2(curTouchVector.y, curTouchVector.x) * Mathf.Rad2Deg;
            float deltaAngle = Mathf.DeltaAngle(prevAngle, curAngle);
            
            modelTransform.Rotate(Vector3.up, -deltaAngle * rotationSpeed, Space.World);
            prevTouchVector = curTouchVector;
        }
    }
    
    // 얼굴에 터치가 시작됐는지 Raycast 체크
    bool IsTouchOnHead(Vector2 screenPos)
    {
        if (headTransform == null || arCamera == null)
        {
            //Debug.Log("headTransfomr or arcamera is null");
            return false;
        }
        Ray ray = arCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Debug.Log($"Zoom RaycastHit : {hit.collider.name}");
            return hit.transform == headTransform;
        }
        //Debug.Log("Zoom Raycast miss");
        return false;
    }

    // Reset 버튼
    public void ResetZoomAndRotation()
    {
        if (headTransform != null)
        {
            headTransform.localScale = originalScale;
        }

        if (modelTransform != null && arImageTrackingManager != null)
        {
            var trackedImage = arImageTrackingManager.GetCurrentlyTrackingImage();
            if (trackedImage != null)
            {
                modelTransform.rotation = trackedImage.transform.rotation *
                                          Quaternion.Euler(0f, arImageTrackingManager.ModelYRotationOffset, 0f);
            }
            
        }
        Debug.Log("UI Btn Click : Zoom/Rotation Reset");
    }
}
