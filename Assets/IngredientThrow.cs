using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientThrow : MonoBehaviour
{
    public bool isObjectThrowable;

    private Rigidbody _rb;
    private IngredientMovement _ingredientMovement;
    private Vector3 startPosition;
    private float delayTime = 0.01f;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _ingredientMovement = GetComponent<IngredientMovement>();
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
            if (!_ingredientMovement.isSelected)
            {
                _rb.AddForce(mouseDelta);
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
