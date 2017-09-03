using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float bulletStart;
    public GameObject BulletPrefab;
	// Use this for initialization
	public void Shot()
    {
        GameObject shot = Instantiate(BulletPrefab) as GameObject;
        shot.transform.rotation = transform.rotation;
        shot.transform.position = transform.position + transform.up * bulletStart;
        shot.GetComponent<BulletScript>().parentGun = transform;
        shot.GetComponent<BulletScript>().Shot();
    }
}
