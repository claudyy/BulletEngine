using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    //per Object
    public Vector2 randomAnglePreObj;
    //Group
    public Vector2 randomAngleGroup;

    //circle
    public int circlePatternCount;
    public float maxAngle;
    public bool circleRight = true;
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
        if (circleRight == false) { rot *= -1; offset *= -1; }
        //Debug.Log (rot);
        float startOffset = Random.Range(randomAngleGroup.x, randomAngleGroup.y);
        patternPerShoot = Mathf.Min(patternPerShoot, circlePatternCount);
        for (int i = 0; i < patternPerShoot; i++)
        {
            BaseWeaponObject temp = Spawn();
            temp.transform.position = ShootOrigin.position;
            temp.transform.up = transform.up;
            Vector3 angle = new Vector3(0, 0, temp.transform.eulerAngles.z + rot * currentPatternIndex - offset);
            angle += new Vector3(0,0, Random.Range(randomAnglePreObj.x, randomAnglePreObj.y));
            angle += new Vector3(0,0, startOffset);
            Debug.Log(angle);
            temp.transform.rotation = Quaternion.Euler(angle);
            currentPatternIndex++;
            if (currentPatternIndex >= circlePatternCount) currentPatternIndex = 0;
        }
    }
    public void FormShoot()
    {
        for (int i = 0; i < formPositionList.Count; i++)
        {
            BaseWeaponObject temp = Spawn();
            temp.transform.position = ShootOrigin.position;
            temp.transform.position = ShootOrigin.TransformPoint(formPositionList[i]);
            if (formCircleDir) { temp.transform.up = (temp.transform.position- transform.position ).normalized; }
            else { temp.transform.up = transform.up; }
                

        }
    }
    public BaseWeaponObject Spawn()
    {
        Debug.Log("SpawnObj "+ spawnObj.name);
        BaseWeaponObject wObj = BulletEngine.instance.GetPoolWeaponObject(spawnObj);
        if(wObj is Beam)
        {
            wObj.transform.parent = ShootOrigin;
        }
        return wObj;
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
