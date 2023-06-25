using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Blackhole : MonoBehaviour
{
    private PointEffector2D effector;
    private GameObject player;
    private void Awake()
    {
        effector = GetComponent<PointEffector2D>();
    }
    private void OnEnable()
    {
        PlayerCharacterControl.onPlayerFallingStart += this.EffectorOff;
        PlayerCharacterControl.onPlayerFallingEnd += this.EffectorOn;

        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        catch
        {
            Debug.Log("null");
        }
        if (player.GetComponent<PlayerCharacterControl>().GetIsFallen() == true)
        {
            EffectorOff();
        }
        else if (player.GetComponent<PlayerCharacterControl>().GetIsFallen() == false)
        {
            EffectorOn();
        }
    }
    private void OnDisable()
    {
        PlayerCharacterControl.onPlayerFallingStart -= this.EffectorOff;
        PlayerCharacterControl.onPlayerFallingEnd -= this.EffectorOn;
    }

    void EffectorOn()
    {
        effector.forceMagnitude = -10.0f;
    }

    void EffectorOff()
    {
        effector.forceMagnitude = 0.0f;
    }
}
