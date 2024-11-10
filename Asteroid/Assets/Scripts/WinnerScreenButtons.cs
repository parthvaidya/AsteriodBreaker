using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerScreenButtons : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button restartButton; // Assign in Inspector or find in code
    [SerializeField] private Button lobbyButton;

    private void Start()
    {
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
