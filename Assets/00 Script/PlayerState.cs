using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] playerState _playState = playerState.IDLE;
    Rigidbody _rigi;
    Animation _animation;
    const float velocityThreshold = 0.1f;

    void Start()
    {
        _rigi = GetComponent<Rigidbody>();
        _animation = this.transform.GetChild(0).gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        UpdateState();
        if (_animation != null)
        {
            _animation.UpdateAnimation(_playState);
        }
    }

    void UpdateState()
    {
        if (!PlayerController.Instance.IsGrounded)
        {
            _playState = playerState.JUMP;
        }
        else
        {
            if (Mathf.Abs(_rigi.velocity.x) > velocityThreshold || Mathf.Abs(_rigi.velocity.z) > velocityThreshold)
            {
                _playState = playerState.RUN;
            }
            else
            {
                _playState = playerState.IDLE;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone"))
        {
            _playState = playerState.DIE;
            Observer.Notify("GameOver", null);
        }
    }

    public enum playerState
    {
        IDLE,
        RUN,
        JUMP,
        DIE
    }
}