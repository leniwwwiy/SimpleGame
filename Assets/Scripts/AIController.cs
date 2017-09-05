using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public UnitController Unit;
    public UnitController targetUnit;
    private List<RaycastInfo> raycastResult;
    bool needToPiu = false;
    Vector3 needDirection;
    void Start()
    {
    }

    void Update()
    {
        if (TryToShot())
        {
            needToPiu = true;
        }
        else
        {
            needToPiu = false;
        }
        if (needToPiu)
        {
            Unit.transform.up = needDirection;
            Unit.Piu();
        }
    }

    bool TryToShot()
    {
        raycastResult = Unit.raycastTargets(4);
        for (int it = 0; it < raycastResult.Count; ++it)
        {
            RaycastInfo info = raycastResult[it];
            if (info.count < 1)
            {
                continue;
            }
            UnitController unit = info.hits[info.count - 1].collider.GetComponent<UnitController>();
            if (unit)
            {
                if (!unit.enemy)
                {
                    needDirection = info.direction;
                    return true;
                }
            }
        }
        return false;
    }
}
