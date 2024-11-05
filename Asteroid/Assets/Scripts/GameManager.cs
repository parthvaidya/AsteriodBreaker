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


    public void Start()
    {
        UpdateLivesUI();
        UpdateScoreUI();
    }
    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;

        UpdateLivesUI();
        if (this.lives <=0 )
        {
            GameOver();
        }

        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
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

    private void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        UpdateScoreUI();

        
        if (score >= 250)
        {
            SceneManager.LoadScene(3); 
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
}
