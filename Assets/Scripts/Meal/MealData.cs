using UnityEngine;

namespace Meal
{
    [CreateAssetMenu(fileName = "MealData", menuName = "ScriptableObjects/CreateMeal", order = 1)]
    public class MealData : ScriptableObject
    {
        public string mealName;
        public GameObject mealObject;
        public int price;
    }
}
