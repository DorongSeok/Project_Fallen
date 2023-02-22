using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterInsideCollisionCheckTest : MonoBehaviour
{
    bool isCollision = false;
    GameObject enterObject;

    public bool GetIsCollision()
    {
        return isCollision;
    }
    public void SetIsCollision(bool isCollision)
    {
        this.isCollision = isCollision;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && isCollision == false)
        {
            isCollision = true;
            enterObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == enterObject)
        {
            isCollision = false;
            if (GetComponentInParent<PlayerCharacterTest2>().GetIsMove() == false)
            {
                gameObject.transform.parent.gameObject.GetComponent<PlayerCharacterTest2>().InsideCollsionEnd();
            }
            enterObject = null;
        }
    }
}
