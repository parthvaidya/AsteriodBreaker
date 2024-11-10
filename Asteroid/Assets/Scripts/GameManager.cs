using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public NewBehaviourScript player;
    public ParticleSystem explosion;
    public float respawnTime = 3.0f;

    public int lives = 3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI achievementPopupText;
    public TextMeshProUGUI popupText;
    int score = 0;
    public GameOverController gameOverController;
    public int scoreMultiplier = 1;
    public float scoreBoostDuration = 5.0f;
    private bool hasReached200Points = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Ensures there's only one GameManager
        }
        else
        {
            Instance = this;
        }
    }
    public void Start()
    {
        UpdateLivesUI();
        UpdateScoreUI();
        gameOverController.gameObject.SetActive(false);
        achievementPopupText.gameObject.SetActive(false);
    }
    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        SoundManager.Instance.Play(Sounds.PlayerDeath);
        this.lives--;

        UpdateLivesUI();
        if (this.lives <=0 )
        {
            GameOver();
        }

        
        

    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        //also add scoring logic here
        IncreaseScore(10);
    }

    public void ShowPopupMessage(string message)
    {
        popupText.text = message;
        popupText.gameObject.SetActive(true);
        Invoke(nameof(HidePopupMessage), 2.0f);  // Hide after 2 seconds
    }

    private void HidePopupMessage()
    {
        popupText.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        Debug.Log("Game over");
        Time.timeScale = 0;
        gameOverController.PlayerDied();
        this.enabled = false;
        //yet to implement it
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("ignoreCollisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), 3.0f);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void IncreaseScore(int increment)
    {
        score += increment * scoreMultiplier;
        UpdateScoreUI();
        CheckAchievements();

        if (score >= 300)
        {
            SceneManager.LoadScene(3); 
        }
    }
    public void ActivateScoreBooster()
    {
        StartCoroutine(ScoreBoosterCoroutine());
    }

    private IEnumerator ScoreBoosterCoroutine()
    {
        scoreMultiplier = 2; // Double the score
        yield return new WaitForSeconds(scoreBoostDuration);
        scoreMultiplier = 1; // Reset to normal after duration
    }

    private void CheckAchievements()
    {
        if (!hasReached200Points && score >= 200)
        {
            hasReached200Points = true;
            ShowAchievementPopup("Congratulation Player you just Scored 200 points!!");
            Debug.Log("200 Points!!");
        }

        
    }

    private void ShowAchievementPopup(string message)
    {
        achievementPopupText.text = message;
        achievementPopupText.gameObject.SetActive(true); // Show the popup
        SoundManager.Instance.Play(Sounds.PlayerMove);
        StartCoroutine(HideAchievementPopup());
    }

    private IEnumerator HideAchievementPopup()
    {
        yield return new WaitForSeconds(2f); // Show popup for 2 seconds
        achievementPopupText.gameObject.SetActive(false); // Hide popup
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }


}
