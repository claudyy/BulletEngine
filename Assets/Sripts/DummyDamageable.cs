using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDamageable : MonoBehaviour,IDamagable {
    public Attributes attributes;
    public Attributes GetAttributes()
    {
        return attributes;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
