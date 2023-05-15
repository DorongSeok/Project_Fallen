using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutChecker : MonoBehaviour
{
    public Transform restartPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.transform.position = restartPosition.position;
        }
    }
}
