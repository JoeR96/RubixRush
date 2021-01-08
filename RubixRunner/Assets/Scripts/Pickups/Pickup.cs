using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public abstract class Pickup : MonoBehaviour
{
    protected PoolObjectType type;
    protected GameManager spawner;
    protected ScoreSystem scoreSystem;
    [SerializeField] protected AudioClip clip;
    protected Player player;
    protected float start;
    protected float target;
    protected float returnValue;

    protected virtual void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        scoreSystem = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreSystem>();
        player = FindObjectOfType<Player>();
    }

    protected void OnEnable()
    {
        start = player.MoveForce;
    }

    protected virtual void ApplyEffect()
    {
        //override this with buff effect
    }

    protected void ReturnToPool()
    {

        ObjectPooler.Instance.ReturnObject(gameObject,type);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound("Pickup");
            
            ApplyEffect();
            StartCoroutine(LerpGameObjectPosition(gameObject, player.transform.position, 1f));
            ScoreFunction();
        }
        if (other.gameObject.CompareTag("EndOfLine"))
        {
            ReturnToPool();
        }
    }

    protected void ScoreFunction()
    {
        scoreSystem.AddScore();
        scoreSystem.AddBuff();
    }
    

 

    public IEnumerator LerpGameObjectPosition(GameObject toLerp, Vector3 targetTransform, float rate)
    {
        float time = 1f;
        float i = 0;
        float r = rate / time;
        var start = toLerp.transform.position;
        while (i < 1)
        { 
            i += Time.deltaTime * rate;
            toLerp.transform.position = Vector3.Lerp(start, targetTransform, i);
            //transform.rotation = Quaternion.RotateTowards(start, target, i);
            yield return null;
        }
        ReturnToPool();
        
    }
    
}
