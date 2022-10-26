using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Controllers;
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
            if (lastCoin)
            {
                LevelManager.OnLevelStageComplete.Invoke();
            }

            UIManager.instance.AddCoin(1);
            Destroy(this.gameObject);
        });
    }
}
