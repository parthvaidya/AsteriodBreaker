using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : Spawner<Powerups>
{

    protected override void SetSpawnProperties(Powerups powerup, Vector2 trajectory)
    {
        powerup.powerupType = (Powerups.PowerupType)Random.Range(0, System.Enum.GetValues(typeof(Powerups.PowerupType)).Length);
        powerup.SetTrajectory(trajectory);
    }
    
}


