using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type : MonoBehaviour
{
    public PoolObjectType PoolType;

    private void OnEnable()
    {
        
    }
    public void ReturnToPool()
    {
        ObjectPooler.instance.ReturnObject(gameObject, PoolType);
    }
}


