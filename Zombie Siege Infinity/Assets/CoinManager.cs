using UnityEngine;
using TMPro; // Add this line to use TextMeshPro

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public TMP_Text coinText; // Change from Text to TMP_Text
    private int coinCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        coinCount = 0;
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + coinCount.ToString();
    }
}
