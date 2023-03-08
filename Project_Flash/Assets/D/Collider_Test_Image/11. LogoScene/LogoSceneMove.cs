using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneMove : MonoBehaviour
{
    public float posX = 0;
    public float startY = 0;
    public float endY = 0;
    public float posZ = 0;


    void Update()
    {
        if(this.transform.position.y <= endY)
        {
            Vector3 newPosition = new Vector3(posX, endY, posZ);
            this.transform.position = newPosition;
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void MoveReset()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        Vector3 newPosition = new Vector3( posX, startY, posZ );
        this.transform.position = newPosition;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void MoveStart()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
