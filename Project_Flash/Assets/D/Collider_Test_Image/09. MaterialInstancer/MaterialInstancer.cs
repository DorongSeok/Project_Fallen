using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialInstancer : MonoBehaviour
{
    private MeshRenderer _MeshRenderer;

    [SerializeField]
    private Color _Color;

    void Start()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
        _MeshRenderer.material = Instantiate(_MeshRenderer.material);
        _MeshRenderer.material.SetColor("_Color", _Color);
    }
}
