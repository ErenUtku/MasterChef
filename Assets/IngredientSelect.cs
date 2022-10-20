using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class IngredientSelect : MonoBehaviour
{
    [Header("Click Elements")]
    private float firstClickTime;
    private float timeBetweenClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int clickNumber = 0;

    public bool isObjectSelectable;

    private IngredientMovement _ingredientMovement;

    private void Start()
    {
        _ingredientMovement = GetComponent<IngredientMovement>();
    }
    private void Update()
    {
        if (!isObjectSelectable) return;

        if (Input.GetMouseButtonUp(0))
        {
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
                _ingredientMovement.isSelected = true;
                _ingredientMovement.KillDoTweens();
                
                transform.DOMove(LevelFacade.instance.targetPanTransform.transform.position, 2f);

                var rb = GetComponent<Rigidbody>();
                rb.isKinematic = true;

                break;
            }
            yield return new WaitForEndOfFrame();
        }

        clickNumber = 0;
        isObjectSelectable = false;
        isTimeCheckAllowed = true;
        
    }
}
