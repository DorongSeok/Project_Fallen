using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_LeftRightMove : MonoBehaviour
{
    Vector3 pos;
    public float delta;
    public float speed;

    public float delay;
    private void Start()
    {
        pos = transform.position;
    }
    private void Update()
    {
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(delay + (Time.time * speed));

        transform.position = v;
    }
}
