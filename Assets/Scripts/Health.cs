using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public AudioClip DamageSoundEffect;
    public AudioClip DeathSoundEffect;
    private AudioSource _audioSource;

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
    public float _currentHealth = 10f;

    public bool IsDamaged
    {
        get { return _IsDamaged; }
    }
    protected bool _IsDamaged = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
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
        _IsDamaged = false;
        OnHitReset?.Invoke();
    }
    public void Damage(float damageAmount, GameObject source)
    {
        if (!_canDamage)
            return;
        _currentHealth -= damageAmount;
        _IsDamaged = true;
        if (_currentHealth <= 0)
        {
            Die();
        }
        if (_audioSource != null && DamageSoundEffect != null)
        {
            _audioSource.PlayOneShot(DamageSoundEffect);
        }

        Invulnerable.StartCooldown();
        _canDamage = false;

        OnHit?.Invoke(source);
    }
    public void Die()
    {
        //Debug.Log("You Died");
        if (DeathParticles == null)
        {
            Destroy(this.gameObject);
            return;
        }
        GameObject.Instantiate(DeathParticles, transform.position, transform.rotation);
        if (_audioSource != null && DeathSoundEffect != null)
        {
            _audioSource.PlayOneShot(DeathSoundEffect);
        }
        Invoke("Destroy", 0.01f);
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
