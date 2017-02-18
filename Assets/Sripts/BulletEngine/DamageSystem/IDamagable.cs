﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TookDamageInfo
{
    public bool tookDamage;
    public TookDamageInfo(bool _tookDamage)
    {
        tookDamage = _tookDamage;
    }
}
[System.Serializable]
public class Attributes
{
    public int health;
    public bool isDead;
    public TookDamageInfo ApplyDamage(Damage d)
    {
        health -= d.damage;
        if(health<=0)
        {
            isDead = true;
        }
        return new TookDamageInfo(true);
    }

}
[System.Serializable]
public class Damage
{
    public int damage;
}
public interface IDamagable  {

   Attributes GetAttributes();
}
