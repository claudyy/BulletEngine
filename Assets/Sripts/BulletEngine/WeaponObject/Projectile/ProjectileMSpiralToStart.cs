using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMSpiralToStart : BaseProjectileModifier {
    float spiralSpeed;
    public bool spiralRight = true;
    public override void Init(Projectile _projectile)
    {
        base.Init(_projectile);
        spiralSpeed = 360 / projectile.lifeTime;
        spiralSpeed *= spiralRight == true ? 1 : -1;
    }
    public override void Tick()
    {
        base.Tick();
        projectile.dir = projectile.dir.Rotate(spiralSpeed*Time.smoothDeltaTime);
    }
}
