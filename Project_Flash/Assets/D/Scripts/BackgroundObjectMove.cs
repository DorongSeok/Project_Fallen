using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectMove : MonoBehaviour
{
    public float addRotationZ = 0.05f;
    void FixedUpdate()
    {
        transform.Rotate(0, 0, addRotationZ);
    }
}
