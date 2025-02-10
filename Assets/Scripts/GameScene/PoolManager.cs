using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolManager : MonoBehaviour
{
    private static readonly List<Pool> ObjectPools = new List<Pool>();

    private static GameObject _objectPoolEmptyHolder;

    private void Awake()
    {
        _objectPoolEmptyHolder = new GameObject("Orbs");
    }

    public static GameObject SpawnObject(GameObject prefabToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        Pool pool = ObjectPools.Find(p => p.PoolName == prefabToSpawn.name);

        // si el pool no existeix en cream un amb el nom del gameObject que volem obtenir
        if (pool == null)
        {
            pool = new Pool() { PoolName = prefabToSpawn.name };
            ObjectPools.Add(pool);
        }

        // Comprovar si hi ha qualque gameObject inactiu per utilitzar
        GameObject spawnableOject = pool.InactiveObjects.FirstOrDefault();

        if (spawnableOject == null)
        {
            spawnableOject = Instantiate(prefabToSpawn, spawnPosition, spawnRotation);

            spawnableOject.transform.SetParent(_objectPoolEmptyHolder.transform);
        }
        else
        {
            spawnableOject.transform.position = spawnPosition;
            spawnableOject.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableOject);
            spawnableOject.SetActive(true);
        }

        return spawnableOject;
    }

    public static GameObject SpawnObject(GameObject prefabToSpawn, Transform parentTransform)
    {
        Pool pool = ObjectPools.Find(p => p.PoolName == prefabToSpawn.name);

        // si el pool no existeix en cream un amb el nom del gameObject que volem obtenir
        if (pool == null)
        {
            pool = new Pool() { PoolName = prefabToSpawn.name };
            ObjectPools.Add(pool);
        }

        // Comprovar si hi ha qualque gameObject inactiu per utilitzar
        GameObject spawnableOject = pool.InactiveObjects.FirstOrDefault();

        if (spawnableOject == null)
        {
            spawnableOject = Instantiate(prefabToSpawn, parentTransform);
        }
        else
        {
            pool.InactiveObjects.Remove(spawnableOject);
            spawnableOject.SetActive(true);
        }

        return spawnableOject;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string trueObjectName = obj.name[..^"(Clone)".Length];
        Pool pool = ObjectPools.Find(p => p.PoolName == trueObjectName);

        if (pool != null)
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
}

public class Pool
{
    public string PoolName;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}