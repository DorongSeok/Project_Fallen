using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_CircleMove : MonoBehaviour
{
    public float moveSpeed;
    private void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
}
