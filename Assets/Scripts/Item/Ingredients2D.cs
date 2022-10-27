using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "Ingredient2D", menuName = "ScriptableObjects/CreateIngredient2DSprite", order = 1)]
    public class Ingredients2D : ScriptableObject
    {
        public string ingredientName2D;
        public Sprite ingredientSprite;
    }
}
