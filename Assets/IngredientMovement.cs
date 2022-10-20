using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IngredientMovement : MonoBehaviour
{
    [Header("Items Alt Movements")]
    private IngredientThrow _ingredientThrow;
    private IngredientSelect _ingredientSelect;

    [Header("Floating Values")]
    [SerializeField] private float floatDensity = -2f;
    [SerializeField] private float floatTime = 0.5f;

    [Header("Border Values")]
    [SerializeField] private float borderX = 2.5f;
    [SerializeField] private float borderY = 3f;

    private Vector3 mOffset;
    private float mZCoord;

    private Rigidbody _rb;
    public bool isSelected;

    private void Start()
    {
        _ingredientThrow = GetComponent<IngredientThrow>();
        _ingredientSelect = GetComponent<IngredientSelect>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            
            //Floating
            transform.DOMoveZ(floatDensity, floatTime).OnComplete(() => { mOffset.z = floatDensity; });
            //End Floating

            _ingredientThrow.isObjectThrowable = true;
            _ingredientSelect.isObjectSelectable = true;
        }
    }

    private void OnMouseDrag()
    {
        if (!isSelected)
        {
            var newVector = (GetMouseWorldPos() + mOffset);
            transform.position = new Vector3(newVector.x, newVector.y, floatDensity);
        }
    }

    private void OnMouseUp()
    {
        if (!isSelected)
        { 
            KillDoTweens();
        }
    }

    private void Update()
    {

        if (isSelected) return;

        #region Border Calculation

        if (transform.position.x >= borderX)
        {
            transform.position = new Vector3(borderX, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -borderX)
        {
            transform.position = new Vector3(-borderX, transform.position.y, transform.position.z);
        }

        if (transform.position.y >= borderY)
        {
            transform.position = new Vector3(transform.position.x ,borderY, transform.position.z);
        }

        if (transform.position.y <= -borderY)
        {
            transform.position = new Vector3(transform.position.x, -borderY, transform.position.z);
        }
        #endregion

    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord + floatDensity;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void KillDoTweens()
    {
        DOTween.KillAll();
    }

    public void ThrowBack()
    {
        _rb.isKinematic = false;
        _rb.AddForce(0, 5, 0, ForceMode.Impulse);
        Invoke(nameof(KeepTheBorder), 1f);
    }

    private void KeepTheBorder()
    {
        isSelected = false;
    }


}
