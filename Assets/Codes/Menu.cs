using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    //others:
    public AudioSource Menu_SFX;
    public AudioSource Menu_Music;
    public Slider Music_Slider;
    public Slider SFX_Slider;
    public AudioMixer Music_Mixer;
    public AudioMixer SFX_Mixer;
    //Game Objects:
    public GameObject Next_Button;
    public GameObject Prev_Button;
    public GameObject settings;
    public GameObject shop;
    public GameObject page_one;
    public GameObject page_two;
    public GameObject play;
    public GameObject SetButton;
    public GameObject ShopButton;
    public GameObject PlayButton;
    public GameObject BG2_Lock;
    public GameObject BG3_Lock;
    //Audio Clips:
    public AudioClip Click;
    public AudioClip Cancel;
    public AudioClip ResetSound;
    public AudioClip Purchase;
    public AudioClip Cant_Purchase;
    //Texts:
    public Text coinText; // Reference to the UI text displaying coins
    public Text A1_Q;
    public Text A2_Q;
    public Text A1_P;
    public Text A2_P;
    public Text BG1_State;
    public Text BG2_State;
    public Text BG3_State;
    public Text BG2_Price_text;
    public Text BG3_Price_text;
    //Ints:
    private int BG_Index = 0;
    private int A1_Price;
    private int A2_Price;
    private int BG2_Price = 100;
    private int BG3_Price = 200;
    private int A1_Num = 3;
    private int A2_Num = 3;
    private int music_volume;
    private int sfx_volume;
    private int balance = 0;
    private int BG2_Available = 0;
    private int BG3_Available = 0;


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
        BG2_Available = PlayerPrefs.GetInt("BG2", BG2_Available);
        BG3_Available = PlayerPrefs.GetInt("BG3", BG3_Available);
        //PlayerPrefs.SetInt("Coins", balance);
    }
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
        page_two.SetActive(false);
        page_one.SetActive(true);
        Next_Button.SetActive(true);
        Prev_Button.SetActive(true);
        shop.SetActive(true);
        ShopButton.SetActive(false);
        PlayButton.SetActive(false);
        SetButton.SetActive(false);
        Menu_SFX.PlayOneShot(Click);
        balance = PlayerPrefs.GetInt("Coins", balance);
    }
    public void NextPage()
    {
        coinText.text = balance.ToString();
        page_two.SetActive(true);
        page_one.SetActive(false);
        PlayerPrefs.GetInt("BG2", BG2_Available);
        PlayerPrefs.GetInt("BG3", BG3_Available);
        Menu_SFX.PlayOneShot(Click);
        if(BG2_Available == 0)
        {
            BG2_Lock.SetActive(true);
        }
        else
        {
            BG2_Lock.SetActive(false);
        }
        if(BG3_Available == 0)
        {
            BG3_Lock.SetActive(true);
        }
        else
        {
            BG3_Lock.SetActive(false);
        }
        if(BG_Index == 0)
        {
            BG1_State.text = "In Use";
            if(BG2_Available == 0)
            {
                BG2_State.text = "Buy";
            }
            else
            {
                BG2_State.text = "Use";
            }
            if(BG3_Available == 0)
            {
                BG2_State.text = "Buy";
            }
            else
            {
                BG3_State.text = "Use";
            }
        }
        if(BG_Index == 1)
        {
            BG2_State.text = "In Use";
            if(BG3_Available == 0)
            {
                BG3_State.text = "Buy";
            }
            else
            {
                BG3_State.text = "Use";
            }
            BG1_State.text = "Use";
        }
        if(BG_Index == 2)
        {
            BG3_State.text = "In Use";
            if(BG2_Available == 0)
            {
                BG2_State.text = "Buy";
            }
            else
            {
                BG2_State.text = "Use";
            }
            BG1_State.text = "Use";
        }
    }
        public void PrevPage()
    {
        page_two.SetActive(false);
        page_one.SetActive(true);
        PlayerPrefs.GetInt("A1 Price", A1_Price);
        PlayerPrefs.GetInt("A1 Num", A1_Num);
        PlayerPrefs.GetInt("A2 Price", A2_Price);
        PlayerPrefs.GetInt("A2 Num", A2_Num);
        coinText.text = balance.ToString();
        A1_P.text = A1_Price.ToString();
        A2_P.text = A2_Price.ToString();
        A1_Q.text = "You have: " + A1_Num;
        A2_Q.text = "You have: " + A2_Num;
        Menu_SFX.PlayOneShot(Click);
    }
    public void Choose_BG1()
    {
        if(BG_Index != 0)
        {
            Menu_SFX.PlayOneShot(Click);
            BG_Index = 0;
        }
        else if(BG_Index == 0)
        {
            Menu_SFX.PlayOneShot(Click);
        }
    }
    public void Choose_BG2()
    {
        if(BG_Index != 1 && BG2_Available == 0 && balance <= BG2_Price)
        {
            Menu_SFX.PlayOneShot(Cant_Purchase);
        }
        else if(BG_Index != 1 && BG2_Available == 0 && balance >= BG2_Price)
        {
            Menu_SFX.PlayOneShot(Purchase);
            coinText.text = balance.ToString();
            balance -= BG2_Price;
            BG2_Available = 1;
        }
        else if(BG_Index != 1 && BG2_Available == 1)
        {
            BG_Index = 1;
            Menu_SFX.PlayOneShot(Click);
        }
        else if(BG_Index == 1)
        {
            Menu_SFX.PlayOneShot(Click);
        }
    }
    public void Choose_BG3()
    {
        if(BG_Index != 2 && BG3_Available == 0 && balance <= BG3_Price)
        {
            Menu_SFX.PlayOneShot(Cant_Purchase);
        }
        else if(BG_Index != 2 && BG3_Available == 0 && balance >= BG3_Price)
        {
            coinText.text = balance.ToString();
            Menu_SFX.PlayOneShot(Purchase);
            balance -= BG3_Price;
            BG3_Available = 1;
        }
        else if(BG_Index != 2 && BG3_Available == 1)
        {
            BG_Index = 2;
            Menu_SFX.PlayOneShot(Click);
        }
        else if(BG_Index == 2)
        {
            Menu_SFX.PlayOneShot(Click);
        }
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
        PlayerPrefs.SetInt("BG2", BG2_Available);
        PlayerPrefs.SetInt("BG3", BG3_Available);
        PlayerPrefs.SetInt("BG Play", BG_Index);
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
        BG2_Available = 0;
        BG3_Available = 0;
        BG_Index = 0;
    }
}
