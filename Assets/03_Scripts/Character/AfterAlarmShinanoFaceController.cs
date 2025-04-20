using System;
using UnityEngine;
using System.Collections;

public class AfterAlarmShinanoFaceController : MonoBehaviour
{
   private Animator animator;

   private void Start()
   {
       
       StartCoroutine(FaceSequence());
   }

   IEnumerator FaceSequence()
   {
       animator.SetLayerWeight(animator.GetLayerIndex("Facial Layer"), 0);
       
       yield return new WaitForSeconds(1.5f);
       animator.SetLayerWeight(animator.GetLayerIndex("Facial Layer"), 1);
       animator.SetInteger("FaceState", 1);
       
       yield return new WaitForSeconds(0.3f);
       animator.SetInteger("FaceState", 2);
   }
}
