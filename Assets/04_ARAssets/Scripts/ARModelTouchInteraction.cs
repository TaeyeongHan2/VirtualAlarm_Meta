using UnityEngine;

public class ARModelTouchInteraction : MonoBehaviour
{
    // public Animator animator;
    /*
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject touchedObject = hit.collider.gameObject;
                Debug.Log("터치 : " + touchedObject);

                if (touchedObject.layer == LayerMask.NameToLayer("Head"))
                {
                    Debug.Log("Nod");
                    // animator.SetTrigger("Nod");
                }
                else if (touchedObject.layer == LayerMask.NameToLayer("Hand"))
                {
                    Debug.Log("Wave");
                    // animator.SetTrigger("Wave");
                }
            }
        }
    }
    */

}
