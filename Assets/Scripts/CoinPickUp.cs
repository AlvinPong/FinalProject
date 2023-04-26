using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    protected GameManager _gameManager;

    public int CoinScore = 1;
    private ScoreManager _scoreManager;

    public AudioClip CoinSoundEffect;
    private AudioSource _audioSource;
    void Start()
    {
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {
            if (_gameManager == null)
                return;
            _gameManager.CoinsCollected++;
            _scoreManager.AddScore(CoinScore);

            if (_audioSource != null && CoinSoundEffect != null)
            {
                _audioSource.PlayOneShot(CoinSoundEffect);
            }
            //Debug.Log("Coin Picked Up " + _scoreManager.CoinAmount);
            Invoke("Death", 0.1f);
        }
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
}
