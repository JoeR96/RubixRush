using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEnvironment : MonoBehaviour
{

    private float _xRange;
    private float _yRange;
    private float _zRange;
    
    private float ReturnSpawnPosition(float range)
    {
        var toReturn = Random.Range(-range, range);
        return toReturn;
    }

    private void SpawnStar()
    {
        var toSpawn = Random.Range(12, 15);
    }
    
    
}
