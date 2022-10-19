using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
public class IngredientsSpriteManager : MonoBehaviour
{
    public Ingredients2D[] ingredientsSprite;

    public Sprite CheckIngredient2DSprite(IngredientData ingredient)
    {
        foreach (var ingredients2D in ingredientsSprite)
        {
            if (ingredients2D.ingredientName2D == ingredient.ingredientName)
            {
                return ingredients2D.ingredientSprite;
            }
        }

        Debug.LogWarning("You missed the sprite with matching Ingredient, Check your data again please");
        return null;
    }
    
}
