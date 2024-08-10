using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public TMP_Text coinText;  // Using TextMeshPro for UI display

    // Private field with a public property
    private int coinCount;
    public int CoinCount {
        get { return coinCount; }
        set {
            coinCount = value;
            UpdateCoinText();  // Update the UI whenever the coin count changes
        }
    }

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
        CoinCount = 100;  // Starting coins, adjust as needed
    }

    public void AddCoins(int amount)
    {
        CoinCount += amount;  // Set through property to update the UI automatically
    }

    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + CoinCount;
    }
}
