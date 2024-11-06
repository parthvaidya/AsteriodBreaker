using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{

    public enum PowerupType { IncreaseSpeed, ExtraBullets, ExtraLives }
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
                break;
            case PowerupType.ExtraBullets:
                gameManager.player.bullets.maxLifetime += 3; 
                break;
            case PowerupType.ExtraLives:
                gameManager.lives += 2;
                gameManager.UpdateLivesUI();
                break;
        }
    }
}

