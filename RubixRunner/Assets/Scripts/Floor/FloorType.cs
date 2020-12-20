using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorType : MonoBehaviour
{
    public PoolObjectType PoolType;
    public bool state;
    private void OnEnable()
    {
        state = true;
    }
    public void ReturnToPool()
    {
        state = false;
        ObjectPooler.instance.ReturnObject(gameObject, PoolType);
    }
}


