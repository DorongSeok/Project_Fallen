using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBugChecker : MonoBehaviour
{
    List<GameObject> enterObject = new List<GameObject>();

    private void OnEnable()
    {
        enterObject.Clear();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) 
        {
            if (enterObject.Count == 0)
            {
                Physics2D.IgnoreLayerCollision(6, 8, true);
            }
            enterObject.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enterObject.Contains(collision.gameObject)) 
        {
            enterObject.Remove(collision.gameObject);
            if (enterObject.Count == 0)
            {
                Physics2D.IgnoreLayerCollision(6, 8, false);
            }
        }
    }
}
