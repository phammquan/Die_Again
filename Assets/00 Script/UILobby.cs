using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
   [SerializeField] Button _playButton;
   
    void Start()
    {
        _playButton.onClick.AddListener(() => { SceneManager.LoadScene("Level1"); });
    }
    
}
