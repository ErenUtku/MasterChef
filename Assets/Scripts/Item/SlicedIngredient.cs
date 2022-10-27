using Meal;
using UnityEngine;

namespace Item
{
    public class SlicedIngredient : MonoBehaviour
    { 
        private void OnEnable()
        {
            MealManager.instance.AddToList(this);
        }
    }
}
