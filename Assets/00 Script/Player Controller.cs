using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Move")] [SerializeField] private float _speed;
    [SerializeField] float _jumpForce;

    private Rigidbody _rb;
    [SerializeField] private bool _isGrounded = true;
    public bool IsGrounded => _isGrounded;
    [Space] [SerializeField] private Button _jumpButton;
    [SerializeField] private Joystick _joystick;
    private float horizontal;
    private float vertical;

    private bool _gameOver;
    public bool GameOver => _gameOver;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _jumpButton.onClick.AddListener(Jump);
    }

    private void Update()
    {
        horizontal = (_joystick.Horizontal != 0) ? _joystick.Horizontal : Input.GetAxis("Horizontal");
        vertical = (_joystick.Vertical != 0) ? _joystick.Vertical : Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundController.Instance.SFXPlay("Jump");
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (!_gameOver)
        {
            Move();
        }

        CheckGameOver();
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }


    void CheckGameOver()
    {
        if (this.transform.position.y < -7 && !_gameOver)
        {
            SoundController.Instance.SFXPlay("Die");
            _gameOver = true;
            Observer.Notify("GameOver", null);
        }
    }
}