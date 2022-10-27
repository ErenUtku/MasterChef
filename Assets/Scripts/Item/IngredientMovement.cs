using System;
using DG.Tweening;
using Level;
using UnityEngine;

namespace Item
{
    public class IngredientMovement : MonoBehaviour
    {
        //Items Alt Movements
        private IngredientThrow ingredientThrow;
        private IngredientSelect ingredientSelect;

        //Border and Float values
        private float floatDensity;
        private float floatTime;

        //Border Values
        private float borderX;
        private float borderY;
        //Z Offsets
        private Vector3 mOffset;
        private float mZCoord;

        //Components
        private Rigidbody rb;
        private LevelFacade levelFacade;

        //DoTween
        private Sequence mySequence;
        private Guid uid;
        
        //Ingredient Selected by Player
        public bool isSelected;

        private void Start()
        {
            levelFacade = LevelFacade.instance;

            #region BORDERS

            floatDensity = levelFacade.FloatDensity();
            floatTime = levelFacade.FloatTime();
            borderX = levelFacade.BorderX();
            borderY = levelFacade.BorderY();

            #endregion

            ingredientThrow = GetComponent<IngredientThrow>();
            ingredientSelect = GetComponent<IngredientSelect>();
            rb = GetComponent<Rigidbody>();   
        }

        private void OnMouseDown()
        {
            if (isSelected) return;
            
            if (Camera.main != null) mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();

            //Floating (Creating Sequence with Unique ID)

            if (mySequence == null)
            {
                mySequence = DOTween.Sequence();

                mySequence.Append(transform.DOMoveZ(floatDensity, floatTime));
                //.OnComplete(() => { mOffset.z = floatDensity; });

                uid = System.Guid.NewGuid();
                mySequence.id = uid;
            }

            mySequence.Play();

            //End Floating

            ingredientThrow.isObjectThrowable = true;
            ingredientSelect.isObjectSelectable = true;
        }

        private void OnMouseDrag()
        {
            if (isSelected) return;
            
            var newVector = (GetMouseWorldPos() + mOffset);
            transform.position = new Vector3(newVector.x, newVector.y, floatDensity);
        }

        private void OnMouseUp()
        {
            KillSequence();
        }

        private void Update()
        {

            if (isSelected) return;

            #region Border Calculation

            if (transform.position.x >= borderX)
            {
                var position = transform.position;
                position = new Vector3(borderX, position.y, position.z);
                transform.position = position;
            }
            if (transform.position.x <= -borderX)
            {
                var position = transform.position;
                position = new Vector3(-borderX, position.y, position.z);
                transform.position = position;
            }

            if (transform.position.y >= borderY)
            {
                var position = transform.position;
                position = new Vector3(position.x ,borderY, position.z);
                transform.position = position;
            }

            if (transform.position.y <= -borderY)
            {
                var position = transform.position;
                position = new Vector3(position.x, -borderY, position.z);
                transform.position = position;
            }

            #endregion

        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;

            mousePoint.z = mZCoord + floatDensity;

            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        private void KillSequence()
        {
            DOTween.Kill(uid);
            mySequence = null;
            //DOTween.Kill(mySequence);
        }

        public void ThrowBack()
        {
            rb.isKinematic = false;
            rb.AddForce(0, 5, 0, ForceMode.Impulse);
            Invoke(nameof(KeepTheBorder), 1f);
        }

        private void KeepTheBorder()
        {
            isSelected = false;
        }


    }
}