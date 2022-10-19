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
        public Receipt receiptData;
        private Receipt defaultReceipt;

        public static StoreReceiptData instance;
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Deseralize();

            var jsonString = JsonConvert.SerializeObject(receiptData);

            PlayerPrefs.SetString("Save Data", jsonString);
        }


        public void SaveData()
        {
            //DO no update Level Count
            var jsonString = JsonConvert.SerializeObject(receiptData);
            //Debug.Log(jsonString);
            PlayerPrefs.SetString("Save Data", jsonString);
            
            Debug.Log(jsonString);
        }

        private void Deseralize()
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