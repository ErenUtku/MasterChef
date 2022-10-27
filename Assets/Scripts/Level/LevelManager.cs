using Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        #region DELEGATE

        public delegate void LevelLoadHandler();

        public delegate void LevelStageCompleteHandler();

        public delegate void LevelCompleteHandler();

        public delegate void LevelFailHandler();

        #endregion

        #region EVENTS

        public static LevelLoadHandler onLevelLoad;

        public static LevelStageCompleteHandler onLevelReceiptComplete;
        
        public static LevelCompleteHandler onLevelComplete;
        
        public static LevelFailHandler onLevelFail;

        #endregion

        #region SERIALIZE PRIVATE FIELDS
        
        [SerializeField] private LevelSource levelSource;

        [SerializeField] private GameObject levelSpawnPoint;
        
        [SerializeField] private int loopLevelsStartIndex = 1;
        
        [SerializeField] private bool loopLevelGetRandom = true;

        #endregion

        #region PRIVATE FIELDS

        private GameObject activeLevel;

        #endregion

        #region PRIVATE METHODS

        private void CheckRepeatLevelIndex()
        {
            if (loopLevelsStartIndex < levelSource.levelData.Length) return;
            loopLevelsStartIndex = 0;
        }

        private GameObject GetLevel()
        {
            if (PlayerPrefsController.GetLevelIndex() >= levelSource.levelData.Length)
            {
                if (loopLevelGetRandom)
                {
                    var levelIndex = Random.Range(loopLevelsStartIndex, levelSource.levelData.Length - 1);
                    PlayerPrefsController.SetLevelIndex(levelIndex);
                }
            }

            var level = levelSource.levelData[PlayerPrefsController.GetLevelIndex()];

            var levelData = level.GetComponent<LevelFacade>();

            levelData.levelIndex = PlayerPrefsController.GetLevelIndex();
            levelData.levelNumber = PlayerPrefsController.GetLevelNumber();

            return level;
        }
        
        private void LevelLoad()
        {
            activeLevel = Instantiate(GetLevel(), levelSpawnPoint.transform, false);
            onLevelLoad?.Invoke();           
        }

        #endregion

        #region PUBLIC METHODS
        
        public void LoadMenuScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        public static void LevelReceiptComplete()
        {
            StoreReceiptData.instance.GoToNextReceipt();
        }
        
        public void LevelComplete()
        {
            if (PlayerPrefsController.GetLevelNumber() % 3 == 0)
            {
                PlayerPrefsController.SetLevelChest(1);
            }
            PlayerPrefsController.SetLevelIndex(PlayerPrefsController.GetLevelIndex() + 1);

            PlayerPrefsController.SetLevelNumber(PlayerPrefsController.GetLevelNumber() + 1);         

#pragma warning disable CS0618
            Application.LoadLevel(Application.loadedLevel);
#pragma warning restore CS0618
        }

        public void LevelFail()
        {
#pragma warning disable CS0618
            Application.LoadLevel(Application.loadedLevel);
#pragma warning restore CS0618
        }

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            CheckRepeatLevelIndex();
        }

        private void Start() => LevelLoad();

        #endregion
    }
}