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

            BringOldReceiptData();

            var jsonString = JsonConvert.SerializeObject(receiptData);

            PlayerPrefs.SetString("Save Data", jsonString);
        }

        private void BringOldReceiptData()
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
    }
}