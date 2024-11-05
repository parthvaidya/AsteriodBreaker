using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public Powerups powerupPrefab;
    public float spawnDistance = 12f;
    public float spawnRate = 3f; 
    public int amountPerSpawn = 1;
    [Range(0f, 45f)]
    public float trajectoryVariance = 15f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = transform.position + (spawnDirection * spawnDistance);

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Powerups powerup = Instantiate(powerupPrefab, spawnPoint, rotation);
            powerup.powerupType = (Powerups.PowerupType)Random.Range(0, System.Enum.GetValues(typeof(Powerups.PowerupType)).Length);

            powerup.SetTrajectory(rotation * -spawnDirection);
        }
    }
}


