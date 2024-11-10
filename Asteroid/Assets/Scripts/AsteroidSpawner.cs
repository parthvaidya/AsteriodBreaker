using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : Spawner<Asteroid>
{



    protected override void SetSpawnProperties(Asteroid asteroid, Vector2 trajectory)
    {
        asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
        asteroid.SetTrajectory(trajectory);
    }


}
