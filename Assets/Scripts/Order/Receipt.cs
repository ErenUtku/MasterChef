using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Order
{
    [CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/CreateOrder", order = 1)]
    public class Receipt : ScriptableObject
    {
        public string receiptName;
        public IngredientAmount[] ingredients;
        public GameObject resultFood;
    }
}

namespace Order
{
    [Serializable]
    public class IngredientAmount
    {
        public IngredientData ingredientData;
        public int amount;
    }
}
