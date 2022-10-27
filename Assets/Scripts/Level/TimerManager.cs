using UnityEngine;

namespace Level
{
    public class TimerManager : MonoBehaviour
    {
        private UIManager uiManager;

        public float timeLeft;
        public bool timerOn;

        private void Awake()
        {
            LevelManager.onLevelLoad += LevelLoadTimer;
            LevelManager.onLevelComplete += StopTimer;
        }

        private void Start()
        {     
            uiManager = UIManager.instance;
        }

        private void OnDestroy()
        {
            LevelManager.onLevelLoad -= LevelLoadTimer;
            LevelManager.onLevelComplete -= StopTimer;
        }

        private void Update()
        {
            if (!timerOn) return;
            
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                LevelManager.onLevelFail.Invoke();
                timerOn = false;
            }
        }

        private void UpdateTimer(float currentTime)
        {
            currentTime += 1;
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            var setText = ($"{minutes:00} : {seconds:00}");

            uiManager.UpdateTimer(setText);
        }

        #region METHODS

        private void SetTimer(float value)
        {
            timeLeft = value;
        }

        private void StartTimer(bool value)
        {
            timerOn = value;
        }

        #endregion

        #region EVENTS

        private void LevelLoadTimer()
        {
            SetTimer(LevelFacade.instance.TimeLeft());
            StartTimer(true);
        }

        private void StopTimer()
        {
            StartTimer(false);
        }

        #endregion

    }
}
