using System;
using System.Collections.Generic;
using UnityEngine;


public enum PoolObjectType
{
    FLOORGREY,
    BLUEFLOOR,
    WHITEFLOOR,
    REDFLOOR,
    GREENFLOOR,
    YELLOWFLOOR,
    ORANGEFLOOR,
    EMPTYFLAT,
    FLOORMASTER,
    PICKUPSLOWPLAYER,
    PICKUPFASTPLAYER,
    PICKUPSCORE,
    OBSTACLE
}
[Serializable]
public class PoolType
{
    public PoolObjectType type;
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
    #region Object Pooler

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
            if (type == MasterPool[i].type)
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
        GameObject returnObject = null;
        
        if(pool.Count > 0)
        {
            returnObject = pool[pool.Count - 1];
            returnObject.SetActive(true);
        }
        else
        {
            returnObject = Instantiate(currentPool.PrefabToPool);
        }
        return returnObject;
    }
    public void ReturnObject(GameObject obj,PoolObjectType type)
    {
        obj.SetActive(false);
        PoolType poolList = GetPoolType(type);
        List<GameObject> pool = poolList.ObjectPool;
        obj.transform.parent = poolList.PoolHolder.transform;
        pool.Add(obj);
    }
    #endregion
}
