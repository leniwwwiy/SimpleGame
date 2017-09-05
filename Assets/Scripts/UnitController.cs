using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInfo
{
    public Vector3 source, direction;
    public List<RaycastHit2D> hits;
    public List<Vector3> outDirections;
    public int count;
    public RaycastInfo()
    {
        hits = new List<RaycastHit2D>();
        outDirections = new List<Vector3>();
        count = 0;
    }

    public void AddHit(RaycastHit2D hit, Vector3 outDirection)
    {
        hits.Add(hit);
        outDirections.Add(outDirection);
        count++;
    }
}

public class UnitController : MonoBehaviour
{
    public float UnitSpeed, UnitRotationSpeed;
    private Weapon gun;
    public bool enemy;
    public float callDown;
    private float timer;
    private const int reflectAngle = 85;
    
    void Start()
    {
        gun = GetComponent<Weapon>();
    }

    void Update()
    {
        if (timer <= callDown)
        {
            timer += Time.deltaTime;
        }
    }

    public void Go()
    {
        transform.position = transform.position + transform.up * UnitSpeed;
    }

    public void GoBack()
    {
        transform.position = transform.position + transform.up * (-UnitSpeed);
    }

    public void GoRight()
    {
        transform.position = transform.position + transform.right * UnitSpeed;
    }

    public void GoLeft()
    {
        transform.position = transform.position + transform.right * (-UnitSpeed);
    }

    public void RotateLeft(float angle)
    {
        transform.Rotate(0, 0, -Mathf.Max(UnitRotationSpeed, angle));
    }

    public void RotateLeft()
    {
        transform.Rotate(0, 0, -UnitRotationSpeed);
    }

    public void RotateRight(float angle)
    {
        transform.Rotate(0, 0, Mathf.Max(UnitRotationSpeed, angle));
    }

    public void RotateRight()
    {
        transform.Rotate(0, 0, UnitRotationSpeed);
    }

    public List<RaycastInfo> raycastTargets(int deep)
    {
        List<RaycastInfo> ans = new List<RaycastInfo>();
        float angle = 1f;

        for (float i = 0; i < 360; i += angle)
        {
            RaycastInfo info = new RaycastInfo();

            info.source = transform.position + transform.up * (GetComponent<CircleCollider2D>().radius * transform.localScale.x + 0.1f);
            info.direction = transform.up;
            RaycastHit2D hit = Physics2D.Raycast(info.source, info.direction);
            if (hit.collider != null)
            {
                info.AddHit(hit, Vector3.Reflect(info.direction, hit.normal));
            }
            transform.Rotate(0, 0, angle);
            ans.Add(info);
        }

        for (int i=0; i<ans.Count; ++i)
        {
            RaycastInfo info = ans[i];
            
            for (int j = 0; j < deep && j < info.count; ++j)
            {
                RaycastHit2D oldHit = info.hits[j];
                if (oldHit.collider.gameObject.GetComponent<UnitController>())
                {
                    Debug.DrawRay(oldHit.point, info.outDirections[j] * Vector3.Distance(oldHit.point, oldHit.point), Color.red);
                    continue;
                }
                else
                {
                    Debug.DrawRay(oldHit.point, info.outDirections[j] * Vector3.Distance(oldHit.point, oldHit.point), Color.blue);
                }
                if (oldHit.collider.gameObject.GetComponent<BulletScript>())
                {
                    Debug.DrawRay(oldHit.point, info.outDirections[j] * Vector3.Distance(oldHit.point, oldHit.point), Color.red);
                    continue;
                }
                RaycastHit2D hit = Physics2D.Raycast(oldHit.point + new Vector2(info.outDirections[j].x * 0.1f, info.outDirections[j].y * 0.1f), info.outDirections[j]);
                if (hit.collider != null)
                {
                    info.AddHit(hit, Vector3.Reflect(info.outDirections[j], hit.normal));
                }
            }
        }

        return ans;
    }
    public RaycastInfo Aiming(int deep)
    {
        List<RaycastInfo> ans = new List<RaycastInfo>();
        float angle = 1f;

        for (float i = 0; i < angle; i += angle)
        {
            RaycastInfo info = new RaycastInfo();

            info.source = transform.position + transform.up * (GetComponent<CircleCollider2D>().radius * transform.localScale.x + 0.1f);
            info.direction = transform.up;
            RaycastHit2D hit = Physics2D.Raycast(info.source, info.direction);
            if (hit.collider != null)
            {
                info.AddHit(hit, Vector3.Reflect(info.direction, hit.normal));
            }
            ans.Add(info);
        }

        if (ans[0].count < 1)
        {
            Debug.DrawRay(ans[0].source, ans[0].direction, Color.red, 1f, true);
        }

       // for (int i = 0; i < ans.Count; ++i)
       // {
            RaycastInfo info1 = ans[0];

            for (int j = 0; j < deep && j < info1.count; ++j)
            {
                RaycastHit2D oldHit = info1.hits[j];
                if (oldHit.collider.gameObject.GetComponent<BulletScript>() || oldHit.collider.gameObject.GetComponent<UnitController>())
                {
                    Debug.DrawRay(oldHit.point, info1.outDirections[j] * Vector3.Distance(oldHit.point, oldHit.point), Color.red, 1f, true);
                    continue;
                }
                else
                {
                    Debug.DrawRay(oldHit.point, info1.outDirections[j] * Vector3.Distance(oldHit.point, oldHit.point), Color.blue, 1f, true);
                }
                RaycastHit2D hit = Physics2D.Raycast(oldHit.point + new Vector2(info1.outDirections[j].x * 0.1f, info1.outDirections[j].y * 0.1f), info1.outDirections[j]);
                if (hit.collider != null)
                {
                    info1.AddHit(hit, Vector3.Reflect(info1.outDirections[j], hit.normal));
                }
            }
       // }
        Debug.Log(ans[0].count);
        return ans[0];
    }

    public void Piu()
    {
        if (timer > callDown)
        {
            gun.Shot();
            timer = 0;
        }
    }
}
