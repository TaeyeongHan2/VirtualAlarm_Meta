using TMPro;
using UnityEngine;

public class HoursScrollView : MonoBehaviour
{
    public static HoursScrollView Instance { get; private set; }
    public string hours;
    public Vector3 hoursTargetPosition;
    public GameObject sellectedGO;
    //public RectTransform searchArea;

    void Awake()
    {
        Instance = this;
        //searchArea = gameObject.GetComponent<RectTransform>();
        hoursTargetPosition = gameObject.transform.position;
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

    void FixedUpdate()
    {
        GameObject _ = FindCenterObject(hoursTargetPosition);
        if (_ != null)
        {
            sellectedGO = _;
            hours = sellectedGO.GetComponent<TextMeshProUGUI>().text;
            //Debug.Log(hours);
        }
        else
        {
            //Debug.Log("현재 위치에 오브젝트 없어서 이전 정보 갱신 안됨");
        }
    }
}
