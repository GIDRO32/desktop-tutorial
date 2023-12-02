using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public AudioSource Menu_Source;
    public GameObject settings;
    public GameObject shop;
    public GameObject play;
    public GameObject SetButton;
    public GameObject ShopButton;
    public GameObject PlayButton;
    public Text coinText; // Reference to the UI text displaying coins
    private int A1_Price;
    private int A2_Price;
    private int A1_Num = 3;
    private int A2_Num = 3;
    public Text A1_Q;
    public Text A2_Q;
    public Text A1_P;
    public Text A2_P;
    int balance;

    // Start is called before the first frame update
    void Start()
    {
        Menu_Source = GetComponent<AudioSource>();
        settings.SetActive(false);
        shop.SetActive(false);
        play.SetActive(false);
        // Get the coin count from PlayerPrefs
        int earnings = PlayerPrefs.GetInt("Earnings", 0);
        balance = PlayerPrefs.GetInt("Coins", balance);
        A1_Price = PlayerPrefs.GetInt("A1 Price", A1_Price);
        A2_Price = PlayerPrefs.GetInt("A2 Price", A2_Price);
        A1_Num = PlayerPrefs.GetInt("A1 Num", A1_Num);
        A2_Num = PlayerPrefs.GetInt("A2 Num", A2_Num);
        balance = balance + earnings;
        PlayerPrefs.SetInt("Coins", balance);
        UpdateCoinText(balance);
    }

    public void settingsPanel()
    {
        settings.SetActive(true);
        ShopButton.SetActive(false);
        PlayButton.SetActive(false);
        SetButton.SetActive(false);
    }
    public void buySize()
    {
        if (balance >= A1_Price)
        {
            balance -= A1_Price;
            A1_Price += 15;
            A1_Num++;

            // Save updated values to PlayerPrefs
            PlayerPrefs.SetInt("Coins", balance);
            PlayerPrefs.SetInt("A1 Price", A1_Price);
            PlayerPrefs.SetInt("A1 Num", A1_Num);
        }
    }
public void buySpeed()
    {
        if (balance >= A2_Price)
        {
            balance -= A2_Price;
            A2_Price += 15;
            A2_Num++;

            // Save updated values to PlayerPrefs
            PlayerPrefs.SetInt("Coins", balance);
            PlayerPrefs.SetInt("A2 Price", A2_Price);
            PlayerPrefs.SetInt("A2 Num", A2_Num);
        }
    }

        public void shopPanel()
    {
        shop.SetActive(true);
        ShopButton.SetActive(false);
        PlayButton.SetActive(false);
        SetButton.SetActive(false);
        A1_P.text = A1_Price.ToString();
        A2_P.text = A2_Price.ToString();
        A1_Q.text = "You have: " + A1_Num;
        A2_Q.text = "You have: " + A2_Num;
    }

        public void closeShopPanel()
    {
        shop.SetActive(false);
        ShopButton.SetActive(true);
        PlayButton.SetActive(true);
        SetButton.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        settings.SetActive(false);
        ShopButton.SetActive(true);
        PlayButton.SetActive(true);
        SetButton.SetActive(true);
    }
        public void PlayMode()
    {
        play.SetActive(true);
        ShopButton.SetActive(false);
        PlayButton.SetActive(false);
        SetButton.SetActive(false);
    }
        public void ClosePlayMode()
    {
        play.SetActive(false);
        ShopButton.SetActive(true);
        PlayButton.SetActive(true);
        SetButton.SetActive(true);
    }
    void UpdateCoinText(int coins)
    {
        // Update the UI text displaying coins
        coinText.text = coins.ToString();
    }
    public void resetData()
    {
        A1_Price = 10;
        A2_Price = 10;
        A1_Num = 3;
        A2_Num = 3;
    }
}
