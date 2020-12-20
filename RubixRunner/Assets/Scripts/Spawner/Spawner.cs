using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    [SerializeField] ObjectSpawner _masterSpawner;
    [SerializeField] private DataScriptableObject _data;
    [SerializeField] private GameObject _spawn;
    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();
    private List<GameObject> _pooledObjects = new List<GameObject>();
    private float _distanceTravelled;
    
    //do we want these or can we do it another way?//inheritance/case?
    private bool SpawnEmpty = false;
    private static bool SpawnPickups = false;
    private bool SuperSpawn = false;
    private bool dynamicTutorial;
    private bool floorDynamic;
    
    //timer - return percentage complete as empty spawning chance - this will remove the array/pointer
    private float[] emptyChance = new float [4] {0.25f, 0.5f, 0.75f, 1f};
    private int floorPointer = 0;
    private float floorTimer = 15f;
    
    private float gameTimer = 4f;
    
    public float moveSpeed = 0.15f;
    

    public float distanceTravelled { get; private set; }
    float colourDuration;
    private float colourTimer;

    
    //look to change/increment with timer
    public int gameIncrementer = 9;
    
    
    private void Awake()
    {
        dynamicTutorial = _data.GetTutorialMode();
        floorDynamic = false;
        SpawnPickups = false;
        SpawnEmpty = false;
        //dynamic tutorial variables
        _spawn = GameObject.FindGameObjectWithTag("GameManager");
        _masterSpawner = _spawn.GetComponent<ObjectSpawner>();
    }
    private void OnEnable()
    {
        SpawnFloor();
        Debug.Log(dynamicTutorial);
    }
    private void Update()
    {
        transform.Translate(new Vector3(0f, 0f, -1f) * moveSpeed);
        if (dynamicTutorial)
        {
            DynamicalTutorialTimer(); 
        }
    }

    private void DynamicalTutorialTimer()
    {
        if (gameIncrementer < 13)
        {
            if (gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
            }
        
            if (gameTimer <= 0)
            {
                
                gameIncrementer++;
                //Debug.Log(gameIncrementer);
                gameTimer = 3f;
            }

            if (gameIncrementer == 11)
            {
                SpawnPickups = true;
            }
            if (gameIncrementer == 15)
            {
                SpawnEmpty = true;
            }
        }
        if (floorTimer > 0)
        {
            floorTimer -= Time.deltaTime;
        }
        
        if (floorDynamic)
        {
            if (floorTimer <= 0)
            {
                if (floorPointer < 3)
                {
                    floorPointer++;
                }
                else
                {
                    floorDynamic = false;
                }
            }
        }
    }

    

    
 
    //can probably override this method one day after TD
    public void SpawnFloor()
    {
        foreach (var spawnPoint in _spawnPoints)
        {

            var floor = GetFloor();
            var targetLocation = spawnPoint.transform;
            floor.transform.SetParent(targetLocation);
            SetFloorPosition(spawnPoint.transform, floor);
            
             
             if (dynamicTutorial)
             {
                 if (SpawnPickups && Random.value < 0.5f)
                 {
                     Debug.Log("This should happen");
                     var toSet = GetDynamicPickUp();
                     SetDynamicPickUp(toSet, spawnPoint);
                 }
             }
             
             else if (Random.value < 0.1f)
             {
                 var toSet = GetPickup();
                 SetPickUp(toSet,spawnPoint);
             }
             
             if(SuperSpawn)
             {
             
                 var toSet = GetCoinPickup();
                 SetPickUp(toSet,spawnPoint);
             }
            
        } 
    }
    
    public void ReturnFloor()
    {
        foreach (var floor in _pooledObjects)
        {
            ObjectPooler.instance.ReturnObject(floor,floor.GetComponent<FloorType>().PoolType);
        }
    }
    
    

    #region GetPiece

    private void SpawnPickup(Transform spawnPoint)
    {
        
        var pickUp = GetPickup();
        SetPickupPosition(spawnPoint.transform,pickUp);
        pickUp.transform.SetParent(spawnPoint.transform);
    }
    private GameObject GetFloor()
    { 
        PoolObjectType type = (PoolObjectType) Random.Range(0, 7);
        
        if (SpawnEmpty)
        {
            //spawn floor less often
            type = (PoolObjectType) Random.Range(0,5 );
            if (type == 0)
            {
                if(Random. value > emptyChance[floorPointer]) //%80 percent chance.
                {
                    type = (PoolObjectType) Random.Range(1,5);
                }
            }
        }
        else
        {
            type = (PoolObjectType) Random.Range(0,6 ); 
            
        }
        var floorToReturn = ObjectPooler.instance.GetObject(type);
        _pooledObjects.Add(floorToReturn);
        return floorToReturn;
    }

    private GameObject GetFloorEmpty()
    {
        PoolObjectType type = (PoolObjectType) Random.Range(0,6);
        var floorToReturn = ObjectPooler.instance.GetObject(type);
        return floorToReturn;
    }
    
    private GameObject GetFloorSuperSpawn()
    {
        PoolObjectType type = (PoolObjectType) _masterSpawner.MoveCount;
        var floorToReturn = ObjectPooler.instance.GetObject(type);
        return floorToReturn;
    }
    
    //Object pool returners
    private GameObject GetPickup()
    {
        var type = (PoolObjectType) Random.Range(9,11);
        var toReturn = ObjectPooler.Instance.GetObject(type);
        return toReturn;
    }

    //coins
    private GameObject GetCoinPickup()
    {
        var toReturn = ObjectPooler.Instance.GetObject(PoolObjectType.PICKUPSCORE);
        return toReturn;
    }
    //buffs
    private GameObject GetBuff()
    {
        var toReturn = ObjectPooler.Instance.GetObject(PoolObjectType.PICKUPFASTPLAYER);
        return toReturn;
    }

    private GameObject GetDebuff()
    {
        var toReturn = ObjectPooler.Instance.GetObject(PoolObjectType.PICKUPSLOWPLAYER);
        return toReturn;
    }

    private GameObject GetObstacle()
    {
        var toReturn = ObjectPooler.Instance.GetObject(PoolObjectType.OBSTACLE);
        return toReturn;
    }
    private GameObject GetDynamicPickUp()
    {
        var type = (PoolObjectType) (Random.Range(9, gameIncrementer));
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
    public void SetPickUp(GameObject pickup,GameObject parent)
    {
        var targetLocation = parent.transform.GetChild(0).transform;
        pickup.transform.parent = parent.transform.GetChild(0);
        pickup.transform.position = targetLocation.transform.position;
        pickup.transform.rotation = targetLocation.transform.rotation;
    }
    private static void SetFloorPosition(Transform target, GameObject toPosition)
    {
        var transform1 = target.transform;
        toPosition.transform.parent = transform1;
        toPosition.transform.position = transform1.position;
        toPosition.transform.rotation = transform1.rotation;
    }

    private static void SetPickupPosition(Transform target, GameObject toPosition)
    {
        var transform1 = target.transform;
        toPosition.transform.position = transform1.position;
        toPosition.transform.rotation = transform1.rotation;
    }
    #endregion
    
    #region GameEffects

    public void StopMoving()
    {
        moveSpeed = 0f;
    }

    public void StartMoving()
    {
        moveSpeed = 0.15f;
    }
    

    public void SlowDownMovement()
    {
        //pass variable in
        StartCoroutine(LerpVariable(0.2f));
    }

    public void SpeedUpMovement()
    {
        //pass variable in
        StartCoroutine(LerpVariable(0.3f));
    }

    public void SuperSpeed()
    {
        SpawnEmpty = false;
        SuperSpawn = true;
        //Increment variable
        //Timer
        //Add One
        //Go Again
    }

    #endregion
    //TO DEAL WITH
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EndOfLine"))
        {
            ReturnFloor();
            Destroy(gameObject);
            _masterSpawner.SpawnHolder();
            
            
        }
    }
    
    //can this go in a parent?
    public IEnumerator LerpVariable(float target)
    {
        var m_rotateTime = 1f;
        float timer = 0;
        var start = moveSpeed;
        while (timer < m_rotateTime)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / m_rotateTime, 1);
            moveSpeed = Mathf.Lerp(start, target, percentage);
            yield return null;
        }
    }
}
