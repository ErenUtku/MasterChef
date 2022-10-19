using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
public class CookingCollider : MonoBehaviour
{
    private CookingManager cookingManager;

    private void Awake()
    {
        cookingManager = FindObjectOfType<CookingManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ingredient"))
        {
            var ingredient = other.gameObject.GetComponentInParent<Ingredient>();
            Debug.Log("Yes check me");

            cookingManager.CheckIngredient(ingredient);

        }
    }
}
