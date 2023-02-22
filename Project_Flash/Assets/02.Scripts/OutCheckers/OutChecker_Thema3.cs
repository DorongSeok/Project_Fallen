using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutChecker_Thema3 : MonoBehaviour
{
    public Transform restartPosition_Thema3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.transform.position = restartPosition_Thema3.position;
        }
    }
}
