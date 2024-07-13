using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimController : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("playEffect");
    }

    void OnAnimationComplete()
    {
        transform.gameObject.SetActive(false);
    }

}
