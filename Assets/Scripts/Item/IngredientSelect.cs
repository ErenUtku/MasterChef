using System.Collections;
using DG.Tweening;
using Level;
using UnityEngine;

namespace Item
{
    public class IngredientSelect : MonoBehaviour
    {
        [Header("Click Elements")]
        private float firstClickTime;
        private const float TimeBetweenClick = 0.2f;
        private bool isTimeCheckAllowed = true;
        private int clickNumber = 0;

        public bool isObjectSelectable;

        private IngredientMovement ingredientMovement;

        private void Start()
        {
            ingredientMovement = GetComponent<IngredientMovement>();
        }
        private void Update()
        {
            if (!isObjectSelectable) return;

            if (!Input.GetMouseButtonUp(0)) return;
            
            clickNumber++;

            if (clickNumber != 1 || !isTimeCheckAllowed) return;
            
            firstClickTime = Time.time;
            StartCoroutine(DetectDoubleClick());
        }

        private IEnumerator DetectDoubleClick()
        {
            isTimeCheckAllowed = false;
            while (Time.time < firstClickTime + TimeBetweenClick)
            {
                if (clickNumber == 2)
                {
                    ingredientMovement.isSelected = true;
                
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
}
