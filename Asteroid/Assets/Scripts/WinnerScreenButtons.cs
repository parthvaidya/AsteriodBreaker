using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerScreenButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public Button restartButton; // Assign in Inspector or find in code
    public Button lobbyButton;

    private void Start()
    {
        SoundManager.Instance.Play(Sounds.Winner);
        restartButton.onClick.AddListener(RestartGame);
        lobbyButton.onClick.AddListener(GoToLobby);
    }


    private void RestartGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(1); 
    }

    
    private void GoToLobby()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(0); 
    }
}
