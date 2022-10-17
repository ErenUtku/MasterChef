using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Order;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orderName;

    [Header("Receipt")]
    [SerializeField] private GameObject receiptFolder;
    [SerializeField] private GameObject ingredientPrefab;

    private Receipt levelReceipt;
    private void Start()
    {
        levelReceipt = LevelFacade.instance.levelReceipt;
        InstantiateReceipt(levelReceipt);
        orderName.text = levelReceipt.receiptName;
    }

    private void InstantiateReceipt(Order.Receipt order)
    {
        foreach (var item in order.ingredients)
        {
            var Ingredient = Instantiate(ingredientPrefab, receiptFolder.transform);
            Ingredient.GetComponent<Image>().sprite = item.ingredient.ingredientSprite;
            Ingredient.GetComponentInChildren<TextMeshProUGUI>().text = ($"x" +item.amount.ToString());
        }
    }

}
