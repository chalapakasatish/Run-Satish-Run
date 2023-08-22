using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField]private int coins;
    [SerializeField] private int tempCoins;
    public Text inGameTotalCoinsText,mainMenuTotalCoinsText,levelCompleteCoinText,levelFailedTotalCoinsText;

    public int Coins { get => coins; set => coins = value; }
    public int TempCoins { get => tempCoins; set => tempCoins = value; }

    private void Start()
    {
        Coins = PlayerPrefs.GetInt("Coins",Coins);
        TempCoins = 0;
        inGameTotalCoinsText.text = TempCoins.ToString();
        mainMenuTotalCoinsText.text = Coins.ToString();
    }
    public void AddCoins(int num)
    {
        TempCoins += num;
        inGameTotalCoinsText.text = TempCoins.ToString();
        Coins += num;
        PlayerPrefs.SetInt("Coins", Coins);
    }
    public void RewardAdAddCoins(int num)
    {
        Coins += num;
        PlayerPrefs.SetInt("Coins", Coins);
        mainMenuTotalCoinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
