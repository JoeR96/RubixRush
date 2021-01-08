using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private GameObject distanceButton;
    [SerializeField] private DataScriptableObject _data;
    [SerializeField] private GameObject[] _spawner;
    [SerializeField]private UiManager _uiManager;
    private DynamicUI _dynamicUi;
    
    private List<GameObject> _floorSpawners = new List<GameObject>();
    private bool _startLoop;
    private float _offset = 18f;
    private float x;
    private int state;
    public float DistanceTravelled;
    private bool distancePopupActive;

    float timer = 2f;
    
    private void Awake()
    {
        _dynamicUi = GetComponent<DynamicUI>();
        
    }

    private void Start()
    {
        
        if (_data.tutorialMode)
        {
            SetDynamicState();
        }
        _startLoop = true;
        SpawnFloor(12);
        _startLoop = false;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            SetDefaultState();
        }
        if (Input.GetKey(KeyCode.W))
        {
            SetDynamicState();
        }
        if (Input.GetKey(KeyCode.E))
        {
            SetSuperSpawnState();
        }

        if (distancePopupActive)
        {
            DistanceTimer();
        }
    }
    
    private void DistanceTimer()
    {
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if ((timer <= 0))
        {
            distancePopupActive = false;
            DisableDistanceText();
            timer = 2f;
        }
        
            
    }
    
    #region FloorSetters
    private void SpawnFloor(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnFloorHolder();    
        }
    }

    private bool IsDivisible(int x, int y)
    {
        if (x % y == 0)
        {
            return true;
        }
        return false;
    }

    private void ReturnEndVariables()
    {
        
    }
    
    public void SpawnFloorHolder()
    {
        
        var holder = Instantiate(_spawner[state]);
        
        DistanceTravelled++;
        int t = Convert.ToInt32(DistanceTravelled);
        if (IsDivisible(t, 25))
        {
            Debug.Log("should show text");
            ShowDistanceText(t);
        }
            
        //Check for if the floor holder is dividable by x
        
        
        holder.transform.parent = gameObject.transform;
        if (_startLoop)
        {
            holder.transform.position = new Vector3(0f, 0f, x);
            x += _offset;
        }
        else
        {
            holder.transform.position = GetFloorPosition();
            holder.transform.rotation = GetFloorRotation();
        }
        
        _floorSpawners.Add(holder);
    }
    
    public Vector3 GetFloorPosition()
    {
        
        //Use LINQ to access Last element in the queue 
        var positionToMove = _floorSpawners.Last().transform.position;
        var target = new Vector3(positionToMove.x ,positionToMove.y,positionToMove.z + _offset);
        return target;

    }

    public Quaternion GetFloorRotation()
    {
        var rotationToMove = _floorSpawners.Last().transform.rotation;
        return rotationToMove;
    }
    //We can easily transition between various game states by setting the index variable
    //This will allow us to add varying functionality in the future if required



    public void SetDefaultState()
    {
        state = 0;
    }
    //Activate the dynamic tutorial
    public void SetDynamicState()
    {
        state = 1;
    }
    //Activate "lightspeed" and coin mode - this comment needs refining 
    public void SetSuperSpawnState()
    {
        state = 2;
    }

    public void SetGameOverState()
    {
        //ReturnDistanceTravelled(_distanceTravelled);
        foreach (var spawner in _floorSpawners)
        {
            spawner.GetComponent<BaseSpawner>().Stop();
        }
        _data.AddDistance(DistanceTravelled);
        _uiManager.SetGameOverText();
    }

    private void ReturnDistanceTravelled(float distanceTravelled)
    {
        _data.AddDistance(distanceTravelled);    
    }
    
    public void SpeedUp()
    {
        foreach (var spawner in _spawner)
        {
            spawner.GetComponent<BaseSpawner>().MoveSpeed = 0.075f;
        }
        foreach (var spawner in _floorSpawners)
        {
            spawner.GetComponent<BaseSpawner>().SpeedUp();
            
        }
    }
    #endregion
    
    private void ShowDistanceText(float distanceTravelled)
    {
          distancePopupActive = true;
          distanceText.SetText(distanceTravelled.ToString());
          distanceButton.gameObject.SetActive(true);
    }

    private void DisableDistanceText()
    {
        distanceButton.gameObject.SetActive(false);
    }

}
