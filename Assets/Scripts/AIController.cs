using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    public UnitController Unit;
    public UnitController targetUnit;
    private List<RaycastInfo> raycastResult;
    void Start () {
        Debug.Log(TryToShot());
    }

	void Update () {
    }

    bool TryToShot()
    {
        raycastResult = Unit.raycastTargets(4);
        foreach (var info in raycastResult)
        {
            if (info.count<0)
            {
                continue;
            }
            UnitController unit = info.hits[info.count - 1].collider.GetComponent<UnitController>();
            if (unit)
            {
                if (!unit.enemy)
                {
                    while (Vector3.Angle(unit.transform.up, info.direction) > unit.UnitRotationSpeed)
                    {
                        unit.RotateLeft(Vector3.Angle(unit.transform.up, info.direction));
                    }
                    unit.Piu();
                }
                return true;
            }
        }
        return false;
    }
}
