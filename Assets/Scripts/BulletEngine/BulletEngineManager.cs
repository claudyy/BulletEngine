using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BulletEngineManager : BaseManager {

    public List<BaseWeaponObject> weaponObjList = new List<BaseWeaponObject>();
    public List<Weapon> weaponList = new List<Weapon>();

    public override void Tick()
    {
        base.Tick();
        for (int i = 0; i < weaponList.Count; i++)
        {
            weaponList[i].Tick();
        }
        for (int i = 0; i < weaponObjList.Count; i++)
        {
            weaponObjList[i].Tick();
        }
    }
    public void AddWeapon(Weapon w)
    {
        weaponList.Add(w);
    }
    public void AddWeaponObj(BaseWeaponObject wObj)
    {
        weaponObjList.Add(wObj);
    }
    public void RemoveWeaponObj(BaseWeaponObject wObj)
    {
        weaponObjList.Remove(wObj);
    }
    ///// Pooling
    List<BaseWeaponObject> wObjList = new List<BaseWeaponObject>();
    public BaseWeaponObject GetPoolWeaponObject(BaseWeaponObject wObj)
    {
        for (int i = 0; i < wObjList.Count; i++)
        {

            if (wObjList[i].gameObject.activeInHierarchy == false && wObjList[i].gameObject.name == wObj.gameObject.name)
            {
                return wObjList[i];
            }
        }
        BaseWeaponObject wObjTemp = GameObject.Instantiate(wObj);
        wObjTemp.transform.parent = GameManager.instance.transform;
        wObjTemp.gameObject.name = wObjTemp.gameObject.name.Remove(wObjTemp.gameObject.name.Length - 7);
        wObjList.Add(wObjTemp);
        return wObjTemp;
    }
    public void FillPoolWeaponObject(BaseWeaponObject wObj, int count)
    {
        for (int i = 0; i < count; i++)
        {
            BaseWeaponObject wObjTemp = GameObject.Instantiate(wObj);
            wObjTemp.transform.parent = GameManager.instance.transform;
            wObjTemp.gameObject.name = wObjTemp.gameObject.name.Remove(wObjTemp.gameObject.name.Length - 7);
            wObjTemp.HideObj();
            wObjList.Add(wObjTemp);
        }
    }
    public void RemovePoolWeaponObject(BaseWeaponObject wObj, int count)
    {

        for (int i = 0; i < wObjList.Count; i++)
        {
            if (wObjList[i].gameObject.activeInHierarchy == false && wObjList[i].gameObject.name == wObj.gameObject.name)
            {
                BaseWeaponObject temp = wObjList[i];
                wObjList.RemoveAt(i);
                GameObject.Destroy(temp.gameObject);
                count--;
            }
            if (count <= 0) return;
        }
    }
}
