using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    [SerializeField]private GameObject spawn;

    public bool SpawnEmpty = true;
    public bool SpawnObstacles = false;
    public bool SpawnPickups = false;
    public bool SpawnCoins = false;
    public bool SpawnBuff = false;
    public bool SpawnDebuff = false;
    public ObjectSpawner masterSpawner;
    //seperate spawner
    //seperate pickup
    //lets use some nice OOP here
    public float moveSpeed = 0.15f;
    public bool SuperSpawn = false;

    float colourDuration;
    private float colourTimer;

    public int gameIncrementer = 9;
    
    public int moveCount { get; set; }
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    private void Awake()
    {
        SuperSpawn = false;
        floorDynamic = true;
        SpawnPickups = true;
        SpawnEmpty = false;
        //dynamic tutorial variables
        spawn = GameObject.FindGameObjectWithTag("GameManager");
        masterSpawner = spawn.GetComponent<ObjectSpawner>();
        
    }
    private void OnEnable()
    {
        SpawnFloor();
    }

    private bool floorDynamic;
    private float[] emptyChance = new float [4] {0.25f, 0.5f, 0.75f, 1f};
    private int floorPointer = 0;
    private float floorTimer = 15f;
    private float gameTimer = 4f;
    private bool dynamicTutorial;
    private void Update()
    {

        transform.Translate(new Vector3(0f, 0f, -1f) * moveSpeed);
        if (!dynamicTutorial)
        {
            DynamicalTutorialTimer();
        }

        if (Input.GetKey(KeyCode.J))
        {
            SuperSpawn = true;
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
                Debug.Log(gameIncrementer);
                gameTimer = 3f;
            }
        
            if (gameIncrementer == 13)
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
                    Debug.Log(floorPointer);
                    floorPointer++;
                }
                else
                {
                    Debug.Log(floorPointer);
                    Debug.Log("Should be 3");
                    floorDynamic = false;
                }
            }
        }
    }

    private GameObject GetFloor()
    {
        PoolObjectType  type;
        if (SpawnEmpty)
        {
            
            //spawn floor less often
            type = (PoolObjectType) Random.Range(0,6 );
            if (type == 0)
            {
                if(Random. value > emptyChance[floorPointer]) //%80 percent chance.
                {
                    type = (PoolObjectType) Random.Range(1,6);
                }
            }
        }
        else
        {
            type = (PoolObjectType) Random.Range(1,6 );
        }
        var floorToReturn = ObjectPooler.instance.GetObject(type);
        return floorToReturn;
    }

    private GameObject GetFloorSuperSpawn()
    {
        PoolObjectType type = (PoolObjectType) masterSpawner.MoveCount;
        var floorToReturn = ObjectPooler.instance.GetObject(type);
        return floorToReturn;
    }
    
    //Object pool returners
    private GameObject GetPickup()
    {
        var type = (PoolObjectType) Random.Range(8,11);
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
    
    private GameObject GetFloorEmpty()
    {
        PoolObjectType type = (PoolObjectType) Random.Range(0,6);
        var floorToReturn = ObjectPooler.instance.GetObject(type);
        return floorToReturn;
    }
    
    public void SpawnFloor()
    {
        
        //change this from a foreach loop
        foreach (var spawnPoint in spawnPoints)
        {
            GameObject floor;
            floor = SuperSpawn ? GetFloorSuperSpawn() : GetFloor();
            

            var targetLocation = spawnPoint.transform;
            SetFloorPosition(spawnPoint.transform, floor);
            floor.transform.SetParent(targetLocation);

            //pickup logic
            var random = Random.Range(0, 10);
            if (SpawnPickups)
            {
                
                if (random == 2)
                {
                    var type = (PoolObjectType) (Random.Range(9, gameIncrementer));
                    Debug.Log(type);
                    targetLocation = spawnPoint.transform.GetChild(0).transform;
                    var pickup = ObjectPooler.Instance.GetObject(type);
                    pickup.transform.parent = spawnPoint.transform.GetChild(0);
                    pickup.transform.position = targetLocation.transform.position;
                    pickup.transform.rotation = targetLocation.transform.rotation;
                }
            }
            

        } 
    }

    private static void SetFloorPosition(Transform target, GameObject toPosition)
    {
        var transform1 = target.transform;
        toPosition.transform.position = transform1.position;
        toPosition.transform.rotation = transform1.rotation;
    }

    private static void SetPickupPosition(Transform target, GameObject toPosition)
    {
        var transform1 = target.transform;
        toPosition.transform.position = transform1.position;
        toPosition.transform.rotation = transform1.rotation;
    }



    public void ReturnFloor()
    {

            foreach (var _ in spawnPoints.Select(spawnPoint => spawnPoint.transform.GetChild(1)))
            {
                var type = _.gameObject.GetComponent<Type>().PoolType;
                //ObjectPooler.instance.ReturnObject(_.gameObject, type);
                //fixbug with object pooler
                Destroy(_.gameObject);
            }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EndOfLine"))
        {
            masterSpawner.MoveFloor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EndOfLine"))
        {
            masterSpawner.MoveFloor();
        }
    }

    public void StopMoving()
    {
        moveSpeed = 0f;
    }

    public void StartMoving()
    {
        moveSpeed = 0.15f;
    }
    private void SpawnPickup(Transform spawnPoint)
    {
        
            var pickUp = GetPickup();
            SetPickupPosition(spawnPoint.transform,pickUp);
            pickUp.transform.SetParent(spawnPoint.transform);
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
