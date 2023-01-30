using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_UpDownMove : MonoBehaviour
{
    Vector3 pos;
    public float delta;
    public float speed;

    public float delay;

    private void Start()
    {
        pos = transform.localPosition;
    }
    private void Update()
    {
        Vector3 v = pos;
        v.y += delta * Mathf.Sin(delay + (Time.time * speed));

        transform.localPosition = v;
    }
}
