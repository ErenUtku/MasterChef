using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IngredientMovement), typeof(IngredientThrow),typeof(IngredientSelect))]

    public class Ingredient : MonoBehaviour
    {
        public IngredientData ingredientData;

        public SlicedIngredient ingredientSliced;
    }
}