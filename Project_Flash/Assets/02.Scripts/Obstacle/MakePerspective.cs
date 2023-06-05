using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePerspective : MonoBehaviour
{
    private GameObject player;
    public float perspectiveDegreeX = 0.01f;
    public float perspectiveDegreeY = 0.1f;
    public float maxHeight = 300.0f;
    private float minHeight = -0.82f;
    private float maxWidth = 8.3f;
    private float minWidth = -8.3f;

    private float playerX;
    private float playerY;

    void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;
        playerX = playerPos.x;
        playerY = playerPos.y;

        if (playerX < 0)
        {
            playerX = playerPos.x * (minWidth * perspectiveDegreeX);
            playerX /= minWidth;
        }
        else if(playerX > 0)
        {
            playerX = playerPos.x * (maxWidth * perspectiveDegreeX);
            playerX /= maxWidth;
        }
        if (playerY < 0)
        {
            playerY = playerPos.y * (minHeight * perspectiveDegreeY);
            playerY /= minHeight;
        }
        else if (playerY > 0)
        {
            playerY = playerPos.y * (maxHeight * perspectiveDegreeY);
            playerY /= maxHeight;
        }
        playerPos.x = playerX;
        playerPos.y = playerY;

        this.transform.position = playerPos;
    }
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
