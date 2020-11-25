using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public abstract class Pickup : MonoBehaviour
{
    protected PoolObjectType type;
    protected ObjectSpawner spawner;
    ScoreSystem scoreSystem;
    [SerializeField] protected AudioClip clip;
    protected Player player;
    protected float start;
    protected float target;
    protected float returnValue;

    protected virtual void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectSpawner>();
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
            player.PlayAudio(clip);
            ApplyEffect();
            ReturnToPool();
            ScoreFunction();
        }
    }

    protected void ScoreFunction()
    {
        scoreSystem.AddScore();
        scoreSystem.AddBuff();
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EndOfLine"))
        {
            Destroy(gameObject);
        }
    }
}
