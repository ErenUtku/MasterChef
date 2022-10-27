using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Storage;
public class MenuUIManager : MonoBehaviour
{
    [Header("Shop Buttons & Panel")]
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Button ShopExitBtn;

    [Header("Settings Buttons & Panel")]
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private Button SettingBtn;
    [SerializeField] private Button SettingExitBtn;
    [SerializeField] private Button AudioBtn;
    [SerializeField] private Button MusicBtn;
    [SerializeField] private GameObject audioOffImage;
    [SerializeField] private GameObject musicOffImage;
 
    [Header("Home Buttons & Panel")]
    [SerializeField] private Button HomeBtn;

    [Space]
    [SerializeField] private Button PlayBtn;

    [Header("Level & Currency")]
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private TextMeshProUGUI currencyTxt;

    private int audioClickTime = 0;
    private int musicClickTime = 0;

    private void Start()
    {
        //SHOP
        ShopBtn.onClick.AddListener(() => OpenShopMenu(true));
        ShopExitBtn.onClick.AddListener(() => OpenShopMenu(false));

        //HOME
        HomeBtn.onClick.AddListener(() => OpenHomeMenu(true));

        //Settings
        SettingBtn.onClick.AddListener(() => OpenSettingMenu(true));
        SettingExitBtn.onClick.AddListener(() => OpenSettingMenu(false));
        AudioBtn.onClick.AddListener(() => TurnOffAudio());
        MusicBtn.onClick.AddListener(() => TurnOffMusic());

        //Play
        PlayBtn.onClick.AddListener(() => StartGame());

        //Level
        levelTxt.text = "LEVEL\n" + PlayerPrefsController.GetLevelNumber().ToString();
        UpdateCurrency();
    }

    private void OpenShopMenu(bool value)
    {
        ShopPanel.SetActive(value);
    }

    private void OpenHomeMenu(bool value)
    {
        
    }

    private void OpenSettingMenu(bool value)
    {
        SettingPanel.SetActive(value);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void TurnOffAudio()
    {
        audioClickTime++;
        if (audioClickTime % 2 != 0)
        {
            audioOffImage.SetActive(true);
        }
        else
        {
            audioOffImage.SetActive(false);
        }

        SoundManager.instance.ToggleEffects();
    }

    private void TurnOffMusic()
    {
        musicClickTime++;
        if (musicClickTime % 2 != 0)
        {
            musicOffImage.SetActive(true);
        }
        else
        {
            musicOffImage.SetActive(false);
        }

        SoundManager.instance.ToggleMusic();
    }


    public void UpdateCurrency()
    {
        currencyTxt.text = "    : " + PlayerPrefsController.GetTotalCurrency().ToString();
    }
   
}
