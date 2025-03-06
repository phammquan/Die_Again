using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    [SerializeField] GameObject _player; // truy cập đến player
    [SerializeField] Vector3 _offset; //khoảng cách giữa player và camera
    [SerializeField] private float rateLerp; // tốc độ di chuyển của camera
    public bool gameOver;

    void Start()
    {
        
        _offset = transform.position - _player.transform.position;    
    }

    void Update()
    {
        
        if(!GameManager.Instance.GameOver)
        {
            CameraMove();
        }
        
    }

    void CameraMove()
    {
        Vector3 pos = this.transform.position;
        Vector3 targetPos = _player.transform.position + _offset;
        pos = Vector3.Lerp(pos, targetPos, rateLerp * Time.deltaTime);
        this.transform.position = pos;
    }
}