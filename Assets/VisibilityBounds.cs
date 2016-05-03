using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class VisibilityBounds : MonoBehaviour
{
    public class VisibilityEdge
    {
        public VisibilityEdge(Vector3 start, Vector3 end)
        {
            Start = start;
            End = end;
        }

        public Vector3 Start { get; private set; }
        public Vector3 End { get; private set; }
        public VisibilityEdge NextEdge { get; set; }
        public VisibilityEdge PreviousEdge { get; set; }
    }

    public List<VisibilityEdge> Edges { get; protected set; }

    public IEnumerable<Vector3> GetTransformedPoints()
    {
        var currentEdge = Edges[0];
        var firstPoint = currentEdge.Start;
        var transformedPoints = new List<Vector3>();
        do
        {
            transformedPoints.Add(transform.TransformPoint(currentEdge.Start));
            currentEdge = currentEdge.NextEdge;
        } while (currentEdge.Start != firstPoint);

        return transformedPoints;
    }
}