using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : BaseWeaponObject
{
    public float beamLength=10;
    float curBeamLength;
    LineRenderer lineRen;
    public override void Init()
    {
        base.Init();
        lineRen = GetComponent<LineRenderer>();
        lineRen.SetPosition(0, transform.position);
        lineRen.SetPosition(1, transform.position);
    }
    public override void Tick()
    {
        transform.localScale = new Vector3(transform.localScale.x, curBeamLength, transform.localScale.z);
        base.Tick();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, beamLength);
        if(hit)
        {
            curBeamLength = hit.distance;
        }
        else
        {
            curBeamLength = beamLength;
        }
        lineRen.SetPosition(0, transform.position);
        lineRen.SetPosition(1, transform.position+transform.up*curBeamLength);

    }

}
