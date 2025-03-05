using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerState = PlayerState.playerState;

public class Animation : MonoBehaviour
{
    Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(playerState playerState)
    {
        for (int i = 0; i <= (int)playerState.DIE; i++)
        {
            string stateName = ((playerState)i).ToString();
            if (playerState == ((playerState)i))
            {
                _animator.SetBool(stateName, true);
            }
            else
            {
                _animator.SetBool(stateName, false);
            }
        }

    }
}
