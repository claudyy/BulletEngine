﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public enum WeaponType {Single,Circle,Form }
public class Weapon : MonoBehaviour {
    public Transform ShootOrigin;
    public WeaponType type;
    public BaseWeaponObject spawnObj;
    public float shootCooldown;
    float curShootCooldown;
    public float repeatCooldown;
    float curRepeatCooldown;
    float curShootDuration;
    public int patternPerShoot=2;
    int currentPatternIndex;
    public bool parentShootOrigin;
    //per Object
    public float randomAnglePreObj;
    //Group
    public float randomAngleGroup;

    //circle
    public int circlePatternCount;
    public float maxAngle;
    public bool circleRight = true;
    //From
    public int formPositionCount;
    public List<Vector2> formPositionList;
    public bool formCircleDir;


    //amo
    public int clipSize=12;
    public int ammoCount=20;
    public int ammoPerShoot=1;
    public int currentAmmo;
    public float reloadTime=1;
    
    float currentReloadTime;
    public bool inReload {get { return currentReloadTime > 0; } }
	// Use this for initialization
	void Start () {
        GameManager.instance.bulletEngine.AddWeapon(this);
        SetupOrigin();
        Init();
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
        GameManager.instance.bulletEngine.FillPoolWeaponObject(spawnObj,10);
        currentAmmo = Mathf.Min( clipSize,ammoCount);
    }
    public void Tick()
    {
        TickCooldowns();
    }
    void TickCooldowns()
    {
        curRepeatCooldown -= Time.smoothDeltaTime;
        curShootCooldown -= Time.smoothDeltaTime;
        if(currentReloadTime>0)
        {
            currentReloadTime -= Time.deltaTime;
            if (currentReloadTime <= 0) EndReload();
        }
    }
    public void ShootStart()
    {
        if (inReload) return;
        if (currentAmmo < ammoPerShoot) { StartReload(); return; }
        if (curRepeatCooldown<=0) Shoot();
        curRepeatCooldown = repeatCooldown;
        Debug.Log("Shoot Start");
    }
    public void TickShoot()
    {
        
        if (curShootDuration == 0) ShootStart();
        if (inReload) return;// if in ShootStart called StartReload return
        if (currentAmmo < ammoPerShoot&&inReload==false) { StartReload(); return; }//if already in shooting and amo is empty Start Reload
        curShootDuration += Time.smoothDeltaTime;


        if (curShootCooldown<=0)
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
        
        currentAmmo -= ammoPerShoot;
        switch (type)
        {
            case WeaponType.Single:
                SingleShoot();
                break;
            case WeaponType.Circle:
                CircleShoot();
                break;
            case WeaponType.Form:
                FormShoot();
                break;
            default:
                break;
        }
    }

    public void SingleShoot()
    {
        BaseWeaponObject temp = Spawn(transform.up,ShootOrigin.position);
    }
    public void CircleShoot()
    {
        float rot = maxAngle / (circlePatternCount);
        float offset = maxAngle / 2;
        if (circleRight == false) { rot *= -1; offset *= -1; }
        //Debug.Log (rot);
        float startOffset = Random.Range(-randomAngleGroup, randomAngleGroup);
        patternPerShoot = Mathf.Min(patternPerShoot, circlePatternCount);
        for (int i = 0; i < patternPerShoot; i++)
        {
            float angle = rot * currentPatternIndex - offset;
            angle += Random.Range(-randomAnglePreObj, randomAnglePreObj);
            angle += startOffset;
            Vector2 dir = transform.up;
            dir = dir.Rotate(angle);
            Vector2 pos = ShootOrigin.position;
            BaseWeaponObject temp = Spawn(dir, pos);
            
            Debug.Log(angle);
            currentPatternIndex++;
            if (currentPatternIndex >= circlePatternCount) currentPatternIndex = 0;
        }
    }
    public void FormShoot()
    {
        for (int i = 0; i < formPositionList.Count; i++)
        {
            Vector2 dir= transform.up;
            Vector2 pos = ShootOrigin.TransformPoint(formPositionList[i]);
            if (formCircleDir) { dir=((Vector3)pos - transform.position).normalized; }
            BaseWeaponObject temp = Spawn(dir, pos);
            //temp.transform.position = ShootOrigin.TransformPoint(formPositionList[i]);
            
                

        }
    }
    public BaseWeaponObject Spawn(Vector2 dir,Vector2 pos)
    {
        Debug.Log("SpawnObj "+ spawnObj.name);
        BaseWeaponObject wObj = GameManager.instance.bulletEngine.GetPoolWeaponObject(spawnObj);
        if(parentShootOrigin)
        {
            wObj.transform.parent = ShootOrigin;
        }
        wObj.transform.up = dir;
        wObj.transform.eulerAngles += new Vector3(0, 0, Random.Range(-randomAnglePreObj, randomAnglePreObj));
        wObj.transform.position = pos;
        wObj.Init();
        return wObj;
    }
    public void StartReload()
    {
        currentReloadTime = reloadTime;
        
    }
    public void EndReload()
    {
        if (currentAmmo > 0) ammoCount += currentAmmo;
        int getAmo = (int)Mathf.Min(clipSize, ammoCount);
        getAmo = Mathf.Max(getAmo, 0);
        if (ammoCount>= getAmo)
        {
            
            ammoCount -= getAmo;
        }
        else
        {
            currentReloadTime = reloadTime;
            return;
        }
        
        currentAmmo = getAmo;
    }
    void Pause(bool pause)
    {

    }
    void OnValidate()
    {
        SetupOrigin();
    }
    void OnDrawGizmos()
    {
        string s = "";
        s += type.ToString();
        s += " Amo: " + currentAmmo + " / " + clipSize+" ("+ammoCount+")";
        Handles.Label(transform.position, new GUIContent(s, ""));
    }
    void SetupOrigin()
    {
        if (ShootOrigin == null)
        {
            if(transform.FindChild("ShootOrigin"))
            {
                ShootOrigin = transform.FindChild("ShootOrigin");
            }
            else
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
}
