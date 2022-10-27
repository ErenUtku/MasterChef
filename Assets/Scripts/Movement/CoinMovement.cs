using DG.Tweening;
using Level;
using Meal;
using Storage;
using UnityEngine;

namespace Movement
{
    public class CoinMovement : MonoBehaviour
    {
        private Camera cam;

        private Transform target;
        private Vector3 targetPos;

        public bool lastCoin;

        private void Awake()
        {
            cam = Camera.main;
            target = UIManager.instance.GetCoinPos();
        }

        private void Start()
        {
            var position = target.position;
            targetPos = (new Vector3(position.x, position.y, cam.transform.position.z * -1));
            transform.DOMove(targetPos, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {      
                UIManager.instance.AddCoin(1);

                if (lastCoin)
                {
                    if (StoreReceiptData.instance.allReceiptsData.Count == 0)
                    {
                        LevelManager.onLevelComplete.Invoke();
                    }

                    else
                    {
                        MealManager.instance.FindMeal();
                        UIManager.instance.SetReceiptUI();
                    }
                }

                Destroy(this.gameObject);
            });
        }
    }
}
