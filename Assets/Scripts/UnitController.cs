using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void RotateLeft()
    {
        transform.Rotate(0, 0, -UnitRotationSpeed);
    }

    public void RotateRight()
    {
        transform.Rotate(0, 0, UnitRotationSpeed);
    }

    public List<GameObject> raycastTargets(int deep)
    {
        List<GameObject> ans = new List<GameObject>();
        float angle = 1f;

        Stack<KeyValuePair<Vector3, Vector3>> stack = new Stack<KeyValuePair<Vector3, Vector3>>();
        for (float i = 0; i < 360; i += angle)
        {
            Vector3 source = transform.position + transform.up * (GetComponent<CircleCollider2D>().radius * transform.localScale.x + 0.1f);

            Vector3 nextDirection = transform.up;
            if (Physics2D.Raycast(source, nextDirection))
            {
                RaycastHit2D hit = Physics2D.Raycast(source, nextDirection);
                if (hit.collider != null)
                {
                    Debug.DrawRay(source, transform.up * Vector3.Distance(source, hit.point), Color.red);
                    if (hit.collider.gameObject.GetComponent<UnitController>())
                    {

                    }
                    else
                    {
                        if (Mathf.Abs(Vector3.Angle(nextDirection, hit.normal) - reflectAngle) > 0.01)
                        {
                            stack.Push(new KeyValuePair<Vector3, Vector3>(hit.point, Vector3.Reflect(nextDirection, hit.normal)));
                        }
                    }
                }
            }
            transform.Rotate(0, 0, angle);
        }

        for (int j = 0; j < deep; ++j)
        {
            var stack2 = stack.ToArray();
            stack.Clear();
            for (int i = 0; i < stack2.Length; ++i)
            {
                KeyValuePair<Vector3, Vector3> obj = stack2[i];
                Vector3 source2 = obj.Key, dir = obj.Value;
                dir.Normalize();
                RaycastHit2D hit = Physics2D.Raycast(source2, dir);
                if (hit && hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<UnitController>())
                    {
                        Debug.DrawRay(source2, dir * Vector3.Distance(source2, hit.point), Color.blue);

                    }
                    else
                    {

                        Debug.DrawRay(source2, dir * Vector3.Distance(source2, hit.point), Color.red);
                    }
                    if (Mathf.Abs(Vector3.Angle(dir, hit.normal) - reflectAngle) > 0.01)
                    {
                        stack.Push(new KeyValuePair<Vector3, Vector3>(hit.point, Vector3.Reflect(dir, hit.normal)));
                    }
                }
                else
                {
                    Debug.DrawRay(source2, dir * 10, Color.red);
                }
            }
        }

        return ans;
    }
    public void Aiming()
    {
        KeyValuePair<Vector3, Vector3> first = new KeyValuePair<Vector3, Vector3>(Vector3.right, Vector3.right);
        Vector3 source = transform.position + transform.up * (GetComponent<CircleCollider2D>().radius * transform.localScale.x + 0.1f);

        Vector3 nextDirection = transform.up;
        RaycastHit2D hit = Physics2D.Raycast(source, nextDirection);
        if (hit && hit.collider != null)
        {
            Debug.DrawRay(source, transform.up * Vector3.Distance(source, hit.point), Color.red);
            if (hit.collider.gameObject.GetComponent<UnitController>())
            {

            }
            else
            {
                if (Mathf.Abs(Vector3.Angle(nextDirection, hit.normal) - reflectAngle) > 0.01)
                {
                    first = new KeyValuePair<Vector3, Vector3>(hit.point, Vector3.Reflect(nextDirection, hit.normal));
                }
            }
        }
        else
        {
            Debug.DrawRay(source, transform.up * 10, Color.red);
        }

        bool da = (first.Key == first.Value && first.Key == Vector3.right);
        while (da)
        {
            Vector3 source2 = first.Key, dir = first.Value;
            dir.Normalize();
            RaycastHit2D hit2 = Physics2D.Raycast(source2, dir);
            if (hit2 && hit2.collider != null)
            {
                if (hit2.collider.gameObject.GetComponent<UnitController>())
                {
                    Debug.DrawRay(source2, dir * Vector3.Distance(source2, hit2.point), Color.blue);
                }
                else
                {
                    Debug.DrawRay(source2, dir * Vector3.Distance(source2, hit2.point), Color.red);
                }
                if (Mathf.Abs(Vector3.Angle(dir, hit2.normal) - reflectAngle) > 0.01)
                {
                    first = new KeyValuePair<Vector3, Vector3>(hit2.point, Vector3.Reflect(dir, hit2.normal));
                }
            }
            else
            {
                Debug.DrawRay(source2, dir * 10, Color.red);
            }
            da = (first.Key == first.Value && first.Key == Vector3.right);
        }
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
