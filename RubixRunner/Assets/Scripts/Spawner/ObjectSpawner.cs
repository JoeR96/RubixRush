using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private DataScriptableObject _data;
    [SerializeField] private GameObject spawner;
    
    Queue<GameObject> floorSpawners = new Queue<GameObject>();
    private float offset = 18;
    private float x;
    private float _tutorialCounter;
    private bool startLoop;
    public float MoveCount = 0; // Start is called before the first frame update
    
    private float timer;

    
    
    //how many in game tiles we have ran along
    private float distanceTravelled;
    
    //track how long the distance popup has been active so we can deactivate it 
    private float distanceTimer;
    private float distanceDuration = 2.5f;
    private float colourDuration;

    


    private void Start()
    {
        //distancePopupActive = false;
        startLoop = true;
        for (int i = 0; i < 8; i++)
        {
            SpawnHolder();    
        }

        startLoop = false;
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

        
        // if (distancePopupActive && distanceTimer < distanceDuration)
        // {
        //     distanceTimer += Time.deltaTime;
        //         if (Math.Abs(distanceTimer - distanceDuration) < 0.1)
        //         {
        //             //move this out of the update function
        //             //distanceButton.gameObject.SetActive(false);
        //             distanceTimer = 0f;
        //             //distancePopupActive = false;
        //         }
        // }
        
    }

    //run this method every X distance and update the variables to coincide 
    private void ShowDistanceText(float distanceTravelled)
    {
        //distancePopupActive = true;
        //var t = distanceButton.gameObject.GetComponent<Image>();
        //d/istanceText.SetText(distanceTravelled.ToString());
        //distanceButton.gameObject.SetActive(true);
    }
    
    public bool IsDivisible(float x)
    {
        //will return true if x is divisible by 10 - increase later after testing 
        return (x % 10) == 0;
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
    public void SpawnHolder()
    {
        distanceTravelled += 1;
        if (IsDivisible(distanceTravelled))
        {
            ShowDistanceText(distanceTravelled);
        } 
        var holder = Instantiate(spawner);
        holder.transform.parent = gameObject.transform;
        if (startLoop)
        {
            holder.transform.position = new Vector3(0f, 0f, x);
            x += offset;
        }
        else
        {
            holder.transform.position = GetFloorPosition();
            holder.transform.rotation = GetFloorRotation();
        }
        
        

        floorSpawners.Enqueue(holder);
    }
    public Vector3 GetFloorPosition()
    {
        
        //Use LINQ to access Last element in the queue 
        var positionToMove = floorSpawners.Last().transform.position;
        var target = new Vector3(positionToMove.x ,positionToMove.y,positionToMove.z + 18f);
        return target;

    }

    public Quaternion GetFloorRotation()
    {
        var rotationToMove = floorSpawners.Last().transform.rotation;
        return rotationToMove;
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
    

    
}
