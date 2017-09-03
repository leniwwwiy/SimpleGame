using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public KeyCode RotateLeft, RotateRight, Go, GoBack, GoLeft, GoRight, Fire, Raycast;
    public UnitController Unit;

    void Start () {
    }

	void Update () {
        if (Input.GetKey(Go))
        {
            Unit.Go();
        }
        if (Input.GetKey(GoBack))
        {
            Unit.GoBack();
        }
        if (Input.GetKey(GoRight))
        {
            Unit.GoRight();
        }
        if (Input.GetKey(GoLeft))
        {
            Unit.GoLeft();
        }
        if (Input.GetKey(RotateLeft))
        {
            Unit.RotateLeft();
        }
        if (Input.GetKey(RotateRight))
        {
            Unit.RotateRight();
        }
        if (Input.GetKeyDown(Fire))
        {
            Unit.Piu();
        }
        if (Input.GetKeyDown(Raycast))
        {
            //Unit.raycastTargets(10);
            //Unit.raycastTargets(4);
        }
    }

    void FixedUpdate()
    {
    }
}
