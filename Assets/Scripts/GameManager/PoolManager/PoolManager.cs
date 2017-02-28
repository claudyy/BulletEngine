using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : BaseManager {

    Transform poolParent;
    public List<IPoolObject> objectPool = new List<IPoolObject>();
    public override void Init()
    {
        base.Init();
        GameObject go = new GameObject();
        go.name = "Pool";
        poolParent = go.transform;
        poolParent.parent = GameManager.instance.transform;

    }
    public GameObject SpawnObject(IPoolObject spawnObj, Vector3 pos)
    {
        objectPool.RemoveAll(item => item == null);
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].GetGameObject().name == spawnObj.GetGameObject().name)
            {
                if (objectPool[i].GetGameObject().activeInHierarchy == false)
                {

                    objectPool[i].GetGameObject().SetActive(true);
                    objectPool[i].GetGameObject().transform.position = pos;
                    objectPool[i].PoolObjectInit();
                    return objectPool[i].GetGameObject();
                }
            }
        }
        GameObject go = GameObject.Instantiate(spawnObj.GetGameObject());
        IPoolObject temp = go.GetComponent(typeof(IPoolObject)) as IPoolObject;
        temp.GetGameObject().name = temp.GetGameObject().name.Remove(temp.GetGameObject().name.Length - 7, 7);
        temp.GetGameObject().transform.parent = poolParent;
        temp.GetGameObject().transform.position = pos;
        temp.PoolObjectInit();
        objectPool.Add(temp);
        return temp.GetGameObject();
    }
    public void DeactivateAll()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            objectPool[i].GetGameObject().SetActive(false);
        }
    }
    public void BackToPool(IPoolObject poolObj)
    {
        poolObj.GetGameObject().transform.parent = poolParent;
    }
}
