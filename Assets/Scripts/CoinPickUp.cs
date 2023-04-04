using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    protected GameManager _gameManager;

    public int CoinScore = 1;
    private ScoreManager _scoreManager;
    void Start()
    {
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {
            if (_gameManager == null)
                return;
            _gameManager.CoinsCollected++;
            _scoreManager.AddScore(CoinScore);
            //Debug.Log("Coin Picked Up " + _scoreManager.CoinAmount);
            Destroy(gameObject);
        }
        
        
    }
}
