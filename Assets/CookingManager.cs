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

    private void Start()
    {
        levelFacade = LevelFacade.instance;
    }

    public void CheckIngredient(Ingredient ingredient)
    {
        foreach (var receiptIngredient in StoreReceiptData.instance.receiptData.ingredients)
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
                    return;
                }                
            }          
        }

        ThrowObjectBack(ingredient);
    }

    private void DecreaseAmount(IngredientAmount value,Ingredient ingredient)
    {
        value.amount--;
        Destroy(ingredient.gameObject);

        var IngredientSliced = Instantiate(ingredient.ingredientSliced.gameObject, transform.position, Quaternion.identity);
    }

    private void CheckCookingDone()
    {
        if (CookingIsDone(StoreReceiptData.instance.receiptData.ingredients))
        {
            Debug.Log("Receipt finished");
            Invoke(nameof(ReceiptDone), 2f);
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

    private void ReceiptDone()
    {
        LevelManager.instance.LevelReceiptComplete();
    }

}
