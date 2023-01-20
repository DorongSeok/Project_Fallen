using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterInsideCollisionCheck : MonoBehaviour
{
    bool isCollision = false;

    public bool InsideCollisionCheck()
    {
        return isCollision;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isCollision = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isCollision = false;
            gameObject.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().InsideCollsionEnd();
        }
    }
}
