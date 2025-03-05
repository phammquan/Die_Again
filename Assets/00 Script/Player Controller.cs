using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Move")] [SerializeField] private float _speed;
    private float _oldSpeed;
    [SerializeField] float _jumpForce;
    private Rigidbody _rb;
    [SerializeField]private bool _isGrounded = true;
    public bool IsGrounded => _isGrounded;
    [Space] [SerializeField] private Button _jumpButton;
    [SerializeField] private Joystick _joystick;
    private float horizontal;
    private float vertical;

    void Start()
    {
         _oldSpeed = _speed;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _jumpButton.onClick.AddListener(Jump);
    }

    private void Update()
    {
        horizontal = _joystick.Horizontal;
        vertical = _joystick.Vertical;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _speed /= 2;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }
    
    void FixedUpdate()
    {
        Move();
        CheckGroud();
    }

    void Move()
    {
        

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 velocity = _rb.velocity;
        velocity.x = moveDirection.x * _speed;
        velocity.z = moveDirection.z * _speed;
        _rb.velocity = velocity;

        if (moveDirection.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
    void CheckGroud()
    {
        float distanst = 0.5f;
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, distanst))
        {
            if (hit.collider.CompareTag("Platform"))
            {
                _isGrounded = true;
                _speed = _oldSpeed;
            }
        }
        Debug.DrawRay(transform.position, Vector3.down * distanst, Color.red);
    }
}