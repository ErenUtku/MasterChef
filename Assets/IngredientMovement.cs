using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientMovement : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    public bool isMoving;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        isMoving = true;
    }

    private void OnMouseDrag()
    {
        if (isMoving)
        {
            var newVector = (GetMouseWorldPos() + mOffset);

            transform.position = (GetMouseWorldPos() + mOffset);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
