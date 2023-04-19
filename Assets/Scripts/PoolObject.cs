using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private static PoolObject i;
    [SerializeField] private GameObject prefab;

    [SerializeField] private List<GameObject> objectPool = new List<GameObject>();

    [SerializeField] private int initialPoolSize = 10;

    public static PoolObject Instanse { get => i; set => i = value; }

    private PoolObject() { }
    private void Awake()
    {
        if (Instanse == null) { Instanse = this; }
    }
    
    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            obj.transform.parent = null;
            objectPool.Add(obj);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject obj in objectPool)
        {
            if (obj!=null && !obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = Instantiate(prefab, transform);
        newObj.SetActive(true);
        objectPool.Add(newObj);
        return newObj;
    }
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
