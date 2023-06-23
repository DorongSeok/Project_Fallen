using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterInsideCollisionCheck : MonoBehaviour
{
    bool isCollision = false;
    List<GameObject> enterObject = new List<GameObject>();

    public bool GetIsCollision()
    {
        return isCollision;
    }
    public void SetIsCollision(bool isCollision)
    {
        this.isCollision = isCollision;
    }
    private void OnEnable()
    {
        isCollision = false;
        enterObject.Clear();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) // 차징 이동 중, 접촉한 모든 대상으로부터 exit를 체크하기 위한 구조
        {
            isCollision = true;
            enterObject.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (enterObject.Contains(collision.gameObject)) // 추락 중, 접촉한 대상이 아닌 경우 반응하지 않도록 함
            {
                enterObject.Remove(collision.gameObject);
                if (enterObject.Count == 0)
                {
                    isCollision = false;
                    if (GetComponentInParent<PlayerCharacterControl>().GetIsMove() == false)
                    {
                        gameObject.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().InsideCollsionEnd();
                    }
                }
            }
        }
    }
}
