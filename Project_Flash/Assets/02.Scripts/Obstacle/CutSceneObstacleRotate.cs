using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneObstacleRotate : MonoBehaviour
{
    public float rotateSpeed = 1.0f;

    void FixedUpdate()
    {
        this.transform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed);
    }
}
