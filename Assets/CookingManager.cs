using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Order;
using Items;
using Data;
public class CookingManager : MonoBehaviour
{
    private LevelFacade levelFacade;
    private Receipt receiptData;

    private void Start()
    {
        levelFacade = LevelFacade.instance;
        receiptData = StoreReceiptData.instance.receiptData;
    }

    public void CheckIngredient(Ingredient ingredient)
    {
        foreach (var receiptIngredient in receiptData.ingredients)
        {
            if(ingredient.ingredientData.ingredientName == receiptIngredient.ingredientData.ingredientName)
            {
                if (receiptIngredient.amount == 0)
                {
                    ThrowObjectBack(ingredient);
                    return;
                }

                if (receiptIngredient.amount > 0)
                {
                    DecreaseAmount(receiptIngredient);
                }

                if(receiptIngredient.amount == 0)
                {
                    Invoke(nameof(CheckCookingDone),2f);
                }              
            }

            
        }
    }

    private void DecreaseAmount(IngredientAmount ingredient)
    {
        ingredient.amount--;
    }

    private void CheckCookingDone()
    {
        if (CookingIsDone(receiptData.ingredients))
        {
            Debug.Log("Level finished");
            UIManager.instance.LevelEndScreen(true);
            return;
        }

        Debug.Log("Level is NOT finished");
        return;
    }

    private void ThrowObjectBack(Ingredient ingredient)
    {
        var ingredientMovement = ingredient.gameObject.GetComponent<IngredientMovement>();

        ingredientMovement.ThrowBack();
        
    }

    private bool CookingIsDone(IngredientAmount[] ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            if (ingredient.amount != 0)
            {
                return false;
            }
        }

        return true;
    }


}
