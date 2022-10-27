using System.Collections.Generic;
using System.Linq;
using Item;
using Level;
using Order;
using Storage;
using UnityEngine;

namespace Cooking
{
    public class CookingManager : MonoBehaviour
    {
        
        public void CheckIngredient(Ingredient ingredient)
        {
            foreach (var receiptIngredient in StoreReceiptData.instance.receiptData.ingredients)
            {
                if (ingredient.ingredientData.ingredientName !=
                    receiptIngredient.ingredientData.ingredientName) continue;
                
                switch (receiptIngredient.amount)
                {
                    case 0:
                        ThrowObjectBack(ingredient);
                        return;
                    case > 0:
                        DecreaseAmount(receiptIngredient,ingredient);
                        break;
                }

                if (receiptIngredient.amount != 0) continue;
                
                CheckCookingDone();
                return;
            }

            ThrowObjectBack(ingredient);
        }

        private void DecreaseAmount(IngredientAmount value,Ingredient ingredient)
        {
            value.amount--;
            Destroy(ingredient.gameObject);

            var ingredientSliced = Instantiate(ingredient.ingredientSliced.gameObject, transform.position, Quaternion.identity);
        }

        private void CheckCookingDone()
        {
            if (!CookingIsDone(StoreReceiptData.instance.receiptData.ingredients)) return;
            
            Invoke(nameof(ReceiptDone), 2f);
            return;

        }

        private static void ThrowObjectBack(Component ingredient)
        {
            var ingredientMovement = ingredient.gameObject.GetComponent<IngredientMovement>();

            ingredientMovement.ThrowBack();
        
        }

        private static bool CookingIsDone(IEnumerable<IngredientAmount> ingredients)
        {
            return ingredients.All(ingredient => ingredient.amount == 0);
        }

        private void ReceiptDone()
        {
            LevelManager.LevelReceiptComplete();
        }

    }
}
