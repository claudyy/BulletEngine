using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager  {
    protected bool pause;
	public virtual void Init()
    {

    }
    public virtual void Tick()
    {

    }
    public virtual void Save()
    {

    }
    public virtual void Pause(bool p)
    {
        pause = p;
    }
    public virtual void ExitLevel()
    {

    }
    public virtual void OnLevelLoad()
    {

    }
}
