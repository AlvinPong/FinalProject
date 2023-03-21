using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateCoin : MonoBehaviour
{
    public GameManager _gameManager;
    public TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        if (_gameManager == null || text == null)
            return;

        text.text = "x " + _gameManager.CoinsCollected.ToString();
    }
}
