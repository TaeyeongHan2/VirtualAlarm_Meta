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

    public float moveRadius = 0.04f;
    public float moveSpeed = 0.25f;
    private float timer = 0f;
    
    private bool wasTracking = false;
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
                    modelPrefab.SetActive(true);
                    modelPrefab.transform.position = trackedImage.transform.position;
                    modelPrefab.transform.rotation = trackedImage.transform.rotation;
                }
                
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
                
                // 선택Animator
                // var anim = modelPrefab.GetComponenetInChildren<Animator>();
                //if(anim != null) anim.SetTrigger("Idle");
                    //Debug.Log("[AR] model on");
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
            }
        }
        wasTracking = found;
    }
}
