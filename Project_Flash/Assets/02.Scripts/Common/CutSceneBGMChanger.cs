using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneBGMChanger : MonoBehaviour
{
    public GameObject player;
    public GameObject manager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            manager.GetComponent<Main_CutSceneManager>().ChangeSFX();
        }   
    }
}
