using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Button _retry;
    [SerializeField] private Button _Skip;
    [SerializeField] private Button _Home;
    public LeanTweenType EaseType;


    private void Awake()
    {
        if (_gameOverPanel == null)
        {
            Debug.LogError("GameOverPanel is not assigned in the Inspector");
        }

        Observer.Addlistener("GameOver", OpenPanelOver);
    }

    private void OnDestroy()
    {
        Observer.Removelistener("GameOver", OpenPanelOver);
    }

    void Start()
    {
        _retry.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        _Home.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Lobby");
        });
    }

    void OpenPanelOver(object[] datas)
    {
        //SoundController.Instance.SFXPlay("Lose");
        if (_gameOverPanel != null)
        {
            LeanTween.scale(_gameOverPanel, new Vector3(1f, 1f, 1f), 0.2f).setEase(EaseType);
        }
        else
        {
            Debug.LogError("GameOverPanel is not assigned in the Inspector");
        }
    }
}
