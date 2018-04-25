using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<GameObject>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab) as GameObject;
                newObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(newObject);
            }
        }
    }

    public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // TODO
    }
}
