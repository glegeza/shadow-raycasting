using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    public float RotationSpeed = 25.0f;

    private void Update ()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, RotationSpeed * Time.deltaTime));
	}
}
