using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Item;
using Level;
using Movement;
using Storage;
using UnityEngine;

namespace Meal
{
    public class MealManager : MonoBehaviour
    {
        [Header("All Meals Data")] 
        [SerializeField] private MealData[] allMeals;
        [SerializeField] private List<SlicedIngredient> slicedIngredients;
        
        [Header("Coin Components")]//3D OBJECT COIN AND SPAWN POSITION, NOT UI
        [SerializeField] private GameObject coinSprite;
        [SerializeField] private GameObject coinSpawnPosition;
        
        private int mealPrice;
        
        [HideInInspector] public GameObject levelMeal;

        private void OnDisable()
        {
            slicedIngredients.Clear();
        }

        public static MealManager instance;
        private void Awake()
        {
            instance = this;

            LevelManager.onLevelLoad += FindMeal;

            LevelManager.onLevelReceiptComplete += CreateMeal;
        }

        private void OnDestroy()
        {
            LevelManager.onLevelLoad -= FindMeal;
        
            LevelManager.onLevelReceiptComplete -= CreateMeal;
        }

        public void FindMeal()
        {
            foreach (var meal in allMeals)
            {
                if (meal.mealName != StoreReceiptData.instance.receiptData.receiptName) continue;
                levelMeal = meal.mealObject;
                mealPrice = meal.price;
                return;
            }
        }

        private void CreateMeal()
        {
            var levelFacade = LevelFacade.instance;
            foreach (var slicedObj in slicedIngredients)
            {
                slicedObj.transform.DOMove(levelFacade.targetPanTransform.transform.position, 2f).OnComplete(() =>
                {
                    if (slicedIngredients[^1] == slicedObj)
                    {
                        var meal = Instantiate(levelMeal, levelFacade.targetPanTransform.transform.position, Quaternion.identity);
                        meal.transform.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
                    
                        StartCoroutine(InstantiateCoins(meal));
                        slicedIngredients.Clear();
                    }
              
                    Destroy(slicedObj.gameObject);
                });
            }      
        
        }

        public void AddToList(SlicedIngredient obj)
        {
            slicedIngredients.Add(obj);
        }

        private IEnumerator InstantiateCoins(GameObject meal)
        {
            for (var i = 0; i < mealPrice; i++)
            {
                var coin = Instantiate(coinSprite, coinSpawnPosition.transform.position, Quaternion.identity);
                if (mealPrice - 1 == i)
                {
                    coin.GetComponent<CoinMovement>().lastCoin = true;
                    meal.transform.DOMoveX(-5, 2f).OnComplete(() => Destroy(meal.gameObject));
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
