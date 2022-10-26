using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Order;
using TMPro;
using Data;
using Storage;
using Controllers;
public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject levelWinUI;
    [SerializeField] private GameObject levelLoseUI;

    [Header("Coin")]
    [SerializeField] private GameObject coinSprite;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Level")]
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Order")]
    [SerializeField] private TextMeshProUGUI orderName;
    [SerializeField] private IngredientsSpriteManager orderSpriteManager;

    [Header("Receipt")]
    [SerializeField] private GameObject receiptFolder;
    [SerializeField] private GameObject ingredientPrefab;

    
    private Receipt levelReceipt;

    public static UIManager instance;
    private void Awake()
    {
        instance = this;
        LevelManager.OnLevelLoad += SetReceiptUI;
        LevelManager.OnLevelLoad += GetLevelCount;
        LevelManager.OnLevelFail += ShowLevelLoseScreen;
        LevelManager.OnLevelStageComplete += ShowLevelWinScreen;
    }

    private void OnDestroy()
    {
        LevelManager.OnLevelLoad -= SetReceiptUI;
        LevelManager.OnLevelLoad -= GetLevelCount;
        LevelManager.OnLevelFail -= ShowLevelLoseScreen;
        LevelManager.OnLevelStageComplete -= ShowLevelWinScreen;
    }

    private void Start()
    {     
        GetTotalCurrency();
    }


    

    private void InstantiateReceipt(Receipt order)
    {
        foreach (var item in order.ingredients)
        {
            var Ingredient = Instantiate(ingredientPrefab, receiptFolder.transform);
    
            Ingredient.GetComponent<Image>().sprite = orderSpriteManager.CheckIngredient2DSprite(item.ingredientData);

            Ingredient.GetComponentInChildren<TextMeshProUGUI>().text = ($"x" +item.amount.ToString());
        }
    }

   

    public void UpdateTimer(string value)
    {
        timerText.text = value;
    }


    public void AddCoin(int coinCount)
    {
      
        var totalCoin = PlayerPrefsController.GetTotalCurrency();

        totalCoin += coinCount;

        PlayerPrefsController.SetCurrency(totalCoin);

        coinText.text = totalCoin.ToString();

        /*coinIcon.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            coinIcon.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBounce);
        });*/
      
    }

    public void RemoveCoin(int coinCount)
    {
        var totalCoin = PlayerPrefsController.GetTotalCurrency();

        totalCoin -= coinCount;

        PlayerPrefsController.SetCurrency(totalCoin);

        coinText.text = totalCoin.ToString();

        /*coinIcon.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            coinIcon.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBounce);
        });*/
        
    }

    public void GetTotalCurrency()
    {
        coinText.text = PlayerPrefsController.GetTotalCurrency().ToString();
    }

    public Transform GetCoinPos()
    {
        return coinSprite.transform;
    }

#region EVENTS

    public void GetLevelCount()
    {
        levelText.text = $"LEVEL {LevelFacade.instance.levelNumber.ToString()}";
    }

    public void ShowLevelWinScreen()
    {
        levelWinUI.SetActive(true);
    }

    public void SetReceiptUI()
    {
        levelReceipt = StoreReceiptData.instance.receiptData;
        InstantiateReceipt(levelReceipt);
        orderName.text = levelReceipt.receiptName;
    }

    public void ShowLevelLoseScreen()
    {
        levelLoseUI.SetActive(true);
    }

    #endregion

}
