using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

public class TimerManager : MonoBehaviour
{
    private UIManager uiManager;

    public float timeLeft;
    public bool TimerOn = false;

    public static TimerManager instance;
    private void Awake()
    {
        instance = this;
        LevelManager.OnLevelLoad += LevelLoadTimer;
        LevelManager.OnLevelComplete += StopTimer;
    }

    private void Start()
    {     
        uiManager = UIManager.instance;
    }

    private void OnDestroy()
    {
        LevelManager.OnLevelLoad -= LevelLoadTimer;
        LevelManager.OnLevelComplete -= StopTimer;
    }

    private void Update()
    {
        if (TimerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                LevelManager.OnLevelFail.Invoke();
                TimerOn = false;
                return;
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        var SetText = (string.Format("{0:00} : {1:00}", minutes, seconds));

        uiManager.UpdateTimer(SetText);
    }

    #region METHODS

    private void SetTimer(float value)
    {
        timeLeft = value;
    }

    private void StartTimer(bool value)
    {
        TimerOn = value;
    }

    #endregion

    #region EVENTS

    public void LevelLoadTimer()
    {
        SetTimer(LevelFacade.instance.TimeLeft());
        StartTimer(true);
    }

    public void StopTimer()
    {
        StartTimer(false);
    }

    #endregion

}
