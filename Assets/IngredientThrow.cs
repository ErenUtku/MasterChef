using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientThrow : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private IngredientMovement ingredientMovement;

    public Vector3 startPosition;

    public bool isObjectActive;

    private float delayTime = 0.01f;

    private void Update()
    {
        if (Input.GetMouseButton(0) && isObjectActive)
        {       
            CalculateCurrentPosition();
        }

        if (!Input.GetMouseButtonUp(0)) return;
        
        Vector3 mouseDelta = Input.mousePosition - startPosition;
        mouseDelta.z = 0;

        if (isObjectActive)
        {
            if (!ingredientMovement.isSelected)
            {
                rb.AddForce(mouseDelta);
            }

            isObjectActive = false;
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
