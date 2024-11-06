using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Powerups : MonoBehaviour
{

    public enum PowerupType { IncreaseSpeed, ScoreBooster, ExtraLives }
    public PowerupType powerupType;

    private Rigidbody2D _rigidbody;
    public float speed = 1.5f;
    public float maxLifetime = 10f;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ApplyPowerup();
            SoundManager.Instance.Play(Sounds.collectItem);
            Destroy(gameObject);
        }
    }

    private void ApplyPowerup()
    {
        var gameManager = FindObjectOfType<GameManager>();
        
        switch (powerupType)
        {
            case PowerupType.IncreaseSpeed:
                gameManager.player.thrustSpeed += 2f;
                Debug.Log("Speed Increased X2");
                gameManager.ShowPopupMessage("Speed Boost Activated!");

                break;
            case PowerupType.ScoreBooster:
                gameManager.ActivateScoreBooster();
                Debug.Log("Score Booser now Active !! ");
                gameManager.ShowPopupMessage("Score Booster Active!");

                break;
            case PowerupType.ExtraLives:
                gameManager.lives += 2;
                Debug.Log("Lives Increases!!");
                gameManager.ShowPopupMessage("Extra Lives Increases!");
                gameManager.UpdateLivesUI();
                break;
        }

        
    }

   
}

