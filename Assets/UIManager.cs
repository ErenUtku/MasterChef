using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Order;
using TMPro;
using Data;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject levelEndUI;

    [SerializeField] private TextMeshProUGUI orderName;
    [SerializeField] private IngredientsSpriteManager spriteManager;

    [Header("Receipt")]
    [SerializeField] private GameObject receiptFolder;
    [SerializeField] private GameObject ingredientPrefab;

    private Receipt levelReceipt;

    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void SetReceiptUI()
    {
        levelReceipt = StoreReceiptData.instance.receiptData;
        InstantiateReceipt(levelReceipt);
        orderName.text = levelReceipt.receiptName;
    }

    private void InstantiateReceipt(Receipt order)
    {
        foreach (var item in order.ingredients)
        {
            var Ingredient = Instantiate(ingredientPrefab, receiptFolder.transform);
    
            Ingredient.GetComponent<Image>().sprite = spriteManager.CheckIngredient2DSprite(item.ingredientData);

            Ingredient.GetComponentInChildren<TextMeshProUGUI>().text = ($"x" +item.amount.ToString());
        }
    }

    public void LevelEndScreen(bool value)
    {
        levelEndUI.SetActive(value);
    }

}
