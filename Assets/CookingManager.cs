using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Order;
using Items;
using Data;
using Controllers;
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
                    DecreaseAmount(receiptIngredient,ingredient);
                }

                if(receiptIngredient.amount == 0)
                {
                    CheckCookingDone();
                }              
            }

            
        }
    }

    private void DecreaseAmount(IngredientAmount value,Ingredient ingredient)
    {
        value.amount--;
        Destroy(ingredient.gameObject);

        var IngredientSliced = Instantiate(ingredient.ingredientSliced.gameObject, transform.position, Quaternion.identity);
    }

    private void CheckCookingDone()
    {
        if (CookingIsDone(receiptData.ingredients))
        {
            Debug.Log("Level finished");
            Invoke(nameof(LevelFinish), 2f);
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

    private void LevelFinish()
    {
        LevelManager.OnLevelComplete.Invoke();
    }

}
