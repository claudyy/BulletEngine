using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEngine : MonoBehaviour {
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
    public BaseWeaponObject GetPoolWeaponObject(BaseWeaponObject wOpj)
    {
        return Instantiate(wOpj);
    }
}
