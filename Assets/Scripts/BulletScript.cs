using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float moveSpeed;
    public Transform parentGun;
    public float lifeRange = 10;
    public float radius = 1;
    
    private Vector3 lastPosition;
    private Vector3 direction;

    public void Shot()
    {
        lastPosition = transform.position + transform.up * radius;
        direction = transform.up;
    }

	void Update () {
        transform.position = transform.position + direction * moveSpeed;
        if (Vector3.Distance(transform.position, parentGun.position) > lifeRange)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //moveSpeed = 0;
        UnitController unit = collision.collider.GetComponent<UnitController>();
        if (unit != null)
        {
            if (unit.enemy)
            {
                MainController.instance.scoreController.scoreMy++;
            }
            else
            {
                MainController.instance.scoreController.scoreBot++;
            }
            Destroy(this.gameObject);
        }
        else
        { 
            lastPosition = collision.contacts[0].point;
            Debug.DrawRay(lastPosition, -direction, Color.red);
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
            direction.Normalize();
            Debug.DrawRay(lastPosition, direction, Color.red);
        }
    }
}
