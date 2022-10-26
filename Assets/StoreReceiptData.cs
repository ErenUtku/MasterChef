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
        [SerializeField] private List<Receipt> allDefaultReceipts;
        private List<Receipt> allReceiptsData;
        public Receipt receiptData;

        public static StoreReceiptData instance;
        private void Awake()
        {
            instance = this;

            LevelManager.OnLevelComplete += BringReceiptDataAndDelete;

            GameStartData();

            var jsonString = JsonConvert.SerializeObject(allReceiptsData);

            PlayerPrefs.SetString("Save Data", jsonString);

            receiptData = allReceiptsData[0];
        }

        private void GameStartData()
        {
            var jsonString = PlayerPrefs.GetString("Save Data");

            var data = JsonConvert.DeserializeObject<List<Receipt>>(jsonString);

            if (jsonString == "")
            {
                var dataDefaultSErialize = JsonConvert.SerializeObject(allDefaultReceipts);

                var data2 = JsonConvert.DeserializeObject<List<Receipt>>(dataDefaultSErialize);

                allReceiptsData = data2;
                
                return;
            }

            allReceiptsData = null;
            allReceiptsData = data;
            allDefaultReceipts = data;
        }

        #region EVENT

        public void BringReceiptDataAndDelete()
        {
            var jsonString = PlayerPrefs.GetString("Save Data");

            var data = JsonConvert.DeserializeObject<List<Receipt>>(jsonString);

            allReceiptsData = data;
            allDefaultReceipts = data;

            PlayerPrefs.DeleteKey("Save Data");
        }

        public void GoToNextReceipt()
        {
            allReceiptsData.RemoveAt(0);

            if (allReceiptsData.Count == 0)
            {
                LevelManager.OnLevelReceiptComplete.Invoke();
                return;
            }

            receiptData = allReceiptsData[0];

            LevelManager.OnLevelReceiptComplete.Invoke();
        }

        #endregion
    }
}