using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffChecker : MonoBehaviour
{
    public List<GameObject> Obstacles = new List<GameObject>();  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            for (int i = 0; i < Obstacles.Count; i++)
            {
                Obstacles[i].SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            for (int i = 0; i < Obstacles.Count; i++)
            {
                Obstacles[i].SetActive(false);
            }
        }
    }
}
