using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemaChecker : MonoBehaviour
{
    public GameManager gameManager;
    public int thema_Num;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            gameManager.ThemaChange(thema_Num);
            if (thema_Num == 5)
            {
                collision.gameObject.GetComponent<PlayerCharacterControl>().LightOn();
            }
        }
    }
}
