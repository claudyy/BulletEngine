using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectielMShootWeapon : BaseProjectileModifier {
    public Weapon weapon;
    public override void Tick()
    {
        base.Tick();
        weapon.TickShoot();
    }
}
