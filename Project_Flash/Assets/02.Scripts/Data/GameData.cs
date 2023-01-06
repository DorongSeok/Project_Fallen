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

        public GameData() // 생성자(초기값 선언)
        {
            _savePos = "0,0,0";
            _velocity = "0,0,0";
            _gravityScale = 0.0f;
            _linearDrag = 10.0f;
            _isFallen = false;
            _isMove = false;
        }

        // 추가적인 데이터 저장 필요 시 추가하고 생성자에도 추가할 것
    }
}
