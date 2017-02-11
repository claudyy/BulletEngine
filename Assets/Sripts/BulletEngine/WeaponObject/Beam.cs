using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : BaseWeaponObject
{
    public float beamLength=10;
    float curBeamLength;
    public override void Init()
    {
        base.Init();

    }
    public override void Tick()
    {
        transform.localScale = new Vector3(transform.localScale.x, curBeamLength, transform.localScale.z);
        base.Tick();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 10);
        if(hit)
        {
            curBeamLength = hit.distance;
        }
        else
        {
            curBeamLength = beamLength;
        }
    }

}
