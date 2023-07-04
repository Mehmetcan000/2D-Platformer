using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
   public SpriteRenderer spriteRenderer;


   public Vector3 ReturnEndPoint()
   {

      Vector3 calculatedEndPoint ;
      calculatedEndPoint.x = spriteRenderer.bounds.size.x + this.transform.position.x;
      calculatedEndPoint.y =Random.Range(0,2);
      calculatedEndPoint.z = 0;

      return calculatedEndPoint;
   }
 
  
  
  
  
}
