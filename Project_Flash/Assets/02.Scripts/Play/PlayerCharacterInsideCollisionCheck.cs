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
        if (collision.gameObject.layer == 8) // ��¡ �̵� ��, ������ ��� ������κ��� exit�� üũ�ϱ� ���� ����
        {
            isCollision = true;
            enterObject.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (enterObject.Contains(collision.gameObject)) // �߶� ��, ������ ����� �ƴ� ��� �������� �ʵ��� ��
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
