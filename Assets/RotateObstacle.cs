using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    public float rotationSpeed = 45f; // Adjust the speed as needed

    void Update()
    {
        float rotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotation);
    }
}
