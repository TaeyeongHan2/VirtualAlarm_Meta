using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AMPMScrollView : MonoBehaviour
{
    public static AMPMScrollView Instance { get; private set; }
    public string AMorPM;
    public Vector3 AMorPMtargetPosition;
    public GameObject sellectedGO;
    //public RectTransform searchArea;
    private void Start()
    {
    }

    void Awake()
    {
        //searchArea = gameObject.GetComponent<RectTransform>();
        Instance = this;
        AMorPMtargetPosition = gameObject.transform.position;
    }

    private GameObject FindCenterObject(Vector3 mp)
    {
        //Debug.Log(gameObject.name + "의 월드 포지션: " + mp);
        
        Ray ray = new Ray(mp, Vector3.forward);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        
        if (hit.collider)
        {
            //Debug.Log(hit.collider.name);
            return hit.collider.gameObject;
        }
        else
        {
            //Debug.Log(ray.origin); // 체크중인 위치
            return null; // collider 없으면 null 반환
        }
    }

    void Update()
    {
        GameObject _ = FindCenterObject(AMorPMtargetPosition);
        if (_ != null)
        {
            sellectedGO = _;
            //Debug.Log(sellectedGO.name);
            AMorPM = sellectedGO.GetComponent<TextMeshProUGUI>().text;
            //Debug.Log(AMorPM);
        }
        else
        {
            //Debug.Log("현재 위치에 오브젝트 없어서 이전 정보 갱신 안됨");
        }
    }
}
