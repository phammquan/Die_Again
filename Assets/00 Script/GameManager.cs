using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _gameOver = false;
    public bool GameOver => _gameOver;
    void Start()
    {
        Observer.Addlistener("GameOver", Game_Over);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Game_Over(object[] datas)
    {
        _gameOver = true;
    }
}
