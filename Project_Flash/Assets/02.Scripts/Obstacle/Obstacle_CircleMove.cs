using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_CircleMove : MonoBehaviour
{
    public float moveSpeed;
    public bool isClockwise;
    private void Update()
    {
        if (isClockwise == true)
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * moveSpeed);
        }
        else if (isClockwise == false)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
    }
}
