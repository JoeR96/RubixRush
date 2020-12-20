using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    ScoreSystem scoreSystem;
    UiManager uiManager;
    public float MoveForce { get; set; }

    
    private Animator animator;
    [SerializeField] private AnimationCurve animationCurve;
    
    private GameManager masterSpawner;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerParent;
    [SerializeField] private Camera camera;
    [SerializeField] private float moveForce;
    
    private bool isLeft;
    Vector3 inputVector;
    [SerializeField] private Rigidbody playerRB;
    public AudioSource AudioSource;
    private bool isRunning = false;
    private Camera mainCamera;
    private RotationManager rotationManager;
    
   
    
    //pass these game objects through to the UI manager
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject uiOutput;
    
    //remove inside function
    private bool isUp;
    private void Awake()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        rotationManager = GetComponent<RotationManager>();
        masterSpawner = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        uiManager = uiOutput.GetComponent<UiManager>();
        scoreSystem = GetComponent<ScoreSystem>();
        AudioSource = GetComponent<AudioSource>();
        
        moveForce = 5f;
    }
    // Update is called once per frame

    private float distanceTravelled;
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isRunning == false)
            {
                StartCoroutine(RotatePlayerAndCamera());
            }
        }
    }

    private IEnumerator RotatePlayerAndCamera()
    {
        isRunning = true;
        float time = 180f;
        float i = 0;
        float rate = 1 / time;
        var m_rotateTime = 0.325f;
        var start = transform.rotation;
        var playerStart = transform.position;
        if (isUp == false)
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
                playerParent.transform.rotation = Quaternion.Slerp(start, playerTarget, percentage);
                player.transform.position = Vector3.Lerp(playerStart, targetPos, percentage);
                animationCurve.Evaluate(2);
                isUp = true;
                isRunning = false;
                yield return null;
            }
        }

        else if (isUp)
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
                playerParent.transform.rotation = Quaternion.Slerp(start, playerTarget, percentage);
                player.transform.position = Vector3.Lerp(playerStart, targetPos, percentage);
                isUp = false;
                isRunning = false;
                yield return null;
            }
        }
    }
    
    private void Move()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        playerRB.velocity = inputVector * moveForce;
        
        if (inputVector.x <= 0 || inputVector.x >= 0 )
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, inputVector, out hit, 15))
            {
                var toRot = hit.collider.GetComponentInParent<RotationManager>();
                var dist = Vector3.Distance(transform.position, hit.point);
                if(hit.collider.CompareTag("Wall"))
                {
                    if (dist < 1.25f && inputVector.x >= 1)
                    {
                         {
                            isLeft = false;
                            toRot.GetRotationRight();
                            StartCoroutine("LerpPosition");
                        }
                    }
                    if (dist < 1.25f && inputVector.x <= -1)
                    {
                        {
                            isLeft = true;
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
        float time = 1f;
        float i = 0;
        float rate = 5f / time;
        var start = transform.position;
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            //right lerp
            if (!isLeft)
            {
                transform.position = Vector3.Lerp(start, new Vector3(start.x - 3f, start.y, start.z), i);
            }

            //left lerp
            if (isLeft)
            {
                transform.position = Vector3.Lerp(start, new Vector3(start.x + 3f, start.y, start.z), i);
            }

            //transform.rotation = Quaternion.RotateTowards(start, target, i);
            yield return null;
        }
    }

    public IEnumerator LerpGameObjectPosition(GameObject toLerp, Vector3 target,float rate)
        {
            float time = 1f;
            float i = 0;
            float r = rate / time;
            var start = toLerp.transform.position;
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                toLerp.transform.position = Vector3.Lerp(start, target, i);
                //transform.rotation = Quaternion.RotateTowards(start, target, i);
                yield return null;
            }
    }

    public void PlayAudio(AudioClip toPlay)
    {
        AudioSource.PlayOneShot(toPlay);
    }

    
    public void FloorDeathFunction()
    {
        deathCanvas.SetActive(true);
        //Set game manager state to over
        Debug.Log(masterSpawner);
        //masterSpawner.SetGameOverState();
        //switch to our game over animation
        animator.SetTrigger(3);
        //camera sequence to show game over state 
        rotationManager.RotateGameObject(new Quaternion(0f, 0f, 180f, 0f),  mainCamera.transform, 2f);
        Quaternion target = new Quaternion(0f, 180f, 0f, 0f);
        StartCoroutine(rotationManager.RotateGameObject(target, camera.transform, 2f));
        StartCoroutine(LerpGameObjectPosition(Camera.main.gameObject,new Vector3(camera.transform.position.x,camera.transform.position.y ,-5f), 0.25f));
        StartCoroutine(LerpGameObjectPosition(gameObject,
        new Vector3(transform.position.x, transform.position.y, transform.position.z - 25f), 0.8f));
    }

    private void OnTriggerEnter(Collider other)
    { 
    }

    private void Death()
    {
        
    }
}
