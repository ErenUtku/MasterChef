using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace UI
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private int itemValue;

        private TextMeshProUGUI itemValueText;
        private Button itemBtn;

        private void Start()
        {
            itemBtn = GetComponent<Button>();
            itemBtn.onClick.AddListener(RemoveCoin);
            itemValueText = GetComponentInChildren<TextMeshProUGUI>();
            itemValueText.text = ("COST\n" + itemValue);

            MenuUIManager.onCoinSpend += CheckAvailability;

            CheckAvailability();
        }

        private void OnDestroy()
        {
            MenuUIManager.onCoinSpend -= CheckAvailability;
        }

        private void CheckAvailability()
        {
            if (Storage.PlayerPrefsController.GetTotalCurrency() < itemValue)
            {
                itemBtn.interactable = false;
            }
        }

        private void RemoveCoin()
        {
            UIManager.instance.RemoveCoin(itemValue);
            MenuUIManager.Instance.UpdateCurrency();
            MenuUIManager.Instance.UICoinSpend();
        }
    }
}
