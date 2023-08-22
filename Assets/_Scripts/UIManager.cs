using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mainMenupanel, winPanel, loosePanel, gamePanel, settingsPanel,shopPanel,leaderboardPanel;
    public PlatformGeneration platformGeneration;
    public CurrencyManager currencyManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void PlayButton()
    {
        mainMenupanel.SetActive(false);
        gamePanel.SetActive(true);
        PlayerController.instance.canMove = true;
        currencyManager.TempCoins = 0;
    }
    public void ReplayButton()
    {
        loosePanel.SetActive(false);
        PlayerController.instance.canMove = true;
        gamePanel.SetActive(true);
        platformGeneration.PlatformGenerator();
        currencyManager.TempCoins = 0;
        currencyManager.inGameTotalCoinsText.text = currencyManager.TempCoins.ToString();
    }
    public void ContinueButton()
    {
        winPanel.SetActive(false);
        PlayerController.instance.canMove = true;
        platformGeneration.PlatformGenerator();
        currencyManager.TempCoins = 0;
        currencyManager.inGameTotalCoinsText.text = currencyManager.TempCoins.ToString();
        gamePanel.SetActive(true);
    }
    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
    }
    public void ShopButton()
    {
        shopPanel.SetActive(true);
    }
    public void LeaderboardButton()
    {
        leaderboardPanel.SetActive(true);
    }

}
