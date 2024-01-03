using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
     
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) 
            {
                Debug.Log("Touch began");
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("the touching continues"); 
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) 
            {
                Debug.Log("Touch is over");
            }
        }
    }
}
