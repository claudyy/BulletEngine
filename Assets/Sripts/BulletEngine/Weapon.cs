using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    protected Transform ShootOrigin;

    public BaseWeaponObject spawnObj;
    [Header("Cooldown")]
    public float shootCooldown;
    float curShootCooldown;
    public float repeatCooldown;
    float curRepeatCooldown;
    float curShootDuration;
	// Use this for initialization
	void Start () {
        BulletEngine.instance.AddWeapon(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RotateWeapon(Vector2 dir)
    {
        transform.up = dir;
    }
    public void Init()
    {

    }
    public void Tick()
    {
        TickCooldowns();
    }
    void TickCooldowns()
    {
        curRepeatCooldown -= Time.deltaTime;
        curShootCooldown -= Time.deltaTime;

    }
    public void ShootStart()
    {
        if(curRepeatCooldown<=0) Shoot();
        curRepeatCooldown = repeatCooldown;
        Debug.Log("Shoot Start");
    }
    public void TickShoot()
    {
        if (curShootDuration == 0) ShootStart();
        curShootDuration += Time.deltaTime;


        if(curShootCooldown<=0)
        {
            Shoot();
        }

    }
    public void ShootEnd()
    {
        curShootDuration = 0;
    }
    void Shoot()
    {
        curShootCooldown = shootCooldown;
        BaseWeaponObject temp = Spawn();
        temp.transform.up = transform.up;
    }
    public BaseWeaponObject Spawn()
    {
        Debug.Log("SpawnObj "+ spawnObj.name);
        return BulletEngine.instance.GetPoolWeaponObject(spawnObj);
    }
    void Pause(bool pause)
    {

    }
    void OnValidate()
    {
        if(ShootOrigin==null)
        {
            GameObject go = new GameObject();
            go.name = "ShootOrigin";
            ShootOrigin = go.transform;
            ShootOrigin.parent = transform;
            ShootOrigin.position = transform.position;
            ShootOrigin.rotation = transform.rotation;
        }
    }
}
