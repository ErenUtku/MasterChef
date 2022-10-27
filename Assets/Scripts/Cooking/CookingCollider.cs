using UnityEngine;

namespace Cooking
{
    public class CookingCollider : MonoBehaviour
    {
        private CookingManager cookingManager;

        private void Awake()
        {
            cookingManager = gameObject.GetComponent<CookingManager>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("Ingredient")) return;
            
            var ingredient = other.gameObject.GetComponentInParent<Item.Ingredient>();
            cookingManager.CheckIngredient(ingredient);
        }
    }
}
