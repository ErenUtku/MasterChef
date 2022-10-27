using UnityEngine;

namespace Item
{
    public class IngredientThrow : MonoBehaviour
    {
        public bool isObjectThrowable;

        private Rigidbody rb;
        private IngredientMovement ingredientMovement;
        private Vector3 startPosition;
        private float delayTime = 0.01f;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            ingredientMovement = GetComponent<IngredientMovement>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && isObjectThrowable)
            {       
                CalculateCurrentPosition();
            }

            if (!Input.GetMouseButtonUp(0)) return;
        
            Vector3 mouseDelta = Input.mousePosition - startPosition;
            mouseDelta.z = 0;

            if (isObjectThrowable)
            {
                if (!ingredientMovement.isSelected)
                {
                    rb.AddForce(mouseDelta);
                }

                isObjectThrowable = false;
                delayTime = 0.01f;

            }

        }

        private void CalculateCurrentPosition()
        {
            if (delayTime > 0)
            {
                delayTime -= Time.deltaTime;
                return;
            }

            startPosition = Input.mousePosition;

            delayTime = 0.01f;
        }

    
    }
}
