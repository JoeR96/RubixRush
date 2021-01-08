using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    protected Manager _gameManager;
    [SerializeField] protected List<GameObject> _spawnPoints = new List<GameObject>();
    protected List<GameObject> _pooledObjects = new List<GameObject>();
    public float MoveSpeed { get; set; }
    private float beforePauseSpeed;

    protected void Awake()
    {
        MoveSpeed = 15f;
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<Manager>();
    }

    protected virtual void OnEnable()
    {

        SpawnFloor();
    }
    
    protected void Update()
    {
        
        Move();
    }
    
    protected void Move()
    {
        transform.Translate(new Vector3(0f, 0f, -1f)  * Time.deltaTime * MoveSpeed);
    }

    public void Stop()
    {
        Debug.Log("Hi");
        MoveSpeed = 0;
    }
    
    public void SlowDown()
    {
        MoveSpeed = 0.075f;
    }
    public void SpeedUp()
    {
        MoveSpeed = 0.6f;
    }

    protected virtual void SetObject()
    {
        
    }
    protected virtual void SpawnFloor()
    {
        //override this method with the relevant game state
        foreach (var spawnPoint in _spawnPoints)
        {

            var floor = GetFloor();
            var targetLocation = spawnPoint.transform;
            floor.transform.SetParent(targetLocation);
            SetFloorPosition(spawnPoint.transform, floor);
        }
    }
    //use a bool to merge these?
    protected virtual void SpawnFloorEmpty()
    {
        //override this method with the relevant game state
        foreach (var spawnPoint in _spawnPoints)
        {
            var floor = GetFloorEmpty();
            var targetLocation = spawnPoint.transform;
            floor.transform.SetParent(targetLocation);
            SetFloorPosition(spawnPoint.transform, floor);
        }
    }
    
    protected void SetFloorPosition(Transform target, GameObject toPosition)
    {
        var transform1 = target.transform;
        toPosition.transform.parent = transform1;
        toPosition.transform.position = transform1.position;
        toPosition.transform.rotation = transform1.rotation;
    }
    protected GameObject GetFloor()
    {
        PoolObjectType floorType = (PoolObjectType) Random.Range(1, 7);
        var floorToReturn = ObjectPooler.instance.GetObject(floorType);
        _pooledObjects.Add(floorToReturn);
        return floorToReturn;
    }

    protected GameObject GetFloorEmpty()
    {
        PoolObjectType type = (PoolObjectType) Random.Range(0,6);
        var floorToReturn = ObjectPooler.instance.GetObject(type);
        return floorToReturn;
    }
    
    protected void SetPickup(GameObject pickup,GameObject parent)
    {
        var targetLocation = parent.transform.GetChild(0).transform;
        pickup.transform.parent = parent.transform.GetChild(0);
        pickup.transform.position = targetLocation.transform.position;
        pickup.transform.rotation = targetLocation.transform.rotation;
    }
    
    protected void ReturnFloor()
    {
        foreach (var floor in _pooledObjects)
        {
            ObjectPooler.instance.ReturnObject(floor,floor.GetComponent<FloorType>().PoolType);
        }
    }
    //end of floor

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndOfLine"))
        {
            _gameManager.SpawnFloorHolder();
            ReturnFloor();
            var t = Random.Range(1, 40);
            if (t == 1)
            {
                _gameManager.SetSuperSpawnState();
            }
        }
        
    }
    //interactive items

}
