using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string NextSceneName = "";
    public string CurrentSceneName = "";
    public string MainMenuScene = "";

    public int CoinTarget = 0;
    public int CoinsCollected = 0;

    public int SavedCoins = 0;

    public GameObject Button;
    //public void GoToNextLevel()
    //{
    //    SaveGame();

    //    if (NextSceneName == "")
    //        return;
    //    SceneManager.LoadScene(NextSceneName);
    //}
    public void LoadScene()
    {
        SaveGame();

        if (NextSceneName == "")
            return;
        SceneManager.LoadScene(NextSceneName);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("SavedCoinsCollected", CoinsCollected);
        PlayerPrefs.Save();
    }
    public void LoadGame()
    {
        CoinsCollected = PlayerPrefs.GetInt("SavedCoinsCollected");
    }
    public void StartNewGame()
    {
        CoinsCollected = 0;
        PlayerPrefs.SetInt("SavedCoinsCollected", CoinsCollected);
    }
    private void Start()
    {
        LoadGame();
        if (Button == null) 
            return;
        Button.SetActive(false);
        
    }
    private void Update()
    {
        if (Button == null) return;
        if (CoinsCollected >= CoinTarget)
        {
            Button.SetActive(true);
        }
        else
        {
            Button.SetActive(false);
        }
    }
    public void Retry()
    {
        if (CurrentSceneName == "")
            return;
        SceneManager.LoadScene(CurrentSceneName);
    }
    public void MainMenu()
    {
        SaveGame();
        if (MainMenuScene == "")
            return;
        SceneManager.LoadScene(MainMenuScene);
    }
}
