using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class RayTest : MonoBehaviour
{

    private List<BoxCollider2D> _colliders = new List<BoxCollider2D>();
    private List<PolygonCollider2D> _polyColliders = new List<PolygonCollider2D>();
    private List<Vector2> _collisions = new List<Vector2>();
    
	void Start ()
    {
        _colliders.AddRange(FindObjectsOfType<BoxCollider2D>());
        _polyColliders.AddRange(FindObjectsOfType<PolygonCollider2D>());
    }
	
	void Update ()
    {
        _collisions.Clear();
	}

    void OnDrawGizmos()
    {
        var points = new List<Vector3>();
        foreach (var collider in _colliders)
        {
            var halfWidth = collider.size.x / 2.0f;
            var halfHeight = collider.size.y / 2.0f;
            points.Clear();
            points.Add(collider.transform.TransformPoint(new Vector3(-halfWidth, halfHeight)));
            points.Add(collider.transform.TransformPoint(new Vector3(-halfWidth, -halfHeight)));
            points.Add(collider.transform.TransformPoint(new Vector3(halfWidth, halfHeight)));
            points.Add(collider.transform.TransformPoint(new Vector3(halfWidth, -halfHeight)));
            foreach (var point in points)
            {
                var dir = new Vector2(point.x - transform.position.x, point.y - transform.position.y).normalized;
                var hit = Physics2D.Raycast(transform.position, dir);
                if (hit)
                {
                    hit = Physics2D.Raycast(hit.point + dir * 0.01f, dir);
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(hit.point, 0.05f);
                    if (hit)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(transform.position, hit.point);
                        Gizmos.DrawSphere(hit.point, 0.05f);
                    }
                    else
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawRay(transform.position, dir.normalized * 100);
                    }
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(transform.position, dir.normalized * 100);
                }
            }
        }
    }
}
