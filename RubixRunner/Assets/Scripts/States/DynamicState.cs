using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DynamicState : BaseSpawner
{
    public static int GameIncrementer = 7;
    private float _gameTimer = 4f;
    private int _durationBetweenIncrease = 4;
    private static bool _isIncrementing;
    private static bool _spawnEmpty { get; set; }
    
    // Start is called before the first frame update
    private void Start()
    {
        _isIncrementing = true;
    }
    
    // Update is called once per frame0

    private new void Update()
    {
        base.Update();
        IncrementTutorialStage();
        FloorTimer();
    }

    protected override void SpawnFloor()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            //this is in base we want to try and remove this
            var floor = _spawnEmpty ? GetFloorEmpty() : GetFloor();
            var targetLocation = spawnPoint.transform;
            floor.transform.SetParent(targetLocation);
            SetFloorPosition(spawnPoint.transform, floor);
            
            //create a condition for this to spawn
            
            if (Random.value < 0.05f)
            {
                var toSet = GetDynamicPickUp();
                SetDynamicPickUp(toSet,spawnPoint);
            }
        }
    }
    
    private bool _spawnPickups;

    private GameObject GetDynamicPickUp()
    {
        var type = (PoolObjectType) (Random.Range(7, GameIncrementer));

        var pickup = ObjectPooler.Instance.GetObject(type);
        return pickup;
    }
    
    private void SetDynamicPickUp(GameObject pickup,GameObject parent)
    {
        var targetLocation = parent.transform.GetChild(0).transform;
        pickup.transform.parent = parent.transform.GetChild(0);
        pickup.transform.position = targetLocation.transform.position;
        pickup.transform.rotation = targetLocation.transform.rotation;
    }

    private void FloorTimer()
    {
        float timer = 14f;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (!(timer <= 0))
            _spawnEmpty = true;
    }
    
    private void IncrementTutorialStage()
    {
        if (_isIncrementing)
        {
            if (_gameTimer > 0)
            {
                _gameTimer -= Time.deltaTime;
            }
        
            if (GameIncrementer == 10)
            {
                _isIncrementing = false;
                return;
            }
            
            if (!(_gameTimer <= 0)) return;
            GameIncrementer++;
            _gameTimer = _durationBetweenIncrease;
        }
    }
}
