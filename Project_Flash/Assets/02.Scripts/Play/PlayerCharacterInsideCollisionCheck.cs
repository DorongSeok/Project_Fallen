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
        if (collision.gameObject.layer == 8 && isCollision == false) // ��¡ �̵� ��, ���� ������ ������κ��� exit�� üũ�ϱ� ���� ����
        {
            Debug.Log(collision.gameObject.name);
            isCollision = true;
            enterObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == enterObject) // �߶� ��, ���� ������ ����� �ƴ� ��� �������� �ʵ��� ��
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
