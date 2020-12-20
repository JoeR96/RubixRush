using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuperSpawnerState : BaseSpawner
{
    private static int _superSpawnFloor = 1;
    private float _colourTimer = 2f;
    private int _durationBetweenColourChange = 2;
    private static bool _isIncrementing;
    private static int _stateCounter = 0;
    private static int _index = 1;
    private float distanceTravelled;
    private bool distancePopupActive;
    [SerializeField] private Button distanceButton;
    [SerializeField] private TextMeshProUGUI distanceText;
    private void Start()
    {
        _isIncrementing = true;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        IncrementSuperSpawn();
        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log(_superSpawnFloor);
        }
            
    }

    protected override void SpawnFloor()
    {
       
        _stateCounter += 1;
        if (_stateCounter == 1 && _index < 6)
        {
            _stateCounter = 0;
             _index += 1;
        }

        if (_index == 6)
        {
            //transition back to normal
            _gameManager.SetDefaultState();
        }
        PoolObjectType type = (PoolObjectType) (_index);
        foreach (var spawnPoint in _spawnPoints)
        {
            var superFloor = ObjectPooler.instance.GetObject(type);
            var targetLocation = spawnPoint.transform;
            superFloor.transform.SetParent(targetLocation);
            SetFloorPosition(spawnPoint.transform, superFloor);
            var coin = GetCoin();
            SetObject(coin,spawnPoint);
        }
    }

    private void IncrementSuperSpawn()
    {

    }
    

    
    public bool IsDivisible(float x,float y)
    {
        //will return true if x is divisible by y - increase later after testing 
        return (x % y) == 0;
    }
}


