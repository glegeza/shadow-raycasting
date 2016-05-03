using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxColliderVisibilityBounds : VisibilityBounds
{
    private BoxCollider2D _boxCollider;

	private void Start ()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        GetEdges();
	}

    private void GetEdges()
    {
        var halfWidth = _boxCollider.size.x / 2.0f;
        var halfHeight = _boxCollider.size.y / 2.0f;
        var upperLeft = new Vector3(-halfWidth, halfHeight);
        var lowerLeft = new Vector3(-halfWidth, -halfHeight);
        var upperRight = new Vector3(halfWidth, halfHeight);
        var lowerRight = new Vector3(halfWidth, -halfHeight);

        var leftEdge = new VisibilityEdge(lowerLeft, upperLeft);
        var topEdge = new VisibilityEdge(upperLeft, upperRight);
        var rightEdge = new VisibilityEdge(upperRight, lowerRight);
        var bottomEdge = new VisibilityEdge(lowerRight, lowerLeft);
        leftEdge.NextEdge = topEdge;
        leftEdge.PreviousEdge = bottomEdge;
        topEdge.NextEdge = rightEdge;
        topEdge.PreviousEdge = leftEdge;
        rightEdge.NextEdge = bottomEdge;
        rightEdge.PreviousEdge = topEdge;
        bottomEdge.NextEdge = leftEdge;
        bottomEdge.PreviousEdge = rightEdge;
        Edges = new List<VisibilityEdge>()
        {
            leftEdge,
            topEdge,
            rightEdge,
            bottomEdge
        };
    }
}