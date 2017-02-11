using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponObject : MonoBehaviour {

    public float lifeTime;
    [HideInInspector]
    public float curLifeTime;
    void Start()
    {
        BulletEngine.instance.AddWeaponObj(this);
        Init();
    }
    public virtual void Init()
    {
        curLifeTime = lifeTime;
    }
    public virtual void Tick()
    {
        curLifeTime -= Time.smoothDeltaTime;
        if(curLifeTime<=0)
        {
            DestroyWeaponObj();
        }
    }
    public virtual void Pause(bool pause)
    {

    }
    public virtual void DestroyWeaponObj()
    {
        BulletEngine.instance.RemoveWeaponObj(this);
        Destroy(gameObject);
    }
    public virtual void OnTriggerEnter2D()
    {

    }
}
