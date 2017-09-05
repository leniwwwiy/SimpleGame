using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public UnitController Unit;
    public UnitController targetUnit;
    private List<RaycastInfo> raycastResult;
    bool needToPiu = false;
    Vector3 needDirection, targetPosition;
    void Start()
    {
        targetPosition = Unit.transform.position;
    }

    void Update()
    {
        if (TryToShot())
        {
            needToPiu = true;
            targetPosition = Unit.transform.position;
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
        else
        {
            if (targetPosition == Unit.transform.position)
            {
                GoTo();
            }
            Unit.transform.up = (targetPosition - Unit.transform.position).normalized;
            Unit.Go();
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

    void GoTo()
    {
        raycastResult = Unit.raycastTargets(1);
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < raycastResult.Count; ++i)
        {
            if (raycastResult[i].count == 0)
            {
                raycastResult.Remove(raycastResult[i]);
                i--;
            }
        }

        for (int i = 1; i < raycastResult.Count; ++i)
        {
            RaycastInfo info = raycastResult[i], info1 = raycastResult[i - 1];
            if (info.hits[0].collider.name != info1.hits[0].collider.name)
            {
                points.Add((info.hits[0].point + info1.hits[0].point) / 2);
            }
        }

        if (points.Count < 1)
        {
            targetPosition = targetUnit.transform.position;
            return;
        }

        targetPosition = points[0];
        for (int i = 1; i < points.Count; ++i)
        {
            if (Vector3.Distance(targetUnit.transform.position, points[i]) < Vector3.Distance(targetUnit.transform.position, targetPosition))
            {
                targetPosition = points[i];
            }
        }
    }
}
