using System;
using UnityEngine;
using Item;

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
    public class IngredientAmount
    {
        public IngredientData ingredientData;
        public int amount;
    }
}
