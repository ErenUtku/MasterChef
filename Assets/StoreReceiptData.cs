using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using Order;

namespace Data
{
    public class StoreReceiptData : MonoBehaviour
    {
        [Header("ReceiptData")]
        [SerializeField] private Receipt defaultReceipt;
        [HideInInspector] public Receipt receiptData;

        public static StoreReceiptData instance;
        private void Awake()
        {
            instance = this;

            GameStartData();

            var jsonString = JsonConvert.SerializeObject(receiptData);

            PlayerPrefs.SetString("Save Data", jsonString);
            
        }

        private void GameStartData()
        {
            var jsonString = PlayerPrefs.GetString("Save Data");

            var data = JsonConvert.DeserializeObject<Receipt>(jsonString);

            if (jsonString == "")
            {
                receiptData = defaultReceipt;
                return;
            }
            
            receiptData = null;
            receiptData = data;
            defaultReceipt = data;
        }

        private void OnDestroy()
        {
            var jsonString = PlayerPrefs.GetString("Save Data");
            Debug.Log(jsonString);
            BringReceiptDataAndDelete();
        }

        private void BringReceiptDataAndDelete()
        {
            var jsonString = PlayerPrefs.GetString("Save Data");

            var data = JsonConvert.DeserializeObject<Receipt>(jsonString);

            receiptData = data;
            

        }
    }
}