using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RayTest : MonoBehaviour
{

    private List<BoxColliderVisibilityBounds> _colliders = new List<BoxColliderVisibilityBounds>();
    private List<Vector2> _collisions = new List<Vector2>();
    
	private void Start ()
    {
        _colliders.AddRange(FindObjectsOfType<BoxColliderVisibilityBounds>());
    }
	
	private void Update ()
    {
        _collisions.Clear();
	}

    private void OnDrawGizmos()
    {
        var points = new List<Vector3>();
        foreach (var collider in _colliders)
        {
            foreach (var point in collider.GetTransformedPoints())
            {
                CastRay(point);
            }
        }
    }

    private void CastRay(Vector3 point)
    {
        var dir = new Vector2(point.x - transform.position.x, point.y - transform.position.y).normalized;
        var hit = Physics2D.Raycast(transform.position, dir);
        if (hit)
        {
            hit = Physics2D.Raycast(hit.point + dir * 0.01f, dir);
            Gizmos.color = Color.red;
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

    private IEnumerable<Vector3> GetSortedPoints(List<Vector3> pointList)
    {
        return pointList.OrderBy(p => GetAngle(p));
    }

    private float GetAngle(Vector3 point)
    {
        var dir = point - transform.position;
        return Mathf.Atan2(point.y, point.x);
    }
}
