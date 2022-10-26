using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuUIManager : MonoBehaviour
{
    [Header("Shop Buttons & Panel")]
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Button ShopExitBtn;
    [SerializeField] private GameObject ShopPanel;

    [Header("Home Buttons & Panel")]
    [SerializeField] private Button HomeBtn;

    [Header("Settings Buttons & Panel")]
    [SerializeField] private Button SettingBtn;

    [Space]
    [SerializeField] private Button PlayBtn;

    private void Start()
    {
        //SHOP
        ShopBtn.onClick.AddListener(() => OpenShopMenu(true));
        ShopExitBtn.onClick.AddListener(() => OpenShopMenu(false));

        //HOME
        HomeBtn.onClick.AddListener(() => OpenHomeMenu(true));

        //Settings
        SettingBtn.onClick.AddListener(() => OpenSettingMenu(true));

        //Play
        PlayBtn.onClick.AddListener(() => StartGame());
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

    }

    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
