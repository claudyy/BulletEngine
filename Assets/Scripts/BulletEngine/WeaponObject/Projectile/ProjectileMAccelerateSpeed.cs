using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMAccelerateSpeed : BaseProjectileModifier {
    //public float startSpeed;
    //public float endSpeed;
    public AnimationCurve speedOfLifeTime;
    public override void Init(Projectile _projectile)
    {
        base.Init(_projectile);
    }
    public override void Tick()
    {
        base.Tick();
        //projectile.curSpeed = Mathf.Lerp(endSpeed, startSpeed, projectile.curLifeTime / projectile.lifeTime);
        projectile.curSpeed = speedOfLifeTime.Evaluate(projectile.lifeTime-projectile.curLifeTime);
    }
}
