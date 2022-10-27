using System.Collections.Generic;
using Level;
using Newtonsoft.Json;
using Order;
using UnityEngine;

namespace Storage
{
    public class StoreReceiptData : MonoBehaviour
    {
        [Header("ReceiptData")]
        [SerializeField] private List<Receipt> allDefaultReceipts;
        
        [HideInInspector] public List<Receipt> allReceiptsData;
        [HideInInspector] public Receipt receiptData;

        public static StoreReceiptData instance;
        private void Awake()
        {
            instance = this;

            LevelManager.onLevelComplete += BringReceiptDataAndDelete;

            DeserializeData();

            var jsonString = JsonConvert.SerializeObject(allReceiptsData);

            PlayerPrefs.SetString("Save Data", jsonString);

            receiptData = allReceiptsData[0];
        }

        private void DeserializeData()
        {
            var jsonString = PlayerPrefs.GetString("Save Data");

            var data = JsonConvert.DeserializeObject<List<Receipt>>(jsonString);

            if (jsonString == "")
            {
                var dataDefaultSerialize = JsonConvert.SerializeObject(allDefaultReceipts);

                var data2 = JsonConvert.DeserializeObject<List<Receipt>>(dataDefaultSerialize);

                allReceiptsData = data2;
                
                return;
            }

            allReceiptsData = null;
            allReceiptsData = data;
            allDefaultReceipts = data;
        }

        #region EVENT

        private void BringReceiptDataAndDelete()
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
                LevelManager.onLevelReceiptComplete.Invoke();
                return;
            }

            receiptData = allReceiptsData[0];

            LevelManager.onLevelReceiptComplete.Invoke();
        }

        #endregion
    }
}