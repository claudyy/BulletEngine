using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEngineTest : MonoBehaviour {
    /*
    public static BulletEngine instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("BulletEngine");
                if (go != null){_instance = go.GetComponent<BulletEngine>();}
                else{ GameObject goTemp = new GameObject(); _instance = goTemp.AddComponent<BulletEngine>();}
            }
            return _instance;
        }
    }
    private static BulletEngine _instance;
    */

    public List<BaseWeaponObject> weaponObjList=new List<BaseWeaponObject>();
    public List<Weapon> weaponList=new List<Weapon>();
	// Use this for initialization
	void Start () {
        gameObject.name = "BulletEngine";
        Init();

    }
    void Update()
    {
        Tick();
    }
	void Init()
    {

    }
    void Tick()
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            weaponList[i].Tick();
        }
        for (int i = 0; i < weaponObjList.Count; i++)
        {
            weaponObjList[i].Tick();
        }
    }
    void Pause(bool pause)
    {

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
    List<BaseWeaponObject> wObjList=new List<BaseWeaponObject>();
    public BaseWeaponObject GetPoolWeaponObject(BaseWeaponObject wObj)
    {
        for (int i = 0; i < wObjList.Count; i++)
        {

            if (wObjList[i].gameObject.activeInHierarchy==false && wObjList[i].gameObject.name==wObj.gameObject.name)
            {
                return wObjList[i];
            }
        }
        BaseWeaponObject wObjTemp = Instantiate(wObj);
        wObjTemp.transform.parent = transform;
        wObjTemp.gameObject.name = wObjTemp.gameObject.name.Remove(wObjTemp.gameObject.name.Length - 7);
        wObjList.Add(wObjTemp);
        return wObjTemp;
    }
    public void FillPoolWeaponObject(BaseWeaponObject wObj, int count)
    {
        for (int i = 0; i < count; i++)
        {
            BaseWeaponObject wObjTemp = Instantiate(wObj);
            wObjTemp.transform.parent = transform;
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
                Destroy(temp.gameObject);
                count--;
            }
            if (count <= 0) return;
        }
    }
}
