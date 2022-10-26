using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedIngredient : MonoBehaviour
{
    private void OnEnable()
    {
        MealManager.instance.AddToArray(this);
    }
}
