using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    //fields accessed publically cannot be private
    public float size = 1f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50f;
    public float maxLifetime = 30f;
   
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        
        transform.localScale = Vector3.one * size;
        rb.mass = size;

        
        Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        // The asteroid only needs a force to be added once since they have no
        // drag to make them stop moving
        rb.AddForce(direction * this.movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if ((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            GameManager.Instance.AsteroidDestroyed(this);
            
            SoundManager.Instance.Play(Sounds.AsteroidBreak);
            Destroy(this.gameObject);
        }
    }


    private Asteroid CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        // Create the new asteroid at half the size of the current
        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        // Set a random trajectory
        half.SetTrajectory(Random.insideUnitCircle.normalized );
        SoundManager.Instance.Play(Sounds.AsteroidBreak);
        return half;
    }
}
