using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Data;
using DG.Tweening;
public class MealManager : MonoBehaviour
{
    [SerializeField] private MealData[] allMeals;
    [SerializeField] private GameObject levelMeal;
    [SerializeField] private GameObject coinSprite;
    [SerializeField] private GameObject coinSpawnPosition;
    [SerializeField] private int mealPrice;
    [SerializeField] private List<SlicedIngredient> slicedIngredients;

    private void OnDisable()
    {
        slicedIngredients.Clear();
    }

    public static MealManager instance;
    private void Awake()
    {
        instance = this;
        LevelManager.OnLevelLoad += FindMeal;
        LevelManager.OnLevelComplete += CreateMeal;
    }

    private void OnDestroy()
    {
        LevelManager.OnLevelLoad -= FindMeal;
        LevelManager.OnLevelComplete -= CreateMeal;
    }

    public void FindMeal()
    {
        foreach (var meal in allMeals)
        {
            if (meal.mealName == StoreReceiptData.instance.receiptData.receiptName)
            {
                levelMeal = meal.mealObject;
                mealPrice = meal.price;
                return;
            }
        }
    }

    private void CreateMeal()
    {
        var levelFacade = LevelFacade.instance;
        foreach (var slicedObj in slicedIngredients)
        {
            slicedObj.transform.DOMove(levelFacade.targetPanTransform.transform.position, 2f).OnComplete(() =>
            {
                if (slicedIngredients[slicedIngredients.Count - 1] == slicedObj)
                {
                    var meal = Instantiate(levelMeal, levelFacade.targetPanTransform.transform.position, Quaternion.identity);
                    meal.transform.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
                    StartCoroutine(InstantiateCoins());                   
                }

                Destroy(slicedObj.gameObject);
            });
        }
    }

    public void AddToArray(SlicedIngredient obj)
    {
        slicedIngredients.Add(obj);
    }

    IEnumerator InstantiateCoins()
    {
        for (int i = 0; i < mealPrice; i++)
        {
            var coin = Instantiate(coinSprite, coinSpawnPosition.transform.position, Quaternion.identity);
            if (mealPrice - 1 == i)
            {
                coin.GetComponent<CoinMovement>().lastCoin = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
