using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int CoinAmount;
    public Text CoinText;

    //public int TargetScore = 10;
    //public string Scene = "";

    protected GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CoinText == null)
            return;
        //CoinText.text = CoinAmount.ToString();
        CoinText.text = _gameManager.CoinsCollected.ToString();
    }
    public void AddScore(int AddAmount)
    {
        CoinAmount += AddAmount;
        
        //if (CoinAmount >= TargetScore)
        //{
        //    _gameManager.LoadScene();
        //}
    }
}
