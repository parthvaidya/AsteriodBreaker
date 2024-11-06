using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button restart;
    public Button quitGame;

    private void Awake()
    {
        restart.onClick.AddListener(startGame);
        quitGame.onClick.AddListener(Quit);
    }

    public void PlayerDied()
    {
        gameObject.SetActive(true);
    }

    private void startGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }


    private void Quit()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}
