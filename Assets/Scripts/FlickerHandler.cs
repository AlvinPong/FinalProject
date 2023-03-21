using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerHandler : MonoBehaviour
{
    public Color Color = Color.red;
    public float Duration = 1f;
    public float Interval = 0.2f;

    private Color _originalColor;

    private Health _health;
    private SpriteRenderer _spriteRenderer;

    private bool _canStartFlicker = false;

    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponentInParent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer != null)
            _originalColor = _spriteRenderer.color;

        if (_health != null)
        {
            _health.OnHit += Flicker;
            _health.OnHitReset += StopFlicker;
        }

    }
    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnHit -= Flicker;
            _health.OnHitReset -= StopFlicker;
        }
    }

    private void Flicker(GameObject source)
    {
        
        _canStartFlicker = true;
        StartCoroutine(DoFlicker());
    }
    private void StopFlicker()
    {
        _canStartFlicker = false;
    }

    IEnumerator DoFlicker()
    {
        float duration = Duration;
        float flickerDuration = Interval;

        Color _flickerColor = Color;

        bool colorFlicker = false;

        while (duration > 0)
        {
            if (flickerDuration <= 0)
            {
                colorFlicker = !colorFlicker;
                flickerDuration = Interval;
            }

            if (colorFlicker)
            {
                _spriteRenderer.color = _flickerColor;
                //Debug.Log("Flicker " + _flickerColor);
            }
            else
            {
                _spriteRenderer.color = _originalColor;
                //Debug.Log("Reverse Flicker " + _originalColor);

            }

            flickerDuration -= Time.deltaTime;
            duration -= Time.deltaTime;
            yield return null;
        }
        _spriteRenderer.color = _originalColor;
    }
}
