using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterInsideCollisionCheck : MonoBehaviour
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
        if (collision.gameObject.layer == 8 && isCollision == false) // 차징 이동 중, 최초 접촉한 대상으로부터 exit를 체크하기 위한 구조
        {
            Debug.Log(collision.gameObject.name);
            isCollision = true;
            enterObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == enterObject) // 추락 중, 최초 접촉한 대상이 아닌 경우 반응하지 않도록 함
        {
            isCollision = false;
            if (GetComponentInParent<PlayerCharacterControl>().GetIsMove() == false)
            {
                gameObject.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().InsideCollsionEnd();
            }
            enterObject = null;
        }
    }
}
