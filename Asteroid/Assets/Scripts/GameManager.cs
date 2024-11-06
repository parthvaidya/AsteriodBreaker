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
    int score = 0;
    public GameOverController gameOverController;
    public int scoreMultiplier = 1;
    public float scoreBoostDuration = 5.0f;

    public void Start()
    {
        UpdateLivesUI();
        UpdateScoreUI();
        gameOverController.gameObject.SetActive(false);
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

    public void AsteroidDestoryed(Asteroid asteroid)
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        //also add scoring logic here
        IncreaseScore(10);
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
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }


}
