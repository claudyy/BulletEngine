using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    protected Transform ShootOrigin;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Init()
    {

    }
    void Tick()
    {

    }
    void Pause(bool pause)
    {

    }
    void OnValidate()
    {
        if(ShootOrigin==null)
        {
            GameObject go = new GameObject();
            go.name = "ShootOrigin";
            ShootOrigin = go.transform;
            ShootOrigin.parent = transform;
            ShootOrigin.position = transform.position;
            ShootOrigin.rotation = transform.rotation;
        }
    }
}
