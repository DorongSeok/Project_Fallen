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
        // 키보드나 마우스 입력이 없었으면 바로 리턴
        if(Input.anyKey == false)
        {
            return;
        }

        // 마우스 입력
        if(MouseAction != null)
        {
            // 마우스를 누르고 있으면
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            // 마우스를 누르지 않고 있으면
            else
            {
                // 클릭시 실행
                if(_pressed == true)
                {
                    MouseAction.Invoke(Define.MouseEvent.Click);
                }
                _pressed = false;
            }
            /* 드래그 관련 처리는 나중에 추가할 것 */
        }

        // 키보드 입력
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
