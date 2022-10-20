using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Order;

public class LevelFacade : MonoBehaviour
{
    [Header("Level Details")]
    public int levelIndex;
    public int levelNumber;

    [Header("INGREDIENTS")]
    [Header("Floating Values")]
    [SerializeField] private float floatDensity = -2f;
    [SerializeField] private float floatTime = 0.5f;

    [Header("Border Values")]
    [SerializeField] private float borderX = 2.5f;
    [SerializeField] private float borderY = 3f;

    public GameObject targetPanTransform;

    public static LevelFacade instance;
    private void Awake()
    {
        instance = this;
    }

    public float FloatDensity()
    {
        return floatDensity;
    }

    public float FloatTime()
    {
        return floatTime;
    }

    public float BorderX()
    {
        return borderX;
    }
    public float BorderY()
    {
        return borderY;
    }
}
