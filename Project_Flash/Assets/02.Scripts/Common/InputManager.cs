using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    public void OnUpdate()
    {
        // Ű���峪 ���콺 �Է��� �������� �ٷ� ����
        if(Input.anyKey == false)
        {
            return;
        }

        // ���콺 �Է�
        if(MouseAction != null)
        {
            // ���콺�� ������ ������
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            // ���콺�� ������ �ʰ� ������
            else
            {
                // Ŭ���� ����
                if(_pressed == true)
                {
                    MouseAction.Invoke(Define.MouseEvent.Click);
                }
                _pressed = false;
            }
            /* �巡�� ���� ó���� ���߿� �߰��� �� */
        }

        // Ű���� �Է�
        if(KeyAction != null)
        {
            KeyAction.Invoke();
        }
    }
    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
