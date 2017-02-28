using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTick : MonoBehaviour {
    protected bool pause;
	// Use this for initialization
	public virtual void Awake () {
		
	}
	public virtual void Start()
    {

    }
    public virtual void Tick()
    {

    }
    public virtual void PauseTick()
    {

    }
    public virtual void Pause(bool p)
    {
        pause = p;
    }
}
