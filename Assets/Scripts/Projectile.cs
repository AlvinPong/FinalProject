using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage = 1f;
    public float Speed = 10f;
    public float PushForce = 10f;
    public Cooldown LifeTime;
    public LayerMask TargetLayerMask;

    public Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        LifeTime.StartCooldown();
        _rigidbody.AddRelativeForce(new Vector2(Speed, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (LifeTime.CurrentProgress != Cooldown.Progress.Finished)
            return;

        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;
        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();
        
        if (targetRigidbody == null)
            return;
        if (targetRigidbody != null)
        {
            targetRigidbody.mass = 1f;
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }

        Health targetHealth = col.gameObject.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.Damage(Damage, gameObject);
        }

        Die();
    }
}
