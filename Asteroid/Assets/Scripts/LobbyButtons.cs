using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyButtons : MonoBehaviour
{
    public Button startButton; 
    public Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(startGame);
        quitButton.onClick.AddListener(Quit);
    }


    private void startGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(1);
    }


    private void Quit()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(2);
    }
}
