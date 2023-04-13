using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public LayerMask _layerMask;
    protected float _Mass;
    public float ResetMass = 1f;
    public float _resetMass = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _Mass = _rigidbody.mass;
    }

    // Update is called once per frame
    void Update()
    {
        MassReset();
        if (ResetMass <= 0)
        {
            _rigidbody.mass = _Mass;
            ResetMass = _resetMass;
        }
    }
    private void MassReset()
    {
        ResetMass -= Time.deltaTime;
    }
}
