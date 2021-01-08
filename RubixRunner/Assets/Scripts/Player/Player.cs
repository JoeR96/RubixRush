using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    ScoreSystem scoreSystem;
    UiManager uiManager;
    private Dictionary<string, GameObject> playerSkins = new Dictionary<string, GameObject>();
    private Manager masterSpawner;
    private RotationManager rotationManager;
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject uiOutput;
    [SerializeField] private GameObject _skinHolder;
    [SerializeField] private DataScriptableObject _data;
    public float MoveForce;
    
    [SerializeField] private float _moveForce;
    
    private bool _isLeft;
    private Vector3 _inputVector;
    [SerializeField] private Rigidbody _playerRb;
    public AudioSource AudioSource;
    private bool _isRunning = false;

    

    [SerializeField] private Animator _playerAnimator;
    
    //pass these game objects through to the UI manager
 
    
    //remove inside function
    private bool _isUp;
    private void Awake()
    {
        rotationManager = GetComponent<RotationManager>();
        masterSpawner = GameObject.FindWithTag("GameManager").GetComponent<Manager>();
        uiManager = uiOutput.GetComponent<UiManager>();
        scoreSystem = GetComponent<ScoreSystem>();
        AudioSource = GetComponent<AudioSource>();
        
        _moveForce = 5f;
    }


    private void Start()
    {
        FillDictionary();
    }

    private void FillDictionary()
    {
        foreach (Transform child in _skinHolder.transform)
        {
            playerSkins.Add(child.name,child.gameObject);
            if (child.name == _data._activeSkin.name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private float _distanceTravelled;

    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_isRunning == false)
            {
                StartCoroutine(RotatePlayerAndCamera());
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {

                FloorDeathFunction();
            
        }
        
        
    }

    private IEnumerator RotatePlayerAndCamera()
    {
        _isRunning = true;
        float time = 180f;
        float i = 0;
        float rate = 1 / time;
        var m_rotateTime = 0.325f;
        var start = transform.rotation;
        var playerStart = transform.position;
        
        //so dirty this needs refactoring
        if (_isUp == false)
        {
            float timer = 0;
            var targetPos = new Vector3(transform.position.x, transform.position.y + 8.8f, transform.position.z);
            while (timer < m_rotateTime)
            {
                timer += Time.deltaTime;
                float percentage = Mathf.Min(timer / m_rotateTime, 1);

                Quaternion playerTarget = start * Quaternion.Euler(0f, 0f, 180f);
                Quaternion cameraTarget = start * Quaternion.Euler(0f, 0f, 180f);

                //camera.transform.rotation = Quaternion.Slerp(start, cameraTarget, percentage);
                transform.rotation = Quaternion.Slerp(start, playerTarget, percentage);
                transform.position = Vector3.Lerp(playerStart, targetPos, percentage);
                _isUp = true;
                _isRunning = false;
                yield return null;
            }
        }

        else if (_isUp)
        {
            float timer = 0;
            var targetPos = new Vector3(transform.position.x, transform.position.y - 8.8f, transform.position.z);
            while (timer < m_rotateTime)
            {
                timer += Time.deltaTime;
                float percentage = Mathf.Min(timer / m_rotateTime , 1f);
  
                Quaternion playerTarget = start * Quaternion.Euler(0f, 0f, 180f);
                Quaternion cameraTarget = start * Quaternion.Euler(0f, 0f, -180f);

                //camera.transform.rotation = Quaternion.Slerp(start, cameraTarget, percentage);
                transform.rotation = Quaternion.Slerp(start, playerTarget, percentage);
                transform.position = Vector3.Lerp(playerStart, targetPos, percentage);
                _isUp = false;
                _isRunning = false;
                yield return null;
            }
        }
    }
    
    private void Move()
    {
        _inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Mathf.Clamp(_inputVector.x, -2.5f, 2.5f);
        Mathf.Clamp(_inputVector.y, -90f, 9f);
        _playerRb.velocity = _inputVector * _moveForce;
        
        if (_inputVector.x <= 0 || _inputVector.x >= 0 )
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _inputVector, out hit, 15))
            {
                
                var toRot = hit.collider.GetComponentInParent<RotationManager>();
                var dist = Vector3.Distance(transform.position, hit.point);
                if(hit.collider.CompareTag("Wall"))
                {
                    if (dist < 1.25f && _inputVector.x >= 1)
                    {
                         {
                             _isLeft = false;
                            toRot.GetRotationRight();
                            StartCoroutine("LerpPosition");
                        }
                    }
                    if (dist < 1.25f && _inputVector.x <= -1)
                    {
                        {
                            _isLeft = true;
                            toRot.GetRotationLeft();
                            StartCoroutine("LerpPosition");
                        }

                    }
                }             
            }
        }

    }
    
    public IEnumerator LerpPosition()
    {
        float timer = 0f;
        float rate = 0.75f;
   
        var start = transform.position;
        while (timer < rate)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / rate, 1);
            
            //right lerp
            if (!_isLeft)
            {
                transform.position = Vector3.Lerp(start, new Vector3(start.x - 5.5f, start.y, start.z), percentage);
            }
            //left lerp
            if (_isLeft)
            {
                transform.position = Vector3.Lerp(start, new Vector3(start.x + 5.5f, start.y, start.z), percentage);
            }

            yield return null;
        }
    }
    public IEnumerator LerpGameObjectPosition(GameObject toLerp, Vector3 target,float rate)
        {
            float timer = 1f;

            var start = toLerp.transform.position;
            while (timer < 1)
            {
                timer += Time.deltaTime;
                float percentage = Mathf.Min(timer / rate, 1);
                
                toLerp.transform.position = Vector3.Lerp(start, target, percentage);
                //transform.rotation = Quaternion.RotateTowards(start, target, i);
                yield return null;
            }
    }

    public void FloorDeathFunction()
    {
        deathCanvas.SetActive(true);
        masterSpawner.SetGameOverState();
        DeathAnimation();
        DeathAudioFunction();
    }
    
    private void DeathAnimation()
    {
        StartCoroutine(DeathLerp());
    }

    private IEnumerator DeathLerp()
    {
        //target player rotation
        Quaternion target = new Quaternion(0f, 180f, 0f, 0f);
        StartCoroutine(rotationManager.RotateGameObject(target, transform, 0.25f));
        //move character to centre of hole
        StartCoroutine(LerpGameObjectPosition(gameObject, new Vector3(transform.position.x, transform.position.y ,
                transform.position.z + 2f),2f));
        _playerAnimator.SetBool("IsDead",true);
        //wait been lerps
       yield return new WaitForSeconds(1f);
       //suck character out of hole
       StartCoroutine(LerpGameObjectPosition(gameObject, new Vector3(transform.position.x, transform.position.y - 40f ,
               transform.position.z ),1f));
       yield return new WaitForSeconds(1f);
       AdManager.Instance.PlayAd();
    }

    private void DeathAudioFunction()
    {
        AudioManager.Instance.GameMusicSource.gameObject.SetActive(false);
        AudioManager.Instance.PlaySound("GameOver");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SuperState"))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SuperState"))
        {
            
        }
    }
}
