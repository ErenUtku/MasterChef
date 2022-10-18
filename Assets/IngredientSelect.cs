using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientSelect : MonoBehaviour
{
    [Header("Click Elements")]
    private float firstClickTime;
    private float timeBetweenClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int clickNumber = 0;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Im up");
            clickNumber++;

            if (clickNumber == 1 && isTimeCheckAllowed)
            {
                firstClickTime = Time.time;
                StartCoroutine(DetectDoubleClick());
            }
        }
    }

    private IEnumerator DetectDoubleClick()
    {
        isTimeCheckAllowed = false;
        while (Time.time < firstClickTime + timeBetweenClick)
        {
            if (clickNumber == 2)
            {
                Debug.Log("DOUBLECLICK");
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        clickNumber = 0;
        isTimeCheckAllowed = true;
        
    }
}
