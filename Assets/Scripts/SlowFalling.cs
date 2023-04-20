using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFalling : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    public float GravityScale;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;
        _rigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        _rigidbody.gravityScale = GravityScale;
    }
}
