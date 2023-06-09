using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_BigAndSmall : MonoBehaviour
{
    Vector3 scale;
    public float delta;
    public float speed;

    public float delay;
    private void Start()
    {
        scale = transform.localScale;
    }
    private void Update()
    {
        
        Vector3 v = scale;
        v.x += Mathf.Abs(delta * Mathf.Sin(delay + (Time.time * speed)));
        v.y += Mathf.Abs(delta * Mathf.Sin(delay + (Time.time * speed)));

        transform.localScale = v;
    }
}
