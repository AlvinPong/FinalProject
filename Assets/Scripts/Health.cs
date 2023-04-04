using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit;

    public delegate void ResetEvent();
    public ResetEvent OnHitReset;

    //public
    public float CurrentHealth
    {
        get { return _currentHealth; }
    }
    public GameObject DeathParticles;

    public bool _canDamage = true;

    public Cooldown Invulnerable;
    //private
    private float _currentHealth = 10f;

    private void Update()
    {
        ResetInvulnerble();
    }
    private void ResetInvulnerble()
    {
        if (_canDamage)
            return;
        if (Invulnerable.IsOnCooldown && _canDamage == false)
            return;
        _canDamage = true;
        OnHitReset?.Invoke();
    }
    public void Damage(float damageAmount, GameObject source)
    {
        if (!_canDamage) 
            return;
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0)
        {
            Die();
        }

        Invulnerable.StartCooldown();
        _canDamage = false;

        OnHit?.Invoke(source);
    }
    public void Die()
    {
        //Debug.Log("You Died");
        GameObject.Instantiate(DeathParticles,transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
