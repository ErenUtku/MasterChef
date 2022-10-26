using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IngredientMovement), typeof(IngredientThrow),typeof(IngredientSelect))]

    public class Ingredient : MonoBehaviour
    {
        public IngredientData ingredientData;

        public SlicedIngredient ingredientSliced;
    }
}