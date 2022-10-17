using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredient
{
    [CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/CreateIngredient", order = 1)]
    public class IngredientData : ScriptableObject
    {      
        public string ingredientName;
        public int ingredientValue;
        public Sprite ingredientSprite;
    }
}
