using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //if time put gamemanager on scriptable object can also save menu stats 
    Queue<GameObject> floorSpawners = new Queue<GameObject>();
    private float offset = 18;
    private float x;
    private float tutorialCounter;

    public float MoveCount = 0; // Start is called before the first frame update
    
    private float timer;
    private float colourDuration;
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            SpawnHolder();    
        }
    }

    private void Awake()
    {
        MoveCount = 1;
    }

    
    private void Update()
    {
        if (timer > colourDuration)
        {
            timer -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.J))
        {
            StartCoroutine(LerpVariable(6f));
        }
    }
    
    public IEnumerator LerpVariable(float target)
    {
        var m_rotateTime = 1;
        float timer = 0;
        var start = MoveCount;
        while (timer < m_rotateTime)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / m_rotateTime, 1);
            MoveCount = Mathf.Lerp(start, target, percentage);
            yield return null;
            MoveCount = 1;
        }
    }
    private void SpawnHolder()
    {
        var holder = ObjectPooler.instance.GetObject(PoolObjectType.FLOORMASTER);
        holder.transform.parent = gameObject.transform;
        holder.transform.position = new Vector3(0f, 0f, x);
        x += offset;
        floorSpawners.Enqueue(holder);
    }
    //OOP THIS LATER FINISH GAME FIRST TODAY YOU RETARD
    public void MoveFloor()
    {
        
        var floorToMove = floorSpawners.Dequeue();
        //Use LINQ to access Last element in the queue 
        var positionToMove = floorSpawners.Last().transform.position;
        floorToMove.transform.position = new Vector3(positionToMove.x ,positionToMove.y,positionToMove.z + 18f);
        floorSpawners.Enqueue(floorToMove);
        //get distance from player to trigger
        //get Transform set transform.position at the back of the queue - could add to queue in reverse
        //set floorToMove as set - offset
        //move object to back of the queue
        var spawner = floorToMove.GetComponent<Spawner>();
        spawner.ReturnFloor();
        spawner.SpawnFloor();
    }

    public void StopMoving()
    {
        foreach (var spawner in floorSpawners)
        {
            var _ = spawner.GetComponent<Spawner>();
            _.StopMoving();
        }
    }

    public void StartMoving()
    {
        foreach (var spawner in floorSpawners)
        {
            var _ = spawner.GetComponent<Spawner>();
            _.StartMoving();
        }
    }

    public void SpawnerSpeedUp()
    {
        foreach (var spawner in floorSpawners)
        {
            var _ = spawner.GetComponent<Spawner>();
            _.SpeedUpMovement();
        }
    }

    public void SpawnerSpeedDown()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
    {
        foreach (var spawner in floorSpawners)
        {
            var _ = spawner.GetComponent<Spawner>();
            _.SlowDownMovement();
        }
    }

    private void SuperSpeedMode()
    {
        foreach (var spawner in floorSpawners)
        {
            var _ = spawner.GetComponent<Spawner>();
        }
    }

    private void IntroduceEmptyFloor()
    {
        
    }
    private void IntroduceCoins()
    {
        
    }
    private void IntroducePickups()
    {
        
    }
    private void IntroduceObstacles()
    {
        
    }

    
}
