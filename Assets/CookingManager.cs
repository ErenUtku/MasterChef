using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Order;
using Items;
using Data;
public class CookingManager : MonoBehaviour
{
    private LevelFacade levelFacade;
    private StoreReceiptData receiptData;

    private void Start()
    {
        levelFacade = LevelFacade.instance;
        receiptData = StoreReceiptData.instance;
    }

    public void CheckIngredient(Ingredient ingredient)
    {
        foreach (var receiptIngredient in receiptData.receiptData.ingredients)
        {
            if(ingredient.ingredientData.ingredientName == receiptIngredient.ingredientData.ingredientName)
            {
                if (receiptIngredient.amount > 0)
                {
                    DecreaseAmount(receiptIngredient);
                    return;
                }
            }

            ThrowObjectBack(ingredient.gameObject);
            return;
        }
    }

    private void DecreaseAmount(IngredientAmount ingredient)
    {
        ingredient.amount--;
    }

    private void ThrowObjectBack(GameObject ingredient)
    {
        Debug.Log("Check AddForce Later");
        ingredient.GetComponent<Rigidbody>().AddForce(Vector3.back);
    }


}
