using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType {Single,Circle,Form }
public class Weapon : MonoBehaviour {
    protected Transform ShootOrigin;
    public WeaponType type;
    public BaseWeaponObject spawnObj;
    public float shootCooldown;
    float curShootCooldown;
    public float repeatCooldown;
    float curRepeatCooldown;
    float curShootDuration;

    //circle
    public int circlePatternCount;
    public float maxAngle;
    //From
    public int formPositionCount;
    public List<Vector2> formPositionList;
    public bool formCircleDir;
	// Use this for initialization
	void Start () {
        BulletEngine.instance.AddWeapon(this);
        SetupOrigin();

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
        curRepeatCooldown -= Time.smoothDeltaTime;
        curShootCooldown -= Time.smoothDeltaTime;

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
        curShootDuration += Time.smoothDeltaTime;


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
        BaseWeaponObject temp = Spawn();
        temp.transform.position = ShootOrigin.position;
        temp.transform.up = transform.up;
    }
    public void CircleShoot()
    {
        float rot = maxAngle / (circlePatternCount);
        float offset = maxAngle / 2;
        //Debug.Log (rot);

        for (int i = 0; i < circlePatternCount; i++)
        {
            BaseWeaponObject temp = Spawn();
            temp.transform.position = ShootOrigin.position;
            temp.transform.up = transform.up;
            temp.transform.rotation = Quaternion.Euler(0, 0, temp.transform.eulerAngles.z + rot * i - offset);
        }
    }
    public void FormShoot()
    {
        for (int i = 0; i < formPositionList.Count; i++)
        {
            BaseWeaponObject temp = Spawn();
            temp.transform.position = ShootOrigin.position;
            temp.transform.position = transform.TransformPoint(formPositionList[i]);
            if (formCircleDir) { temp.transform.up = (temp.transform.position- transform.position ).normalized; }
            else { temp.transform.up = transform.up; }
                

        }
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
        SetupOrigin();
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
