using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        public string _savePos; // 0/0/0
        public string _velocity; // 0/0/0
        public float _gravityScale;
        public float _linearDrag;
        public bool _isFallen;
        public bool _isMove;
        public float _duration;
        public bool _isignoreLayerCollision;

        public GameData() // ������(�ʱⰪ ����)
        {
            _savePos = "0,0,0";
            _velocity = "0,0,0";
            _gravityScale = 0.0f;
            _linearDrag = 5.0f;
            _isFallen = false;
            _isMove = false;
            _duration = 0.0f;
            _isignoreLayerCollision = false;
        }

        // �߰����� ������ ���� �ʿ� �� �߰��ϰ� �����ڿ��� �߰��� ��
    }
}
