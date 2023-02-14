using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Wind : MonoBehaviour
{
    public float windPower;
    public bool windDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }
}
