using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TMP_Text coinDisplay;
    public Button[] weaponButtons;
    public GameObject[] weaponPrefabs;
    public int[] weaponPrices;
    public GameObject shopPanel;
    public Weapon playerWeapon; // Reference to the player's active weapon

    // Ammo additions
    public Button[] ammoButtons; // Buttons for each type of ammo
    public GameObject[] ammoPrefabs; // Corresponding ammo prefabs
    public int[] ammoPrices; // Prices for each ammo type

    internal bool isShopOpen = false;

    private void Start()
    {
        Debug.Log("ShopManager Start");
        InitializeShop();
        shopPanel.SetActive(isShopOpen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B key pressed - toggling shop");
            ToggleShop();
        }
    }

    private void InitializeShop()
    {
        // Initialize weapon buttons
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            int index = i;
            weaponButtons[i].onClick.AddListener(() => AttemptPurchaseWeapon(index));
        }
        // Initialize ammo buttons
        for (int i = 0; i < ammoButtons.Length; i++)
        {
            int index = i;
            ammoButtons[i].onClick.AddListener(() => AttemptPurchaseAmmo(index));
        }
        UpdateCoinDisplay();
    }

    private void AttemptPurchaseWeapon(int index)
    {
        if (CoinManager.Instance.CoinCount >= weaponPrices[index])
        {
            CoinManager.Instance.AddCoins(-weaponPrices[index]);
            WeaponManager.Instance.PickUpWeapon(weaponPrefabs[index]);
            UpdateCoinDisplay();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    private void AttemptPurchaseAmmo(int index)
    {
        if (CoinManager.Instance.CoinCount >= ammoPrices[index])
        {
            CoinManager.Instance.AddCoins(-ammoPrices[index]);
            GameObject ammoGameObject = Instantiate(ammoPrefabs[index]); // Instantiate to simulate picking it up
            WeaponManager.Instance.PickUpAmmo(ammoGameObject);
            UpdateCoinDisplay();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    private void UpdateCoinDisplay()
    {
        coinDisplay.text = "Coins: " + CoinManager.Instance.CoinCount;
    }

    private void ToggleShop()
    {
        isShopOpen = !isShopOpen;
        shopPanel.SetActive(isShopOpen);

        Cursor.lockState = isShopOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isShopOpen;

        if (playerWeapon != null)
        {
            Debug.Log("Shop open: " + isShopOpen + "; Weapon active: " + playerWeapon.isActiveAndEnabled);
            playerWeapon.isShooting = !isShopOpen;
            playerWeapon.enabled = !isShopOpen;
        }

        // Pause/Resume game time and sound
        Time.timeScale = isShopOpen ? 0 : 1;
        SoundManager.Instance.TogglePause(isShopOpen);
    }
}
