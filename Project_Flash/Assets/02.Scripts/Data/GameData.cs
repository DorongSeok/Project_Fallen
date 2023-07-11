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
        public bool _isFirstPlay;
        public int _level;
        public int _fallenCount;
        public float _second;
        public bool _isBugCheckerActive;

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
            _isFirstPlay = true;
            _level = 1;
            _fallenCount = 0;
            _second = 0.0f;
            _isBugCheckerActive = false;
        }

        // �߰����� ������ ���� �ʿ� �� �߰��ϰ� �����ڿ��� �߰��� ��
    }
    public class OptionData
    {
        public float _bgmSound;
        public float _sfxSound;
        public bool _isStarted;

        public OptionData()
        {
            _bgmSound = -10.0f;
            _sfxSound = -10.0f;
            _isStarted = false;
        }
    }
}
