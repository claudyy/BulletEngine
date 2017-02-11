using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : BaseWeaponObject
{
    public float speed=6;
    float curSpeed;
    List<BaseProjectileModifier> modifierList=new List<BaseProjectileModifier>();
    public Vector2 dir;
    public override void Init()
    {
        base.Init();
        dir = transform.up;
        modifierList = GetComponents<BaseProjectileModifier>().ToList<BaseProjectileModifier>();
        for (int i = 0; i < modifierList.Count; i++)
        {
            modifierList[i].Init(this);
        }
    }
    public override void Tick()
    {
        base.Tick();
        for (int i = 0; i < modifierList.Count; i++)
        {
            modifierList[i].Tick();
        }
        curSpeed = speed;
        transform.up = dir;
        transform.position += transform.up * curSpeed * Time.smoothDeltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1);
        if (hit)
        {
            DestroyWeaponObj();
        }
    }

}
