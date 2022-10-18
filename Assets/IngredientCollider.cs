using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCollider : MonoBehaviour
{
    [SerializeField] private IngredientMovement movement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TopCollider"))
        {
            Debug.Log("Cant move");
            movement.isMoving = false;
        }
    }
}
