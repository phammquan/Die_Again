using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool _gameOver = false;
    public bool GameOver => _gameOver;
    void Start()
    {
        Observer.Addlistener("GameOver", Game_Over);
        Observer.Addlistener("Complete", CompleteLevel);
    }

    private void OnDestroy()
    {
        Observer.Removelistener("GameOver", Game_Over);
        Observer.Removelistener("Complete", CompleteLevel);
    }
    
    void Game_Over(object[] datas)
    {
        _gameOver = true;
        Debug.Log("Game Over");
    }
    void CompleteLevel(object[] datas)
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)  
        {
            StartCoroutine(LoadLevel(nextSceneIndex));
        }
        else
        {
            StartCoroutine(LoadLevel(0));
        }
    }

    IEnumerator LoadLevel(int index)
    {
        SoundController.Instance.SFXPlay("Win");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(index);
    }
}
