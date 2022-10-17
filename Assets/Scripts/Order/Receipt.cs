using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingredient;

namespace Order
{
    [CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/CreateOrder", order = 1)]
    public class Receipt : ScriptableObject
    {
        public string receiptName;
        public IngredientAmount[] ingredients;
    }
}

namespace Order
{
    [Serializable]
    public struct IngredientAmount
    {
        public IngredientData ingredient;
        public int amount;
    }
}
