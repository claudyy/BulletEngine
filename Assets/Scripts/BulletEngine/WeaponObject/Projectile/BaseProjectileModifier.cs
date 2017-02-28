using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectileModifier : MonoBehaviour {
    protected Projectile projectile;
	public virtual void Init(Projectile _projectile)
    {
        projectile = _projectile;
    }
    public virtual void Tick()
    {

    }
}
