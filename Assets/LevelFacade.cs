using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Order;

public class LevelFacade : MonoBehaviour
{
    public Receipt levelReceipt;

    public static LevelFacade instance;
    private void Awake()
    {
        instance = this;
    }
}
