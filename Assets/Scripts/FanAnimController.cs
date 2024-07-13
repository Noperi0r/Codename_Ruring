using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanAnimController : MonoBehaviour
{
    // Referenced by animation clip event
    void OnAnimationComplete()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
