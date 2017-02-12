using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOwnerDummy : MonoBehaviour {
    public Weapon myWeapon;
    public Gradient color;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            myWeapon.TickShoot();
        }
        if (Input.GetMouseButtonUp(0))
        {
            myWeapon.ShootEnd();
        }
        transform.eulerAngles += new Vector3(0, 0, 45 * Time.smoothDeltaTime);
    }
}
