using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public AudioSource Menu_SFX;
    public AudioSource Menu_Music;
    public Slider Music_Slider;
    public Slider SFX_Slider;
    public AudioMixer Music_Mixer;
    public AudioMixer SFX_Mixer;
    public GameObject settings;
    public GameObject shop;
    public GameObject play;
    public GameObject SetButton;
    public GameObject ShopButton;
    public GameObject PlayButton;
    public AudioClip Click;
    public AudioClip Cancel;
    public AudioClip ResetSound;
    public AudioClip Purchase;
    public AudioClip Cant_Purchase;
    public Text coinText; // Reference to the UI text displaying coins
    private int A1_Price;
    private int A2_Price;
    private int A1_Num = 3;
    private int A2_Num = 3;
    private int music_volume;
    private int sfx_volume;
    public Text A1_Q;
    public Text A2_Q;
    public Text A1_P;
    public Text A2_P;
    private int balance = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Menu_Music = GetComponent<AudioSource>();
        settings.SetActive(false);
        shop.SetActive(false);
        play.SetActive(false);
        A1_Price = PlayerPrefs.GetInt("A1 Price", A1_Price);
        Music_Slider.value = PlayerPrefs.GetFloat("Volume", 0.75f);
        SFX_Slider.value = PlayerPrefs.GetFloat("SFX", 0.75f);
        A2_Price = PlayerPrefs.GetInt("A2 Price", A2_Price);
        A1_Num = PlayerPrefs.GetInt("A1 Num", A1_Num);
        A2_Num = PlayerPrefs.GetInt("A2 Num", A2_Num);
        //PlayerPrefs.SetInt("Coins", balance);
    }
    // public void setVolume()
    // {
    //     float M_sliderValue = Music_Slider.value;
    //     Music_Mixer.SetFloat("Master", Mathf.Log10(M_sliderValue) * 20);
    //     PlayerPrefs.SetFloat("Volume", M_sliderValue);
    // }
    // public void setSFX()
    // {
    //     float S_sliderValue = SFX_Slider.value;
    //     SFX_Mixer.SetFloat("Master", Mathf.Log10(S_sliderValue) * 20);
    //     PlayerPrefs.SetFloat("SFX", S_sliderValue);
    // }
    public void settingsPanel()
    {
        Menu_SFX.PlayOneShot(Click);
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
            UpdateCoinText(balance);
            UpdateA1(A1_Num, A1_Price);
            Menu_SFX.PlayOneShot(Purchase);
        }
        else
        {
            Menu_SFX.PlayOneShot(Cant_Purchase);
        }
    }
public void buySpeed()
    {
        if (balance >= A2_Price)
        {
            balance -= A2_Price;
            A2_Price += 15;
            A2_Num++;
            UpdateCoinText(balance);
            UpdateA2(A2_Num, A2_Price);
            Menu_SFX.PlayOneShot(Purchase);
        }
        else
        {
            Menu_SFX.PlayOneShot(Cant_Purchase);
        }
    }

        public void shopPanel()
    {
        shop.SetActive(true);
        ShopButton.SetActive(false);
        PlayButton.SetActive(false);
        SetButton.SetActive(false);
        Menu_SFX.PlayOneShot(Click);
        balance = PlayerPrefs.GetInt("Coins", balance);
        PlayerPrefs.GetInt("A1 Price", A1_Price);
        PlayerPrefs.GetInt("A1 Num", A1_Num);
        PlayerPrefs.GetInt("A2 Price", A2_Price);
        PlayerPrefs.GetInt("A2 Num", A2_Num);
        coinText.text = balance.ToString();
        A1_P.text = A1_Price.ToString();
        A2_P.text = A2_Price.ToString();
        A1_Q.text = "You have: " + A1_Num;
        A2_Q.text = "You have: " + A2_Num;
    }

        public void closeShopPanel()
    {
        Menu_SFX.PlayOneShot(Cancel);
        shop.SetActive(false);
        ShopButton.SetActive(true);
        PlayButton.SetActive(true);
        SetButton.SetActive(true);
        PlayerPrefs.SetInt("Coins", balance);
        PlayerPrefs.SetInt("A1 Price", A1_Price);
        PlayerPrefs.SetInt("A1 Num", A1_Num);
        PlayerPrefs.SetInt("A2 Price", A2_Price);
        PlayerPrefs.SetInt("A2 Num", A2_Num);
    }

    public void CloseSettingsPanel()
    {
        Menu_SFX.PlayOneShot(Cancel);
        settings.SetActive(false);
        ShopButton.SetActive(true);
        PlayButton.SetActive(true);
        SetButton.SetActive(true);
    }
        public void PlayMode()
    {
        Menu_SFX.PlayOneShot(Click);
        play.SetActive(true);
        ShopButton.SetActive(false);
        PlayButton.SetActive(false);
        SetButton.SetActive(false);
    }
        public void ClosePlayMode()
    {
        Menu_SFX.PlayOneShot(Cancel);
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
    void UpdateA1(int count_one, int price_one)
    {
        A1_P.text = price_one.ToString();
        A1_Q.text = "You have: " + count_one.ToString();
    }
    void UpdateA2(int count_two, int price_two)
    {
        A2_P.text = price_two.ToString();
        A2_Q.text = "You have: " + count_two.ToString();
    }
    public void resetData()
    {
        Menu_SFX.PlayOneShot(ResetSound);
        balance = 0;
        A1_Price = 10;
        A2_Price = 10;
        A1_Num = 3;
        A2_Num = 3;
    }
}
