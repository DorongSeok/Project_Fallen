using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Wind : MonoBehaviour
{
    private AreaEffector2D effector;

    private void Awake()
    {
        effector = GetComponent<AreaEffector2D>();
    }

    private void Start()
    {
        // �׽�Ʈ ���� ����Ǹ� ������ ��
        PlayerCharacterTest2.onPlayerFallingStart += this.EffectorOff;
        PlayerCharacterTest2.onPlayerFallingEnd += this.EffectorOn;

        PlayerCharacterControl.onPlayerFallingStart += this.EffectorOff;
        PlayerCharacterControl.onPlayerFallingEnd += this.EffectorOn;
    }
    private void OnDisable()
    {
        PlayerCharacterTest2.onPlayerFallingStart -= this.EffectorOff;
        PlayerCharacterTest2.onPlayerFallingEnd -= this.EffectorOn;

        PlayerCharacterControl.onPlayerFallingStart -= this.EffectorOff;
        PlayerCharacterControl.onPlayerFallingEnd -= this.EffectorOn;
    }

    void EffectorOn()
    {
        effector.forceMagnitude = 10.0f;
    }

    void EffectorOff()
    {
        effector.forceMagnitude = 0.0f;
    }
}
