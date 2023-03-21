using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float Damage = 1f;
    public LayerMask TargetLayerMask;

    private void OnTriggerEnter2D(Collider2D col)
    {
        DealDamage(col);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        DealDamage(col);
    }

    
    void DealDamage(Collider2D col)
    {
        if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;

        Health targetHealth = col.gameObject.GetComponent<Health>();

        if (targetHealth == null)
            return;
        targetHealth.Damage(Damage, transform.gameObject);
    }
}
