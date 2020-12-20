using System;
using System.Collections.Generic;
using UnityEngine;


public enum PoolObjectType
{
    BLUEFLOOR = 1,
    WHITEFLOOR = 2,
    REDFLOOR = 3,
    GREENFLOOR = 4,
    YELLOWFLOOR = 5,
    ORANGEFLOOR = 6,
    EMPTYFLAT = 0,
    PICKUPSCORE = 7,
    PICKUPSLOWPLAYER = 8,
    PICKUPFASTPLAYER = 9,
    OBSTACLE = 10,
}
[Serializable]
public class PoolType
{
    public PoolObjectType Type;
    public int AmountToPool;
    public GameObject PrefabToPool;
    public GameObject PoolHolder;
    public List<GameObject> ObjectPool = new List<GameObject>();
}
public class ObjectPooler : Singleton<ObjectPooler>
{
    [Header("Pool Properties")]
    public static ObjectPooler Instance;
    //[Header("Pool Holders")]
    public List<PoolType> MasterPool;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < MasterPool.Count ; i++)
        {
            CreatePool(MasterPool[i]);
        }
    }


    private static void CreatePool(PoolType type)
    {
        for (int i = 0; i < type.AmountToPool; i++)
        {
            var pooledObject = Instantiate(type.PrefabToPool, type.PoolHolder.transform);
            pooledObject.SetActive(false);
            type.ObjectPool.Add(pooledObject);
        }
    }
    private PoolType GetPoolType(PoolObjectType type)
    {
        for (int i = 0; i < MasterPool.Count; i++)
        {
            if (type == MasterPool[i].Type)
            {
                return MasterPool[i];
            }
        }
        return null;
    }
    public GameObject GetObject(PoolObjectType type)
    {
        PoolType currentPool = GetPoolType(type);
        List<GameObject> pool = currentPool.ObjectPool;

        
        if(pool.Count > 0)
        {
            var returnObject = pool[pool.Count - 1];
            returnObject.SetActive(true);
            pool.Remove(returnObject);
            return returnObject;

        }
        else
        {
            var returnObject = Instantiate(currentPool.PrefabToPool);
            return returnObject;
        }

    }
    
    public void ReturnObject(GameObject obj,PoolObjectType type)
    {
        PoolType poolList = GetPoolType(type);
        List<GameObject> pool = poolList.ObjectPool;

        if (pool.Contains(obj) == false) 
        {
            obj.SetActive(false);
            obj.transform.parent = poolList.PoolHolder.transform;
            pool.Add(obj);
        }
    }
}
