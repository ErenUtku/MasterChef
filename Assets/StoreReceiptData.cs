using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using Order;
using Controllers;
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

            LevelManager.OnLevelComplete += BringReceiptDataAndDelete;

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
                var dataDefaultSErialize = JsonConvert.SerializeObject(defaultReceipt);

                var data2 = JsonConvert.DeserializeObject<Receipt>(dataDefaultSErialize);

                receiptData = data2;
                
                return;
            }
            
            receiptData = null;
            receiptData = data;
            defaultReceipt = data;
        }

        #region EVENT

        public void BringReceiptDataAndDelete()
        {
            var jsonString = PlayerPrefs.GetString("Save Data");

            var data = JsonConvert.DeserializeObject<Receipt>(jsonString);

            receiptData = data;
            defaultReceipt = data;

            PlayerPrefs.DeleteKey("Save Data");
        }

        #endregion
    }
}