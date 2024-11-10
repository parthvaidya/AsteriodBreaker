using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T prefab;
    [SerializeField] protected float spawnDistance = 12f;
    [SerializeField] protected float spawnRate = 1f;
    [SerializeField] protected int amountPerSpawn = 1;
    [Range(0f, 45f)]
    [SerializeField] protected float trajectoryVariance = 15f;

    protected virtual void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    protected void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = transform.position + (spawnDirection * spawnDistance);

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            T instance = Instantiate(prefab, spawnPoint, rotation);
            SetSpawnProperties(instance, rotation * -spawnDirection);
        }
    }

    // Abstract method to be implemented by derived classes for setting unique properties
    protected abstract void SetSpawnProperties(T instance, Vector2 trajectory);
}
