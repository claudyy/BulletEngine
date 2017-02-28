using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponObject : MonoBehaviour {

    public Damage damage;
    public float lifeTime;
    [HideInInspector]
    public float curLifeTime;

    protected List<IDamagable> alreadyDamaged = new List<IDamagable>();
    protected float damageAgainDelay=0.2f;
    protected float curDamageAgainDelay;
    void Start()
    {
        
    }
    //is called in weapon
    public virtual void Init()
    {
        GameManager.instance.bulletEngine.AddWeaponObj(this);
        curLifeTime = lifeTime;
        gameObject.SetActive(true);
    }
    public virtual void Tick()
    {
        curLifeTime -= Time.smoothDeltaTime;
        if(curLifeTime<=0)
        {
            DestroyWeaponObj();
        }
        curDamageAgainDelay -= Time.deltaTime;
        if(curDamageAgainDelay<0)
        {
            curDamageAgainDelay = damageAgainDelay;
            alreadyDamaged.Clear();
        }
    }
    public virtual void Pause(bool pause)
    {

    }
    public virtual void DestroyWeaponObj()
    {
        GameManager.instance.bulletEngine.RemoveWeaponObj(this);
        HideObj();
    }
    public virtual void HideObj()
    {
        gameObject.SetActive(false);
        transform.parent = GameManager.instance.transform;
    }
    public virtual void OnTriggerEnter2D()
    {

    }
}
