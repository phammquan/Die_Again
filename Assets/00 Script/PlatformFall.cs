using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    BoxCollider _collider;
    public LeanTweenType EaseType;

    

    void Start()
    {
        _collider = this.transform.GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LeanTween.moveLocalY(this.gameObject, -9f, 0.2f).setEase(EaseType);
        }
    }

}