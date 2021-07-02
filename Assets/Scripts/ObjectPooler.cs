using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> _pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    #region singleTon

    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion


    private void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            _poolDictionary.Add(pool.tag, objectPool);
        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + " Doesn't exist");
            return null;
        }

        GameObject objectToSpawn = _poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(false);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        IpooledObject pooledObject = objectToSpawn.GetComponent<IpooledObject>();
        objectToSpawn.SetActive(true);
        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        _poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    
    public GameObject[] SpawnMultipleFromPool(int count , string tag, Vector3[] position, Quaternion[] rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + " Doesn't exist");
            return null;
        }

        GameObject[] _objects = new GameObject[count];
        IpooledObject[] _pooledObjects = new IpooledObject[count];
        
        for (int i = 0; i < count; i++)
        {
            _objects[i] = _poolDictionary[tag].Dequeue();
            _objects[i].SetActive(false);
            _objects[i].transform.position = position[i];
            _objects[i].transform.rotation = rotation[i];
            _pooledObjects[i] =  _objects[i].GetComponent<IpooledObject>();
        }
        
        for (int i = 0; i < count; i++)
        {
            _objects[i].SetActive(true);
            if (_pooledObjects[i] != null)
            {
                _pooledObjects[i].OnObjectSpawn();
            }
            _poolDictionary[tag].Enqueue(_objects[i]);
        }
        
        return _objects;
    }

}

